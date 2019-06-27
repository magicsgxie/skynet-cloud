using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Reflection;
using System.Collections;


namespace UWay.Skynet.Cloud.Mapping.Internal
{
    internal struct DictionaryInfo
    {
        /// <summary>
        /// Dictionary Type
        /// </summary>
        public Type Type;
        public Type Key;
        public Type Value;
        public Type Kvp;


        public bool IsGeneric
        {
            get { return Type != null && typeof(DictionaryEntry) != Kvp; }
        }

        static readonly Type KvpType = typeof(KeyValuePair<,>);
        public static DictionaryInfo Get(Type type)
        {
            DictionaryInfo info;
            if (TypeHelper.IsGenericDictionaryType(type))
            {
                info.Type = type.GetGenericDictionaryType();
                info.Key = info.Type.GetGenericArguments()[0];
                info.Value = info.Type.GetGenericArguments()[1];
                info.Kvp = KvpType.MakeGenericType(info.Key, info.Value);
            }
            else
            {
                info.Type = type;
                info.Key = Types.Object;
                info.Value = Types.Object;
                info.Kvp = typeof(DictionaryEntry);
            }

            return info;
        }

    }

    class DictionaryMapper : MapperBase
    {
        readonly Type fromType, toType;

        static readonly Type KvpType = typeof(KeyValuePair<,>);

        public DictionaryMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {
            this.fromType = fromType;
            this.toType = toType;
        }

        public override void Map(ref object from, ref object to)
        {
            if (from == null)
                return;
            var sourceEnumerableValue = (IEnumerable)from;

            var sourceInfo = new DictionaryInfo();
            var destInfo = new DictionaryInfo();

            GetDictionaryInfo(fromType, ref sourceInfo);
            GetDictionaryInfo(toType, ref destInfo);

            if (to == null)
            {
                if (destInfo.IsGeneric)
                    to = ObjectCreator.Create(typeof(Dictionary<,>).MakeGenericType(destInfo.Key, destInfo.Value));
                else
                    to = ObjectCreator.Create(destInfo.Type);
            }

            var addMethod = destInfo.Type.GetMethod("Add", new Type[] { destInfo.Key, destInfo.Value }).GetProc();
            var getKey = sourceInfo.Kvp.GetProperty("Key").GetGetter();
            var getValue = sourceInfo.Kvp.GetProperty("Value").GetGetter();

            var kvpEquals = sourceInfo.Kvp == destInfo.Kvp;
            var keyEquals = kvpEquals ? true
                : (sourceInfo.Key == destInfo.Key
                || destInfo.Key.IsAssignableFrom(sourceInfo.Key));

            var valueEquals = kvpEquals ? true
                : (sourceInfo.Value == destInfo.Value
                || destInfo.Value.IsAssignableFrom(sourceInfo.Value));

            foreach (var item in sourceEnumerableValue)
            {
                var key = getKey(item);
                var value = getValue(item);


                var dstKey =
                    keyEquals
                    ?
                    key
                    : Mapper.Map(key, Types.Object, destInfo.Key);

                var dstValue =
                    valueEquals
                    ?
                    value
                    :
                    Mapper.Map(value, Types.Object, destInfo.Value);

                addMethod(to, dstKey, dstValue);
            }
        }


        internal static void GetDictionaryInfo(Type type, ref DictionaryInfo info)
        {
            if (TypeHelper.IsGenericDictionaryType(type))
            {
                info.Type = type.GetGenericDictionaryType();
                info.Key = info.Type.GetGenericArguments()[0];
                info.Value = info.Type.GetGenericArguments()[1];
                info.Kvp = KvpType.MakeGenericType(info.Key, info.Value);
            }
            else
            {
                info.Type = type;
                info.Key = Types.Object;
                info.Value = Types.Object;
                info.Kvp = typeof(DictionaryEntry);
            }
        }


    }
}
