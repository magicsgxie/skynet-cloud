using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;
using System.Collections.Specialized;
using UWay.Skynet.Cloud.ExceptionHandle;
using System.Collections;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    class ClassToDictionaryMapper : MapperBase
    {

        MappingItem[] members;

        public ClassToDictionaryMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {

        }

        class MappingItem
        {
            public string Name;
            public Getter Getter;
        }

        public override void Map(ref object from, ref object to)
        {
            if (from == null)
                return;

            if (members == null)
                members = _Info
                .SourceMembers
                .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property)
                .Select(m => new MappingItem { Name = m.Name, Getter = m.GetGetter() })
                .ToArray();

            if (to == null)
            {
                if (Types.IDictionaryOfStringAndString.IsAssignableFrom(base._Info.To))
                {
                    to = new Dictionary<string, string>(members.Length);
                }
                else if (Types.IDictionaryOfStringAndObject.IsAssignableFrom(base._Info.To))
                {
                    to = new Dictionary<string, object>(members.Length);
                }
                else if (Types.NameValueCollection.IsAssignableFrom(base._Info.To))
                {
                    to = new NameValueCollection(members.Length);
                }
                else if (Types.StringDictionary.IsAssignableFrom(base._Info.To))
                {
                    to = new StringDictionary();
                }
                else if (typeof(Hashtable).IsAssignableFrom(base._Info.To))
                {
                    to = new Hashtable(members.Length);
                }
            }

            if (Types.IDictionaryOfStringAndString.IsAssignableFrom(base._Info.To))
            {
                var dic = to as IDictionary<string, string>;
                foreach (var m in members)
                    dic[m.Name] = Converter.Convert(m.Getter(from), typeof(string)) as string;

                to = dic;
            }
            else if (Types.IDictionaryOfStringAndObject.IsAssignableFrom(base._Info.To))
            {
                var dic = to as IDictionary<string, object>;
                foreach (var m in members)
                    dic[m.Name] = m.Getter(from);

                to = dic;
            }
            else if (Types.NameValueCollection.IsAssignableFrom(base._Info.To))
            {
                var dic = to as NameValueCollection;
                foreach (var m in members)
                    dic[m.Name] = Converter.Convert(m.Getter(from), typeof(string)) as string;

                to = dic;
            }
            else if (Types.StringDictionary.IsAssignableFrom(base._Info.To))
            {
                var dic = to as StringDictionary;
                foreach (var m in members)
                    dic[m.Name] = Converter.Convert(m.Getter(from), typeof(string)) as string;

                to = dic;
            }
            else if (typeof(Hashtable).IsAssignableFrom(base._Info.To))
            {
                var dic = to as Hashtable;
                foreach (var m in members)
                    dic[m.Name] = m.Getter(from);

                to = dic;
            }
        }
    }

    class DictonaryToClassMapper : MapperBase
    {
        public DictonaryToClassMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {
        }


        public override void Map(ref object from, ref object to)
        {
            if (to == null)
                to = ObjectCreator.Create(_Info.To);

            if (_Info.From.IsDictionaryType())
            {
                var dicType = _Info.From.GetGenericDictionaryType();
                var keyType = dicType.GetGenericArguments()[0];
                var valueType = dicType.GetGenericArguments()[1];

                if (keyType == Types.String)
                {
                    if (valueType == Types.String)
                        new DictionaryOfStringAndString { State = this.State }.Map(ref _Info, ref from, ref to);
                    else
                        new DictionaryOfStringAndObject { State = this.State }.Map(ref _Info, ref from, ref to);
                }
            }
            else if (Types.StringDictionary.IsAssignableFrom(_Info.From))
            {
                var dic = from as StringDictionary;
                var tmpFrom = new Dictionary<string, string>(dic.Count);
                foreach (string k in dic.Keys)
                    tmpFrom.Add(k, dic[k]);

                new DictionaryOfStringAndString { State = this.State }.Map(ref _Info, ref from, ref to);

                tmpFrom.Clear();
                tmpFrom = null;
            }
            else if (Types.NameValueCollection.IsAssignableFrom(_Info.From))
            {
                var dic = from as NameValueCollection;
                var tmpFrom = new Dictionary<string, string>(dic.Count);
                foreach (string k in dic.Keys)
                    tmpFrom.Add(k, dic[k]);

                new DictionaryOfStringAndString { State = this.State }.Map(ref _Info, ref from, ref to);

                tmpFrom.Clear();
                tmpFrom = null;
            }
        }

        interface IDictionaryMapper
        {
            void Map(ref MapperInfo _Info, ref object from, ref object to);
        }

        class DictionaryMapper<TValue> : IDictionaryMapper
        {
            public IErrorState State;
            protected readonly Type ValueType = typeof(TValue);

            public void Map(ref MapperInfo _Info, ref object from, ref object to)
            {
                var dic = from as IDictionary<string, TValue>;
                IDictionary<string, TValue> tmpDic = dic;

                if (_Info.IgnoreCase)
                    tmpDic = new Dictionary<string, TValue>(dic, _Info.IgnoreCase ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture);

                var tmpMembers = _Info
                    .DestinationMembers
                    .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property)
                    .ToArray();


                var keys = tmpDic.Keys.ToList();

                var ms = tmpMembers;
                foreach (var key in keys)
                {
                    var tmpValue = tmpDic[key];
                    var m = ms.FirstOrDefault(s => string.Equals(s.Name, key, StringComparison.OrdinalIgnoreCase));
                    if (m != null)
                        BindProperty(ref to, tmpValue, m);
                    else
                        BindSubProperty(ref to, key, tmpValue);
                }
                tmpDic.Clear();
                tmpDic = null;
            }

            private void BindProperty(ref object to, TValue tmpValue, MemberInfo m)
            {
                var memberType = m.GetMemberType();

                if (tmpValue != null
                    && tmpValue.GetType() == Types.String
                    && memberType.IsArray)
                {
                    if ((tmpValue as string).Contains(","))
                    {
                        var attr = m.GetAttribute<SpliteAttribute>(true);
                        var value = attr == null ? new string[] { tmpValue as string }
                            : (tmpValue as string).Split(attr.Separator);
                        if (memberType.GetElementType() == Types.String)
                            m.Set(to, value);
                        else
                            m.Set(to, Mapper.Map(value, typeof(string[]), memberType));
                    }
                }
                else
                {
                    if (memberType == ValueType || memberType.IsAssignableFrom(ValueType))
                        m.Set(to, tmpValue);
                    else
                        m.Set(to, Mapper.Map(tmpValue, ValueType, memberType));
                }
            }

            private static readonly BindingFlags propertyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;


            private void BindSubProperty(ref object root, string propertyName, TValue value)
            {
                if (root == null) return;
                if (!propertyName.Contains(".")
                     && !propertyName.Contains("[")) return;


                string[] propertyParts = propertyName.Split('.');
                object target = root;
                for (int i = 0; i < propertyParts.Length; i++)
                {
                    var rawSubPropertyName = propertyParts[i];
                    var members = target.GetType().GetMembers(propertyBindingFlags | BindingFlags.SetField | BindingFlags.SetProperty);
                    var p = members.FirstOrDefault(m => string.Equals(m.Name, rawSubPropertyName, StringComparison.OrdinalIgnoreCase));
                    if (p == null)
                        continue;
                    try
                    {
                        target = BindPropertyValue(ref value, target, i < propertyParts.Length - 1, p);
                    }
                    catch { }
                }
            }

            private object BindPropertyValue(ref TValue value, object target, bool flag, MemberInfo p)
            {
                var memberType = p.GetMemberType();
                //取最后一个即属性的本身(过滤了该属性的基类)
                if (flag)
                {
                    var parent = target;
                    target = p.Get(target);//p.GetValue(target, null);
                    if (target == null)
                    {
                        target = ObjectCreator.Create(memberType);
                        p.Set(parent, target);
                    }
                }
                else
                {
                    if (memberType == ValueType || memberType.IsAssignableFrom(ValueType))
                        p.Set(target, value);
                    else
                        p.Set(target, Mapper.Map(value, ValueType, memberType));
                }

                return target;
            }

        }

        class DictionaryOfStringAndString : DictionaryMapper<string> { }
        class DictionaryOfStringAndObject : DictionaryMapper<Object> { }
    }
}
