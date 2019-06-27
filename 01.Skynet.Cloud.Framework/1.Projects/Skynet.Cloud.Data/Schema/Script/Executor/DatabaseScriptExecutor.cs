
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    abstract class DatabaseScriptExecutor : IDatabaseScriptExecutor
    {

        public abstract bool DatabaseExists(DbConfiguration dbConfiguration);
        public abstract void DeleteDatabase(DbConfiguration dbConfiguration);
        public abstract void CreateDatabase(DbConfiguration dbConfiguration, DatabaseScriptEntry script);

        public virtual void CreateTables(DbConfiguration dbConfiguration, DatabaseScriptEntry script)
        {
            var log = dbConfiguration.sqlLogger();
            using (var ctx = dbConfiguration.CreateDbContext())
            {
                ctx.UsingTransaction(() =>
                {
                    CreateTables(log, script, ctx);
                });

            }
        }


        protected virtual void CreateTables(ILogger log, DatabaseScriptEntry script, IDbContext ctx)
        {
            var cmd = ctx.Connection.CreateCommand();

            if (script.SequenceScripts != null && script.SequenceScripts.Length > 0)
            {
                foreach (var item in script.SequenceScripts)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information, item);
                    cmd.ExecuteNonQuery();
                }
            }

            if (script.SchemaScripts != null && script.SchemaScripts.Length > 0)
            {
                foreach (var item in script.SchemaScripts)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information, item);
                    cmd.ExecuteNonQuery();
                }
            }

            if (script.TableScripts != null && script.TableScripts.Length > 0)
            {
                foreach (var item in script.TableScripts)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information, item);
                    cmd.ExecuteNonQuery();

                }
            }

            if (script.PKConstraintScripts != null && script.PKConstraintScripts.Length > 0)
            {
                foreach (var item in script.PKConstraintScripts)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information, item);
                    cmd.ExecuteNonQuery();
                }
            }

            if (script.FKConstraintScripts != null && script.FKConstraintScripts.Length > 0)
            {
                foreach (var item in script.FKConstraintScripts)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information, item);
                    cmd.ExecuteNonQuery();
                }
            }

            if (script.UniquleConstraintScripts != null && script.UniquleConstraintScripts.Length > 0)
            {
                foreach (var item in script.UniquleConstraintScripts)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information, item);
                    cmd.ExecuteNonQuery();
                }
            }

            if (script.CheckConstraintScript != null && script.CheckConstraintScript.Length > 0)
            {
                foreach (var item in script.CheckConstraintScript)
                {
                    cmd.CommandText = item;
                    log.Log(LogLevel.Information,item);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
