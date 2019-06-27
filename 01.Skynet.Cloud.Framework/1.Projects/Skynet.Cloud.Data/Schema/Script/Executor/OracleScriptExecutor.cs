

using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    class OracleScriptExecutor : NonFileDatabaseScriptExecutor
    {
        public override void DeleteDatabase(DbConfiguration dbConfiguration)
        {
            var connectionStringBuilder = dbConfiguration.DbProviderFactory.CreateConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = dbConfiguration.ConnectionString;

            var dbName = GetDatabaseName(connectionStringBuilder);
            var dbPassword = GetPWD(connectionStringBuilder);
            var deleteDatabase = "Drop USER " + dbName + " CASCADE";

            dbConfiguration.sqlLogger().Log(LogLevel.Information, deleteDatabase);
            connectionStringBuilder["USER ID"] = "SYSTEM";
            connectionStringBuilder["Password"] = dbPassword;

            var log = dbConfiguration.sqlLogger();
            using (var ctx = dbConfiguration.CreateDbContext())
            {
                var conn = ctx.Connection;
                conn.ConnectionString = connectionStringBuilder.ConnectionString;

                var cmd = conn.CreateCommand();
                cmd.CommandText = deleteDatabase;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        internal static string GetPWD(System.Data.Common.DbConnectionStringBuilder connectionStringBuilder)
        {
            object objDbName;
            string dbName = null;
            if (connectionStringBuilder.TryGetValue("Password", out objDbName))
                dbName = objDbName.ToString();
            else if (connectionStringBuilder.TryGetValue("PWD", out objDbName))
                dbName = objDbName.ToString();

            return dbName;
        }

        internal static string GetDatabaseName(System.Data.Common.DbConnectionStringBuilder connectionStringBuilder)
        {
            object objDbName;
            string dbName = null;
            if (connectionStringBuilder.TryGetValue("USER ID", out objDbName))
                dbName = objDbName.ToString();
            else if (connectionStringBuilder.TryGetValue("UID", out objDbName))
                dbName = objDbName.ToString();
            return dbName;
        }

        public override void CreateDatabase(DbConfiguration dbConfiguration, DatabaseScriptEntry script)
        {
            var connectionStringBuilder = dbConfiguration.DbProviderFactory.CreateConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = dbConfiguration.ConnectionString;

            var dbName = GetDatabaseName(connectionStringBuilder);
            var dbPassword = GetPWD(connectionStringBuilder);
            var createDatabase = "CREATE USER " + dbName + " IDENTIFIED BY " + dbPassword;

            var systemUser = "SYSTEM";
            dbConfiguration.sqlLogger().Log(LogLevel.Information, createDatabase);
            connectionStringBuilder["USER ID"] = systemUser;
            connectionStringBuilder["Password"] = dbPassword;

            var log = dbConfiguration.sqlLogger();
            using (var ctx = dbConfiguration.CreateDbContext())
            {
                var conn = ctx.Connection;
                conn.ConnectionString = connectionStringBuilder.ConnectionString;

                var cmd = conn.CreateCommand();
                cmd.CommandText = createDatabase;
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "GRANT CONNECT,RESOURCE  TO " + dbName;
                cmd.ExecuteNonQuery();
                //conn.ChangeDatabase(dbName);
                conn.Close();
            }
            connectionStringBuilder["USER ID"] = dbName;
            connectionStringBuilder["Password"] = dbPassword;

            using (var ctx = dbConfiguration.CreateDbContext())
            {
                var conn = ctx.Connection;
                conn.ConnectionString = connectionStringBuilder.ConnectionString;
                conn.Open();
                try
                {
                    ctx.UsingTransaction(() => CreateTables(log, script, ctx));
                }
                catch
                {
                    conn.Close();
                    connectionStringBuilder["USER ID"] = systemUser;
                    conn.ConnectionString = connectionStringBuilder.ConnectionString;
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "Drop USER " + dbName + " CASCADE";
                    cmd.ExecuteNonQuery();

                    throw;
                }
            }

        }






    }
}
