using System;
using System.Reflection;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    class SqlCeScriptExecutor : FileDatabaseScriptExecutor
    {
        protected override void OnCreateDatabase(DbConfiguration dbConfiguration, string dbName)
        {
            var type = dbConfiguration.DbProviderFactory.GetType().Module.GetType("System.Data.SqlServerCe.SqlCeEngine");

            var engine = Activator.CreateInstance(type, dbConfiguration.ConnectionString);
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod;
            try
            {
                type.InvokeMember("CreateDatabase", flags, null, engine, new object[0]);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

    }
}
