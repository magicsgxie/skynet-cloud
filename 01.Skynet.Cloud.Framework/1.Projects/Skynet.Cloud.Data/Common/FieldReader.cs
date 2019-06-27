using System;
using System.Data;
using System.Reflection;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Mapping;

namespace UWay.Skynet.Cloud.Data.Common
{

    class FieldReader
    {
        Type[] typeCodes;
        private readonly IDataReader reader;

        internal FieldReader(IDataReader reader)
        {
            this.reader = reader;
            var fieldCount = reader.FieldCount;
            this.typeCodes = new Type[fieldCount];

            for (int i = 0; i < fieldCount; i++)
            {
                typeCodes[i] = reader.GetFieldType(i);
            }
        }

        public T GetValue<T>(int ordinal)
        {

            object value = reader.GetValue(ordinal);

            var toType = typeof(T);
            var fromType = value.GetType();
            if (fromType == toType)
                return (T)value;
            if (value == DBNull.Value)
                return default(T);
            if (toType == ULinq.BinaryType)
                return (T)ULinq.BinaryCtor(value as byte[]);


            return (T)Converter.Convert(value, typeof(T));
        }

        public bool IsDbNull(int ordinal)
        {
            return reader.IsDBNull(ordinal);
        }

        static readonly MethodInfo GetValueMethod = typeof(FieldReader).GetMethod("GetValue");

        internal static MethodInfo GetReaderMethod(Type type)
        {
            return GetValueMethod.MakeGenericMethod(type);
        }
    }
}
