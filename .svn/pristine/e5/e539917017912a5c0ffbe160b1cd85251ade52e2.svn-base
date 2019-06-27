using System;
using System.Data;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Driver
{
    class SQLiteDriver : AbstractDriver
    {
        protected override void InitializeParameter(IDbDataParameter p, NamedParameter parameter, object value)
        {

            if (parameter.SqlType != null)
            {

                switch (parameter.sqlType.DbType)
                {
                    case DBType.Guid:
                        if (value != null)
                            value = value.ToString();
                        break;
                    case DBType.DateTime:
                        if (value != null)
                        {
                            DateTime d = value.GetType().IsNullable() ? (value as DateTime?).Value : (DateTime)value;
                            value = d.ToString("s");
                        }
                        break;
                }
            }

            base.InitializeParameter(p, parameter, value);
        }

        protected override void InitializeParameter(object item, System.Data.Common.DbParameter p, Type type)
        {
            if (item is Guid)
                item = item.ToString();
            else if (item is DateTime)
                item = ((DateTime)item).ToString("s");
            base.InitializeParameter(item, p, type);
        }
    }
}
