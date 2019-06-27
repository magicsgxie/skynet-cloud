using System;
using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Data.Common
{
    using System.Diagnostics;
    using UWay.Skynet.Cloud.Reflection;

    [Serializable]
    [DebuggerDisplay("{DbType}(Length={Length},Required={Required},Precision={Precision},Scale={Scale})")]
    public sealed class SqlType
    {
        public bool Required { get; private set; }
        public DBType DbType { get; private set; }
        public int Length { get; private set; }
        public byte Precision { get; private set; }
        public byte Scale { get; private set; }

        internal static readonly SqlType Int32 = new SqlType(DBType.Int32);

        readonly int hashCode;

        SqlType(DBType dbType)
        {
            this.DbType = dbType;
            hashCode = dbType.GetHashCode();
        }
        SqlType(DBType dbType, int length)
        {
            this.DbType = dbType;
            this.Length = length;

            unchecked
            {
                hashCode = (dbType.GetHashCode() / 2) + (length.GetHashCode() / 2);
            }

        }
        SqlType(DBType dbType, byte precision, byte scale)
        {
            this.DbType = dbType;
            this.Precision = precision;
            this.Scale = scale;

            unchecked
            {
                hashCode = (dbType.GetHashCode() / 3) + (precision.GetHashCode() / 3) + (scale.GetHashCode() / 3);
            }
        }

        internal static readonly IDictionary<int, DBType> TypeMap = new Dictionary<int, DBType>();

        static SqlType()
        {
            TypeMap[Types.Boolean.TypeHandle.Value.GetHashCode()] = DBType.Boolean;
            TypeMap[Types.Byte.TypeHandle.Value.GetHashCode()] = DBType.Byte;
            TypeMap[Types.ByteArray.TypeHandle.Value.GetHashCode()] = DBType.Binary;
            TypeMap[Types.Char.TypeHandle.Value.GetHashCode()] = DBType.NChar;
            TypeMap[Types.DateTime.TypeHandle.Value.GetHashCode()] = DBType.DateTime;
            TypeMap[Types.Decimal.TypeHandle.Value.GetHashCode()] = DBType.Decimal;
            TypeMap[Types.Double.TypeHandle.Value.GetHashCode()] = DBType.Double;
            TypeMap[Types.Guid.TypeHandle.Value.GetHashCode()] = DBType.Guid;
            TypeMap[Types.Int16.TypeHandle.Value.GetHashCode()] = DBType.Int16;
            TypeMap[Types.Int32.TypeHandle.Value.GetHashCode()] = DBType.Int32;
            TypeMap[Types.Int64.TypeHandle.Value.GetHashCode()] = DBType.Int64;
            TypeMap[Types.Object.TypeHandle.Value.GetHashCode()] = DBType.Unkonw;
            TypeMap[Types.SByte.TypeHandle.Value.GetHashCode()] = DBType.Byte;
            TypeMap[Types.Single.TypeHandle.Value.GetHashCode()] = DBType.Single;
            TypeMap[Types.String.TypeHandle.Value.GetHashCode()] = DBType.NVarChar;
            TypeMap[Types.UInt16.TypeHandle.Value.GetHashCode()] = DBType.Int16;
            TypeMap[Types.UInt32.TypeHandle.Value.GetHashCode()] = DBType.Int32;
            TypeMap[Types.UInt64.TypeHandle.Value.GetHashCode()] = DBType.Int64;
            TypeMap[typeof(DateTimeOffset).TypeHandle.Value.GetHashCode()] = DBType.DateTime;
            //TypeMap[typeof(TimeSpan).TypeHandle.Value.GetHashCode()] = DbType.Time;

            TypeMap[typeof(Boolean?).TypeHandle.Value.GetHashCode()] = DBType.Boolean;
            TypeMap[typeof(byte?).TypeHandle.Value.GetHashCode()] = DBType.Byte;
            TypeMap[typeof(char?).TypeHandle.Value.GetHashCode()] = DBType.NVarChar;
            TypeMap[typeof(DateTime?).TypeHandle.Value.GetHashCode()] = DBType.DateTime;
            TypeMap[typeof(decimal?).TypeHandle.Value.GetHashCode()] = DBType.Decimal;
            TypeMap[typeof(double?).TypeHandle.Value.GetHashCode()] = DBType.Double;
            TypeMap[typeof(Guid?).TypeHandle.Value.GetHashCode()] = DBType.Guid;
            TypeMap[typeof(Int16?).TypeHandle.Value.GetHashCode()] = DBType.Int16;
            TypeMap[typeof(Int32?).TypeHandle.Value.GetHashCode()] = DBType.Int32;
            TypeMap[typeof(Int64?).TypeHandle.Value.GetHashCode()] = DBType.Int64;
            TypeMap[typeof(SByte?).TypeHandle.Value.GetHashCode()] = DBType.Byte;
            TypeMap[typeof(Single?).TypeHandle.Value.GetHashCode()] = DBType.Single;
            TypeMap[typeof(UInt16?).TypeHandle.Value.GetHashCode()] = DBType.Int16;
            TypeMap[typeof(UInt32?).TypeHandle.Value.GetHashCode()] = DBType.Int32;
            TypeMap[typeof(UInt64?).TypeHandle.Value.GetHashCode()] = DBType.Int64;
            TypeMap[typeof(DateTimeOffset?).TypeHandle.Value.GetHashCode()] = DBType.DateTime;
            //TypeMap[typeof(TimeSpan?).TypeHandle.Value.GetHashCode()] = DbType.Time;

            defaultTypes[Types.Boolean.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Boolean);
            defaultTypes[Types.Byte.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Byte);
            defaultTypes[Types.ByteArray.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Binary);
            defaultTypes[Types.Char.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.NChar, int.MaxValue);
            defaultTypes[Types.DateTime.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.DateTime);
            defaultTypes[Types.Decimal.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Decimal);
            defaultTypes[Types.Double.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Double);
            defaultTypes[Types.Guid.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Guid);
            defaultTypes[Types.Int16.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int16);
            defaultTypes[Types.Int32.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int32);
            defaultTypes[Types.Int64.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int64);
            defaultTypes[Types.Object.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Unkonw);
            defaultTypes[Types.SByte.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Byte);
            defaultTypes[Types.Single.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Single);
            defaultTypes[Types.String.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.NVarChar, int.MaxValue);
            defaultTypes[Types.UInt16.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int16);
            defaultTypes[Types.UInt32.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int32);
            defaultTypes[Types.UInt64.TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int64);
            defaultTypes[typeof(DateTimeOffset).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.DateTime);
            //defaultTypes[typeof(TimeSpan).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Time);

            defaultTypes[typeof(Boolean?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Boolean);
            defaultTypes[typeof(byte?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Byte);
            defaultTypes[typeof(char?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.NChar, int.MaxValue);
            defaultTypes[typeof(DateTime?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.DateTime);
            defaultTypes[typeof(decimal?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Decimal);
            defaultTypes[typeof(double?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Double);
            defaultTypes[typeof(Guid?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Guid);
            defaultTypes[typeof(Int16?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int16);
            defaultTypes[typeof(Int32?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int32);
            defaultTypes[typeof(Int64?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int64);
            defaultTypes[typeof(SByte?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Byte);
            defaultTypes[typeof(Single?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Single);
            defaultTypes[typeof(UInt16?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int16);
            defaultTypes[typeof(UInt32?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int32);
            defaultTypes[typeof(UInt64?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.Int64);
            defaultTypes[typeof(DateTimeOffset?).TypeHandle.Value.GetHashCode()] = new SqlType(DBType.DateTime);
            //defaultTypes[typeof(TimeSpan?).TypeHandle.Value.GetHashCode()] = new SqlType(DbType.Time);

        }



        private static readonly IDictionary<int, SqlType> defaultTypes =
           new Dictionary<int, SqlType>(128);

        private static readonly IDictionary<string, SqlType> SqlTypes =
            new Dictionary<string, SqlType>(new Dictionary<string, SqlType>(128), StringComparer.Ordinal);

        internal static SqlType Get(Type type)
        {
            var key = type.TypeHandle.Value.GetHashCode();
            SqlType sqlType;
            if (defaultTypes.TryGetValue(key, out sqlType))
                return sqlType;

            var strKey = key.ToString();
            if (SqlTypes.TryGetValue(strKey, out sqlType))
                return sqlType;

            bool isNotNull = type.IsValueType && !TypeHelper.IsNullable(type);
            type = TypeHelper.GetNonNullableType(type);
            if (type.IsEnum)
                type = Enum.GetUnderlyingType(type);

            DBType dbType = DBType.Unkonw;
            key = type.TypeHandle.Value.GetHashCode();
            //if (!TypeMap.TryGetValue(key, out dbType))
            //    throw new NotSupportedException("Not support '" + type.FullName + "' for sql type.");
            TypeMap.TryGetValue(key, out dbType);

            sqlType = new SqlType(dbType);
            //if (type == Types.Char)
            //    sqlType.Length = 1;
            sqlType.Required = isNotNull;

            lock (SqlTypes)
                SqlTypes[strKey] = sqlType;

            return sqlType;
        }

        internal static SqlType Get(Type memberType, ColumnAttribute col)
        {
            SqlType result = null;
            DBType dbType = col.DbType;

            string key = null;
            if (col.DbType == DBType.Unkonw)
            {
                var typeKey = memberType.TypeHandle.Value.GetHashCode();

                if (col.Length == 0
                && col.IsNullable
                && col.Precision == 0
                && col.Scale == 0)
                {
                    if (defaultTypes.TryGetValue(typeKey, out result))
                        return result;
                }

                var underlyingType = memberType;
                if (underlyingType.IsNullable())
                    underlyingType = Nullable.GetUnderlyingType(underlyingType);
                if (underlyingType.IsEnum)
                    underlyingType = Enum.GetUnderlyingType(underlyingType);
                //if (!TypeMap.TryGetValue(underlyingType.TypeHandle.Value.GetHashCode(), out dbType))
                //    throw new NotSupportedException("Not support '" + underlyingType.FullName + "' for field or property type.");
                typeKey = underlyingType.TypeHandle.Value.GetHashCode();
                if (TypeMap.TryGetValue(typeKey, out dbType))
                {
                    key = string.Concat(typeKey, col.Length, col.IsNullable, col.Precision, col.Scale);
                    if (SqlTypes.TryGetValue(key, out result))
                        return result;

                    defaultTypes.TryGetValue(typeKey, out result);
                }
            }
            else
            {
                key = string.Concat(col.DbType, col.Length, col.IsNullable, col.Precision, col.Scale);
                if (SqlTypes.TryGetValue(key, out result))
                    return result;
                result = new SqlType(col.DbType);
            }
            if (result != null)
                dbType = result.DbType;

            result = new SqlType(dbType, col.Precision, col.Scale) { Length = col.Length, Required = !col.IsNullable };

            lock (SqlTypes)
                SqlTypes.Add(key, result);
            return result;
        }

        internal static SqlType Get(DBType dbType, int length)
        {
            string key = GetKeyForLengthBased(dbType.ToString(), length);
            SqlType result;
            if (!SqlTypes.TryGetValue(key, out result))
            {
                result = new SqlType(dbType, length);
                lock (SqlTypes)
                    SqlTypes.Add(key, result);
            }
            return result;
        }


        private static string GetKeyForLengthBased(string name, int length)
        {
            return name + "(" + length + ")";
        }

        #region System.Object Members

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj == this || Equals(obj as SqlType);
        }

        public bool Equals(SqlType other)
        {
            if (other == null)
                return false;
            return hashCode == other.hashCode;
        }

        #endregion
    }
}