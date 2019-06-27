using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Mapping.Internal;

namespace UWay.Skynet.Cloud.Reflection
{
    static class ObjectCreator
    {
        class CtorMapping
        {
            public ConstructorInfo Constructor;
            public ConstructorHandler Handler;
        }

        static readonly MethodInfo InternalCreate_Method = typeof(ObjectCreator).GetMethod("InternalCreate", BindingFlags.NonPublic | BindingFlags.Static);
        static Dictionary<Type, CtorMapping> ctorCache = new Dictionary<Type, CtorMapping>();
        static readonly object Mutex = new object();



        public static object Create(Type type)
        {
            if (type.IsNullable()) return null;
            return InternalCreate2(type);
        }

        private static object InternalCreate2(Type type)
        {
            if (!type.IsEnum)
            {
                var typeCode = Type.GetTypeCode(type);
                switch (typeCode)
                {
                    case TypeCode.Boolean: return false;
                    case TypeCode.Byte: return default(byte);
                    case TypeCode.Char: return default(char);
                    case TypeCode.DateTime: return default(DateTime);
                    case TypeCode.Decimal: return default(Decimal);
                    case TypeCode.Double: return default(double);
                    case TypeCode.Int16: return default(Int16);
                    case TypeCode.Int32: return default(Int32);
                    case TypeCode.Int64: return default(Int64);
                    case TypeCode.SByte: return default(SByte);
                    case TypeCode.Single: return default(Single);
                    case TypeCode.String: return default(string);
                    case TypeCode.UInt16: return default(UInt16);
                    case TypeCode.UInt32: return default(UInt32);
                    case TypeCode.UInt64: return default(UInt64);

                }
            }

            object[] args = null;
            CtorMapping mapping;
            if (!ctorCache.TryGetValue(type, out mapping))
            {
                var ctor = type.GetConstructor(Type.EmptyTypes);
                if (ctor != null)
                {
                    mapping = new CtorMapping { Constructor = ctor, Handler = ctor.GetCreator() };
                    lock (Mutex)
                        ctorCache[type] = mapping;
                }
            }

            if (mapping != null)
                return mapping.Handler(args);

            return InternalCreate_Method.MakeGenericMethod(type).Invoke(null, null);
        }

        static T InternalCreate<T>() where T : new()
        {
            return new T();
        }

        public static IList CreateList(Type listType, Type listElementType, int length)
        {
            if (listType.IsArray)
                return Array.CreateInstance(listElementType, length);
            Type type = listType.IsInterface || listType.IsAbstract ? typeof(List<>).MakeGenericType(listElementType) : listType;
            return Activator.CreateInstance(type) as IList;
        }

        public static object CreateDictionary(Type dictType, Type keyType, Type valueType)
        {
            var tmpDictType = dictType.IsInterface || dictType.IsAbstract
                ? Types.DictionaryOfT.MakeGenericType(keyType, valueType)
                : dictType;

            return Activator.CreateInstance(tmpDictType);
        }
    }

    class PrimitiveMapperFactory : MapperFactory
    {
        public PrimitiveMapperFactory()
        {
            order = 1;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return Converter.IsPrimitiveType(toType);
        }


        public override IMapper Create(Type fromType, Type toType)
        {
            return new PrimitiveMaper(fromType, toType);
        }
    }

    class TypeConverterFactory
    {
        public static TypeConverter GetTypeConverter(Type type)
        {
            return TypeDescriptor.GetConverter(type);

        }
    }

    class TypeConverterMapperFactory : MapperFactory
    {
        public TypeConverterMapperFactory()
        {
            order = int.MinValue;
        }


        public override bool IsMatch(Type fromType, Type toType)
        {
            var fromConverter = TypeConverterFactory.GetTypeConverter(fromType);

            var flag = fromConverter.CanConvertTo(toType);
            if (!flag)
            {
                return TypeConverterFactory.GetTypeConverter(toType).CanConvertFrom(fromType);
            }

            if (toType == typeof(Color))
            {
                if (fromType == Types.Int32)
                    return true;
            }
            return false;
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new TypeConverterMapper(fromType, toType);
        }


    }

    class DictionaryMapperFactory : MapperFactory
    {
        public DictionaryMapperFactory()
        {
            order = 6;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return fromType.IsDictionaryType() && toType.IsDictionaryType();
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new DictionaryMapper(fromType, toType);
        }
    }

    class ClassMapperFactory : MapperFactory
    {
        public ClassMapperFactory()
        {
            order = int.MaxValue;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return true;
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new ClassMapper(fromType, toType);
        }
    }

    class ClassToDictionaryMapperFactory : MapperFactory
    {
        public ClassToDictionaryMapperFactory()
        {
            order = 11;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return !Types.IDictionaryOfStringAndObject.IsAssignableFrom(fromType)
                &&
                (
                    Types.IDictionaryOfStringAndObject.IsAssignableFrom(toType)
                    || Types.IDictionaryOfStringAndString.IsAssignableFrom(toType)
                    || Types.NameValueCollection.IsAssignableFrom(toType)
                    || Types.StringDictionary.IsAssignableFrom(toType)
                    || typeof(Hashtable).IsAssignableFrom(toType)
                )
                ;
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new ClassToDictionaryMapper(fromType, toType);
        }
    }

    class DictionaryToClassMapperFactory : MapperFactory
    {

        public DictionaryToClassMapperFactory()
        {
            order = 16;
        }
        public override bool IsMatch(Type fromType, Type toType)
        {
            return (Types.IDictionaryOfStringAndObject.IsAssignableFrom(fromType)
                    || Types.IDictionaryOfStringAndString.IsAssignableFrom(fromType)
                    || Types.StringDictionary.IsAssignableFrom(fromType)
                    || Types.NameValueCollection.IsAssignableFrom(fromType))
                && (!toType.IsDictionaryType()
                       || !Types.StringDictionary.IsAssignableFrom(toType)
                    || !Types.NameValueCollection.IsAssignableFrom(toType))
                ;
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new DictonaryToClassMapper(fromType, toType);
        }
    }

    class CollectionMapperFactory : MapperFactory
    {
        public CollectionMapperFactory()
        {
            order = 41;
        }
        public override bool IsMatch(Type fromType, Type toType)
        {
            return (fromType.IsCollectionTypeExcludeStringAndDictionaryType()
                    || toType.IsCollectionTypeExcludeStringAndDictionaryType())
                //&& !Types.IDictionaryOfStringAndObject.IsAssignableFrom(fromType)
                //&& !Types.IDictionaryOfStringAndObject.IsAssignableFrom(toType)
                ;
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new CollectionMapper(fromType, toType);
        }
    }

    class ListSourceMapperFactory : MapperFactory
    {
        public ListSourceMapperFactory()
        {
            order = 36;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return (Types.IListSource.IsAssignableFrom(fromType)
                    && (
                        Types.IEnumerable.IsAssignableFrom(toType)
                        && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(toType)
                        || Types.IListSource.IsAssignableFrom(toType)))
                   ||
                   (Types.IListSource.IsAssignableFrom(toType)
                    && (
                        Types.IEnumerable.IsAssignableFrom(fromType)
                        && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(fromType)
                        || Types.IListSource.IsAssignableFrom(fromType)))

                ;
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new ListSourceMapper(fromType, toType);
        }
    }

    class DataReaderMapperFactory : MapperFactory
    {
        public DataReaderMapperFactory()
        {
            order = 21;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return Types.IDataReader.IsAssignableFrom(fromType);
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new DataReaderMapper(toType);
        }
    }

    class DataRowMapperFactory : MapperFactory
    {
        public DataRowMapperFactory()
        {
            order = 31;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return (Types.DataRow.IsAssignableFrom(fromType)
                    && !Types.IEnumerable.IsAssignableFrom(toType)
                    && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(toType))
                    ||
                    (Types.DataRow.IsAssignableFrom(toType)
                    && !Types.IEnumerable.IsAssignableFrom(fromType)
                    && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(fromType));
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new DataRowMapper(toType);
        }
    }

    class DataTableMapperFactory : MapperFactory
    {
        public DataTableMapperFactory()
        {
            order = 26;
        }

        public override bool IsMatch(Type fromType, Type toType)
        {
            return (Types.DataTable.IsAssignableFrom(fromType)
                    && Types.IEnumerable.IsAssignableFrom(toType)
                    && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(toType))
                    ||
                    (Types.DataTable.IsAssignableFrom(toType)
                    && Types.IEnumerable.IsAssignableFrom(fromType)
                    && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(fromType));
        }

        public override IMapper Create(Type fromType, Type toType)
        {
            return new DataTableMapper(fromType, toType);
        }
    }
}
