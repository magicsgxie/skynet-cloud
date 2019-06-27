using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicAssemblyManager
    {
        /// <summary>
        /// 
        /// </summary>
        //public static void SaveAssembly()
        //{
        //    lock (typeof(DynamicAssemblyManager))
        //    {
        //        assemblyBuilder.(assemblyName.Name + ".dll");
        //    }
        //}

        private static AssemblyName assemblyName;
        private static AssemblyBuilder assemblyBuilder;
        internal static readonly ModuleBuilder moduleBuilder;
        internal static readonly Module Module;

        static DynamicAssemblyManager()
        {
            assemblyName = new AssemblyName(".Skynet.CloudDynamicAssembly");
            assemblyBuilder =  AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
             //AppDomain.CurrentDomain.DefineDynamicAssembly(
             //   assemblyName,
             //   AssemblyBuilderAccess.RunAndSave
             //   );

            moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            Module = assemblyBuilder.GetModules().FirstOrDefault();


        }

        private static string CorrectTypeName(string typeName)
        {
            if (typeName.Length >= 1042)
            {
                typeName = "type_" + typeName.Substring(0, 900) + Guid.NewGuid().ToString().Replace("-", "");
            }
            return typeName;
        }


        internal static TypeBuilder DefineType(string typeName, Type parent)
        {
            lock (typeof(DynamicAssemblyManager))
            {
                return moduleBuilder.DefineType(
                    CorrectTypeName(typeName),
                    TypeAttributes.Public,
                    parent,
                    null
                    );
            }
        }

    }
}
