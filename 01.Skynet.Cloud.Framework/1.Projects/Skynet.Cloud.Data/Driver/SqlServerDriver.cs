using System.Data;
using System.Data.SqlClient;

namespace UWay.Skynet.Cloud.Data.Driver
{
    class SqlServerDriver : AbstractDriver
    {
        protected override void ConvertDBTypeToNativeType(IDbDataParameter p, DBType dbType)
        {
            (p as SqlParameter).SqlDbType = (SqlDbType)(int)dbType;
        }

    }
}
