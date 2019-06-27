using System;
using System.Reflection;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Executor
{
    class AccessScriptExecutor : FileDatabaseScriptExecutor
    {
        protected override void OnCreateDatabase(DbConfiguration dbConfiguration, string dbName)
        {
            var asm = Assembly.Load("Interop.ADOX");
            Guard.NotNull(asm, "asm");

            var type = asm.GetType("ADOX.CatalogClass");
            Guard.NotNull(type, "type");

            //var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            try
            {
                var catalog = Activator.CreateInstance(type);

                var method = type.GetMethod("Create");
                Guard.NotNull(method, "method");
                method.Invoke(catalog, new object[] { dbConfiguration.ConnectionString });

                // type.InvokeMember("Create", flags, null, catalog, new object[] { dbConfiguration.ConnectionString });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }


    }
}
