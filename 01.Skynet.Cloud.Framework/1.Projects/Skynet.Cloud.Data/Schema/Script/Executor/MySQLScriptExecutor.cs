using Microsoft.Extensions.Logging;
using System;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    class MySQLScriptExecutor : NonFileDatabaseScriptExecutor
    {
        public override void DeleteDatabase(DbConfiguration dbConfiguration)
        {
            var connectionStringBuilder = dbConfiguration.DbProviderFactory.CreateConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = dbConfiguration.ConnectionString;

            var dbName = dbConfiguration.DatabaseName;
            var dropDatabase = "DROP DATABASE " + dbName;

            dbConfiguration.sqlLogger().Log(LogLevel.Information,dropDatabase);

            using (var ctx = dbConfiguration.CreateDbContext())
            using (var cmd = ctx.Connection.CreateCommand())
            {
                connectionStringBuilder["Database"] = "mysql";
                ctx.Connection.ConnectionString = connectionStringBuilder.ConnectionString;

                cmd.CommandText = dropDatabase;
                ctx.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }

                try
                {
                    ctx.Connection.ChangeDatabase(dbName);
                    throw new ApplicationException("drop database failed.");
                }
                catch
                {
                }

            }
        }

        public override void CreateDatabase(DbConfiguration dbConfiguration, DatabaseScriptEntry script)
        {
            var connectionStringBuilder = dbConfiguration.DbProviderFactory.CreateConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = dbConfiguration.ConnectionString;

            var dbName = dbConfiguration.DatabaseName;
            var createDatabase = "CREATE DATABASE " + dbName;
            dbConfiguration.sqlLogger().Log(LogLevel.Information,createDatabase);
            connectionStringBuilder["Database"] = "mysql";

            var log = dbConfiguration.sqlLogger();

            using (var ctx = dbConfiguration.CreateDbContext())
            {
                var conn = ctx.Connection;
                conn.ConnectionString = connectionStringBuilder.ConnectionString;

                var cmd = conn.CreateCommand();
                cmd.CommandText = createDatabase;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.ChangeDatabase(dbName);
                try
                {
                    ctx.UsingTransaction(() =>
                    {
                        CreateTables(log, script, ctx);

                    });
                }
                catch
                {
                    conn.ChangeDatabase("mysql");
                    cmd.CommandText = string.Format("Drop DataBase {0}", dbName);
                    cmd.ExecuteNonQuery();
                    throw;
                }
            }
        }


    }
}
