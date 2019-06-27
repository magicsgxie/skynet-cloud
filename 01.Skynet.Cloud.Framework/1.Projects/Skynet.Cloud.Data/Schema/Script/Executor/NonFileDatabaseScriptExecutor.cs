
namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    abstract class NonFileDatabaseScriptExecutor : DatabaseScriptExecutor
    {
        public override bool DatabaseExists(DbConfiguration dbConfiguration)
        {
            try
            {
                using (var conn = dbConfiguration.DbProviderFactory.CreateConnection())
                {
                    conn.ConnectionString = dbConfiguration.ConnectionString;
                    conn.Open();
                    conn.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
