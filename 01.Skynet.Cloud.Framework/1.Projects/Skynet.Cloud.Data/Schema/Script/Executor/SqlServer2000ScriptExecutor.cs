
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    class SqlServer2000ScriptExecutor : SqlServerScriptExecutor
    {
        protected override void CreateTables(ILogger log, DatabaseScriptEntry script, IDbContext ctx)
        {
            script.SchemaScripts = null;
            base.CreateTables(log, script, ctx);
        }
    }
}
