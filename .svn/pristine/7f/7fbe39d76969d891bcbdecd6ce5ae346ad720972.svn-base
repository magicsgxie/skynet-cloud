using System.Data;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Driver
{
    class SqlCeDriver : AbstractDriver
    {
        private static Setter SetSqlDbType;
        protected override void ConvertDBTypeToNativeType(IDbDataParameter p, DBType dbType)
        {
            if (SetSqlDbType == null)
            {
                SetSqlDbType = p.GetType().Module.GetType("System.Data.SqlServerCe.SqlCeParameter").GetProperty("SqlDbType", BindingFlags.Public | BindingFlags.Instance).GetSetter();

            }
            if (SetSqlDbType != null)
                SetSqlDbType(p, (SqlDbType)(int)dbType);
            else
                base.ConvertDBTypeToNativeType(p, dbType);
        }

    }
}
