using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Reflection.Internal;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 类型加载器
    /// </summary>
    public interface IClassLoader
    {
        /// <summary>
        /// 得到或设置Assembly加载器
        /// </summary>
        IAssemblyLoader AssemblyLoader { get; set; }

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="assemblyName">assembly 名称</param>
        /// <param name="shortTypeName">类型名称</param>
        /// <returns>返回类型实例</returns>
        Type Load(string assemblyName, string shortTypeName);

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="fullTypeName">类型的全名</param>
        /// <returns>返回类型实例</returns>
        Type Load(string fullTypeName);
    }

    /// <summary>
    /// Assembly 加载器
    /// </summary>
    public interface IAssemblyLoader
    {
        /// <summary>
        /// 得到所有Assembly解析器
        /// </summary>
        List<IAssemblyResolver> Resolvers { get; }
        /// <summary>
        /// 得到所有已经加载的Assembly
        /// </summary>
        /// <returns></returns>
        Assembly[] GetAssemblies();

        /// <summary>
        /// 从assemblyName 字符串加载Assembly
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        Assembly Load(string assemblyName);
    }

    /// <summary>
    /// Assembly 解析器
    /// </summary>
    public interface IAssemblyResolver
    {
        /// <summary>
        /// 解析Assembly
        /// </summary>
        Assembly Resolve(AssemblyName aname);
    }

    /// <summary>
    /// 确省类型加载器
    /// </summary>
    [Serializable]
    public class SimpleClassLoader : IClassLoader
    {

        IAssemblyLoader assemblyLoader;
        /// <summary>
        /// 得到或设置Assembly加载器
        /// </summary>
        public IAssemblyLoader AssemblyLoader
        {
            get
            {
                if (assemblyLoader == null)
                    assemblyLoader = new DefaultAssemblyLoader();
                return assemblyLoader;
            }
            set
            {
                assemblyLoader = value;
            }
        }

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="fullTypeName">类型的全名</param>
        /// <returns>返回类型实例</returns>
        public Type Load(string fullTypeName)
        {
            Guard.NotNullOrEmpty(fullTypeName, "fullTypeName");
            //从当前域中获取类型
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                var type = Type.GetType(fullTypeName);
                if (type != null) return type;
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }

            return null;

        }



        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var asm = AssemblyLoader.Load(args.Name);
            return asm;
        }

        private static void ThrowIfTypeNameNotExists(String typeName)
        {
            String message = String.Format("The type name {0} could not be located", typeName);
            throw new Exception(message);
        }

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="assemblyName">assembly 名称</param>
        /// <param name="shortTypeName">类型名称</param>
        /// <returns>返回类型实例</returns>
        public Type Load(string assemblyName, string shortTypeName)
        {
            Guard.NotNullOrEmpty(assemblyName, "assemblyName");
            Guard.NotNullOrEmpty(shortTypeName, "shortTypeName");

            var asm = AssemblyLoader.Load(assemblyName);
            if (asm != null)
            {
                return asm.GetType(shortTypeName);
            }
            return null;
        }
    }

    /// <summary>
    /// 类型加载器
    /// </summary>
    public static class ClassLoader
    {
        static IClassLoader current;

        /// <summary>
        /// 得到或设置当前的ClassLoader
        /// </summary>
        public static IClassLoader Current
        {
            get
            {
                if (current == null)
                    current = new SimpleClassLoader();
                return current;
            }
            set
            {
                current = value;
            }
        }

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="assemblyName">assembly 名称</param>
        /// <param name="typeName">类型名称</param>
        /// <returns>返回类型实例</returns>
        public static Type Load(string assemblyName, string typeName)
        {
            Guard.NotNullOrEmpty(assemblyName, "assemblyName");
            Guard.NotNullOrEmpty(typeName, "typeName");

            var type = Current.Load(assemblyName, typeName);
            return type;
        }

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="fullTypeName">类型的全名</param>
        /// <returns>返回类型实例</returns>
        public static Type Load(string fullTypeName)
        {
            Guard.NotNullOrEmpty(fullTypeName, "fullTypeName");
            var type = Current.Load(fullTypeName);
            return type;
        }

    }



    /// <summary>
    /// Assembly加载器
    /// </summary>
    public class AssemblyLoader
    {
        /// <summary>
        /// 加载Assembly
        /// </summary>
        /// <param name="assembylyName"></param>
        /// <returns></returns>
        public static Assembly Load(string assembylyName)
        {
            Guard.NotNullOrEmpty(assembylyName, "assembylyName");
            return ClassLoader.Current.AssemblyLoader.Load(assembylyName);
        }
    }

    /// <summary>
    /// 缺省Assembly加载器
    /// </summary>
    [Serializable]
    public class DefaultAssemblyLoader : IAssemblyLoader
    {

        private Dictionary<AssemblyName, Assembly> cache = new Dictionary<AssemblyName, Assembly>();
        /// <summary>
        /// 得到Assembly解析器列表
        /// </summary>
        public List<IAssemblyResolver> Resolvers { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DefaultAssemblyLoader()
        {
            Resolvers = new List<IAssemblyResolver>();
            Resolvers.Add(new AppDomainAssemblyResolver());
            Resolvers.Add(new AppBaseAssemblyResolver() { OnAssemblyRef = (asmName, asm) => CacheAssembly(asmName, asm) });

            Resolvers.Add(new GacAssemblyResolver());

        }

        /// <summary>
        /// 加载Assembly
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public virtual Assembly Load(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException("assemblyName");

            try
            {
                var aname = new AssemblyName(assemblyName);
                var asm = OnLoad(aname);
                if (asm != null)
                    return GetOrCacheAssembly(asm);

                return null;
            }
            catch
            {
                Trace.WriteLine(string.Format("Error loading assembly '{0}'", assemblyName));
                return null;
            }
        }

        private Assembly OnLoad(AssemblyName anme)
        {
            Assembly asm = null;
            foreach (var resolver in Resolvers)
            {
                try
                {
                    asm = resolver.Resolve(anme);
                    if (asm != null)
                        break;
                }
                catch
                {
                }
                finally
                {
                }

            }

            return asm;
        }

        /// <summary>
        /// 得到所有已经加载的Assembly
        /// </summary>
        /// <returns></returns>
        public Assembly[] GetAssemblies()
        {
            return cache.Values.ToArray();
        }



        Assembly GetOrCacheAssembly(Assembly asm)
        {
            if (asm != null)
                if (!cache.Values.Contains(asm))
                    cache.Add(asm.GetName(), asm);
            return asm;
        }

        void CacheAssembly(AssemblyName name, Assembly asm)
        {
            if (asm != null)
                if (!cache.Values.Contains(asm))
                    cache.Add(name, asm);
        }
    }


    namespace Internal
    {
        class AssemblyNameComparer
        {

            static StringComparer nameCompare = StringComparer.OrdinalIgnoreCase;
            private AssemblyNameComparer() { }


            public static bool Equals(AssemblyName l, AssemblyName r)
            {
                return Equals(l, r, true);
            }

            public static bool Equals(AssemblyName l, AssemblyName r, bool includeVersion)
            {
                if (r == null)
                    return false;
                //名称比较
                if (!nameCompare.Equals(l.Name, r.Name))
                    return false;

                //公钥比较
                if ((l.Flags & AssemblyNameFlags.PublicKey) == AssemblyNameFlags.PublicKey)
                {
                    if (!((r.Flags & AssemblyNameFlags.PublicKey) == AssemblyNameFlags.PublicKey))
                        return false;

                    var keyToken = l.GetPublicKeyToken();
                    if (CompareKeys(keyToken, r.GetPublicKeyToken()))
                        return false;
                }


                //文化比较
                if (l.CultureInfo != null && !l.CultureInfo.Equals(r.CultureInfo))
                    return false;

                //版本比较
                includeVersion = includeVersion && l.Version != null;
                if (includeVersion && !l.Version.Equals(r.Version))
                    return false;

                return true;
            }


            private static bool CompareKeys(byte[] key1, byte[] key2)
            {
                if (key1 == null || key2 == null || key1.Length != key2.Length)
                    return false;

                for (int b = 0; b < key1.Length; b++)
                    if (key1[b] != key2[b])
                        return false;

                return true;
            }

        }


        class AssemblyNameResult
        {
            public string AssemblyFile;
            public AssemblyName AssemblyName;
        }

        class AssemblyMatcher
        {
            readonly AssemblyName SourceName;
            public AssemblyNameResult FullMatchedResult { get; private set; }
            private List<AssemblyNameResult> Results = new List<AssemblyNameResult>();
            bool IncludeVersion;
            public AssemblyMatcher(AssemblyName sourceName, bool includeVersion)
            {
                SourceName = sourceName;
                IncludeVersion = includeVersion;
            }


            public bool Match(string assemblyFile)
            {
#if !SILVERLIGHT
                var asmName = AssemblyName.GetAssemblyName(assemblyFile);
#else
                var asmName = new AssemblyName(assemblyFile);
#endif

                if (!AssemblyNameComparer.Equals(SourceName, asmName, false))
                    return false;


                var item = new AssemblyNameResult { AssemblyFile = assemblyFile, AssemblyName = asmName };
                if (SourceName.Version != null && SourceName.Version.Equals(asmName.Version))
                {
                    FullMatchedResult = item;
                    return true;
                }
                else if (!IncludeVersion)
                {
                    FullMatchedResult = item;
                    return true;
                }

                Results.Add(item);

                return false;
            }

            public string GetMatchAssemblyFile()
            {
                if (FullMatchedResult != null)
                    return FullMatchedResult.AssemblyFile;

                var map = Results.ToDictionary(p => p.AssemblyName.Version);
                var key = map.Keys.Max();
                return map[key].AssemblyFile;
            }
        }
    }

#if !SILVERLIGHT
    /// <summary>
    /// 应用程序域Assembly解析器
    /// </summary>
    public class AppDomainAssemblyResolver : IAssemblyResolver
    {
        /// <summary>
        /// 解析Assembly
        /// </summary>
        /// <param name="aname"></param>
        /// <returns></returns>
        public Assembly Resolve(AssemblyName aname)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies().FirstOrDefault(a => AssemblyNameComparer.Equals(aname, a.GetName()));
        }
    }
#endif

    /// <summary>
    /// 
    /// </summary>
    public class AssemblysAssemblyResolver : IAssemblyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Assembly> Assemblys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IncludeVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aname"></param>
        /// <returns></returns>
        public Assembly Resolve(AssemblyName aname)
        {
            if (Assemblys == null)
                return null;
            return Assemblys.FirstOrDefault(a => AssemblyNameComparer.Equals(aname, a.GetName(), IncludeVersion));
        }

    }

    /// <summary>
    /// Gac Assembly解析器
    /// </summary>
    public class GacAssemblyResolver : IAssemblyResolver
    {
        /// <summary>
        /// 解析Assembly
        /// </summary>
        /// <param name="anme"></param>
        /// <returns></returns>
        public Assembly Resolve(AssemblyName anme)
        {
            var asmName = Gac.GetAssemblyNames().FirstOrDefault(a => AssemblyNameComparer.Equals(anme, a));
            if (asmName != null)
                return Assembly.Load(asmName);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppBaseAssemblyResolver : FileDirectoryAssemblyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public AppBaseAssemblyResolver() : base(SkynetCloudEnvironment.ApplicationPhysicalPath, true) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeChildDirectory"></param>
        public AppBaseAssemblyResolver(bool includeChildDirectory) : base(SkynetCloudEnvironment.ApplicationPhysicalPath, includeChildDirectory) { }
    }

    /// <summary>
    /// 文件目录Assembly解析器
    /// </summary>
    public class FileDirectoryAssemblyResolver : IAssemblyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileDirectory { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Filter { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IncludeChildDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IncludeVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Action<AssemblyName, Assembly> OnAssemblyRef { get; set; }

        /// <summary>
        /// 创建文件目录解析器
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <param name="includeChildDirectory">是否包含子目录</param>
        public FileDirectoryAssemblyResolver(string path, bool includeChildDirectory)
            : this(path, null, includeChildDirectory)
        {
        }

        /// <summary>
        /// 创建文件目录解析器
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <param name="filter">文件过滤器</param>
        /// <param name="includeChildDirectory">是否包含子目录</param>
        public FileDirectoryAssemblyResolver(string path, string filter, bool includeChildDirectory)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);
            if (string.IsNullOrEmpty(filter))
                Filter = "*.dll";
            else
                Filter = filter;

            FileDirectory = path;
            IncludeChildDirectory = includeChildDirectory;
        }

        /// <summary>
        /// 解析Assembly
        /// </summary>
        /// <param name="aname"></param>
        /// <returns></returns>
        public Assembly Resolve(AssemblyName aname)
        {
            var results = new AssemblyMatcher(aname, IncludeVersion);
            Resolve(results, aname, FileDirectory);

            if (results.FullMatchedResult != null)
                return LoadFromFile(results.FullMatchedResult.AssemblyFile);


            return null;
        }


        private Assembly LoadFromFile(string assemblyFile)
        {
            var rawAssembly = File.ReadAllBytes(assemblyFile);
            var asm = Assembly.Load(rawAssembly); //Assembly.LoadFrom(assemblyFile);

            var handler = OnAssemblyRef;
            if (handler != null)
            {
                var refAssemblyNames = asm.GetReferencedAssemblies();
                foreach (var @ref in refAssemblyNames)
                {
                    var refAsm = Resolve(@ref);
                    if (refAsm != null)
                        handler(@ref, refAsm);
                }
            }

            return asm;
        }

        private void Resolve(AssemblyMatcher results, AssemblyName aname, string assemblyPath)
        {
            var assemblyFile = Path.Combine(assemblyPath, aname.Name + ".dll");
            if (File.Exists(assemblyFile))
            {
                if (results.Match(assemblyFile))
                    return;
            }

            var files = Directory.GetFiles(assemblyPath, Filter);
            if (files != null && files.Length > 0)
                foreach (string file in files)
                    if (results.Match(file))
                        return;

            if (!IncludeChildDirectory)
                return;

            var dirs = Directory.GetDirectories(assemblyPath);
            if (dirs != null && dirs.Length > 0)
                foreach (var subDirectory in dirs)
                {
                    Resolve(results, aname, subDirectory);
                }
        }

    }
}
