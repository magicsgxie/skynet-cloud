using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    class DbProviderFactoriesEx
    {
        public static DbProviderFactory GetFactory(string providerName)
        {
            if (providerName == null)
                throw new ArgumentNullException("providerName");
            DbProviderFactory dbFactory;
            switch (providerName)
            {
                case DbProviderNames.MySQL:
                case "MYSQL":
                case "MySQL":
                case "mysql":
                    dbFactory = new MySqlClientFactory();
                    break;
                case DbProviderNames.Oracle_Managed_ODP:
                case "ORACLE":
                case "Oracle":
                case "oracle":
                    dbFactory = new Oracle.ManagedDataAccess.Client.OracleClientFactory();
                    break;
                case DbProviderNames.SqlServer:
                case "SQLSERVER":
                case "SqlServer":
                case "sqlserver":
                    dbFactory = SqlClientFactory.Instance;
                    break;
                default:
                    dbFactory = null;
                    break;
            }
            return dbFactory;
        }
    }
}
