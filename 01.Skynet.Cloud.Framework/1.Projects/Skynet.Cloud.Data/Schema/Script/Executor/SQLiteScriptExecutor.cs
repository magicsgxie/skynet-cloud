using System.Reflection;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    class SQLiteScriptExecutor : FileDatabaseScriptExecutor
    {
        protected override void OnCreateDatabase(DbConfiguration dbConfiguration, string dbName)
        {
            var type = dbConfiguration.DbProviderFactory.GetType().Module.GetType("System.Data.SQLite.SQLiteConnection");

            var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            try
            {
                type.InvokeMember("CreateFile", flags, null, null, new object[] { dbName });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
