using System;
using System.IO;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    abstract class FileDatabaseScriptExecutor : DatabaseScriptExecutor
    {
        public override bool DatabaseExists(DbConfiguration dbConfiguration)
        {
            var dbName = dbConfiguration.DatabaseName;
            return File.Exists(dbName);
        }

        public override void DeleteDatabase(DbConfiguration dbConfiguration)
        {
            var dbName = dbConfiguration.DatabaseName;
            File.Delete(dbName);
        }

        public override void CreateDatabase(DbConfiguration dbConfiguration, DatabaseScriptEntry script)
        {
            var dbName = dbConfiguration.DatabaseName;
            if (File.Exists(dbName))
                throw new InvalidOperationException(string.Format("Unable to create database because the database '{0}' already exists.", dbName));

            OnCreateDatabase(dbConfiguration, dbName);

            try
            {
                CreateTables(dbConfiguration, script);
            }
            catch
            {
                try
                {
                    DeleteDatabase(dbConfiguration);
                }
                catch { }
                throw;
            }
        }

        protected abstract void OnCreateDatabase(DbConfiguration dbConfiguration, string dbName);
    }
}
