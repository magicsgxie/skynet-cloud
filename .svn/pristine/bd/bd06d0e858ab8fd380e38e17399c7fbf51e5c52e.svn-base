using System;
using System.Data;
using System.Data.Common;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Driver
{
    public class OledbDriver : AbstractDriver
    {
        protected override void InitializeParameter(IDbDataParameter p, NamedParameter parameter, object value)
        {
            p.ParameterName = parameter.Name;
            p.Value = value ?? DBNull.Value;
            var sqlType = parameter.SqlType;

            if (parameter.SqlType != null)
            {
                if (sqlType.Length > 0)
                    p.Size = sqlType.Length;
                if (sqlType.Precision > 0)
                    p.Precision = sqlType.Precision;
                if (sqlType.Scale > 0)
                    p.Scale = sqlType.Scale;
                if (sqlType.Required)
                    (p as DbParameter).IsNullable = false;

                switch (sqlType.DbType)
                {
                    case DBType.DateTime:
                        p.DbType = DbType.String;
                        if (p.Value != DBNull.Value && !object.Equals(null, p.Value))
                            p.Value = ((DateTime)p.Value).ToString("yyyy/MM/dd HH:mm:ss");
                        break;
                    case DBType.Int64:
                        p.DbType = DbType.Int32;
                        if (p.Value != DBNull.Value && !object.Equals(null, p.Value))
                            p.Value = System.Convert.ToInt32((long)p.Value);
                        break;
                }
            }
            //p.DbType = SqlType.ToDbType(parameter.sqlType.DbType);
            ConvertDBTypeToNativeType(p, sqlType.DbType);
        }

        protected override void InitializeParameter(object item, System.Data.Common.DbParameter p, System.Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.SByte:
                    p.DbType = DbType.Byte;
                    item = (byte)item;
                    break;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    p.DbType = DbType.Int32;
                    item = Convert.ToInt32(item);
                    break;
                case TypeCode.DateTime:
                    p.DbType = DbType.String;
                    item = ((DateTime)item).ToString("yyyy/MM/dd HH:mm:ss");
                    break;
            }
            p.Value = item;
        }

    }
}
