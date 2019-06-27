using System;
using System.Data;
using System.Data.Common;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Driver
{
    class MySqlDriver : AbstractDriver
    {

        public override bool AllowsMultipleOpenReaders
        {
            get { return false; }
        }


        const string fmt = "yyyy-MM-dd HH:mm:ss";
        protected override void InitializeParameter(System.Data.IDbDataParameter p, Common.NamedParameter parameter, object value)
        {

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

                if (value != null && value is DateTime)
                {
                    var dt = (DateTime)value;
                    switch (sqlType.DbType)
                    {
                        case DBType.DateTime:
                        case DBType.NVarChar:
                            sqlType = SqlType.Get(DBType.NVarChar, 100);
                            value = dt.ToString(fmt);
                            break;

                    }
                }
            }
            p.ParameterName = parameter.Name;
            p.Value = value ?? DBNull.Value;
            ConvertDBTypeToNativeType(p, sqlType.DbType);

        }

        protected override void InitializeParameter(object item, System.Data.Common.DbParameter p, System.Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            if (item != null && item is DateTime)
            {
                item = ((DateTime)item).ToString(fmt);
                p.DbType = DbType.String;
            }
            p.Value = item;
        }

    }
}
