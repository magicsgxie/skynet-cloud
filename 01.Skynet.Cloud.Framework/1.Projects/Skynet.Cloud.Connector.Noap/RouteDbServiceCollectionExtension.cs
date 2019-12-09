using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UWay.Skynet.Cloud.Data;
using System.Linq;
using UWay.Skynet.Cloud.Protocal;
using System.Collections.Generic;
using System;
using System.IO;
using UWay.Skynet.Cloud.Noap;

namespace UWay.Skynet.Cloud.Extensions
{
    /// <summary>
    /// Db Service Extendsion
    /// </summary>
    public  static partial class RouteDbServiceCollectionExtension
    {
        /// <summary>
        /// 添加Mysql路由
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="containerName">连接名称</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="loggerFactory">日志工厂</param>
        public static void UseMysqlRouteDb(this IConfiguration config, string containerName = "DB_Mysql_ConnStr",  string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildeMysqlConnectionString(serviceName);
            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.MySQL,  loggerFactory);
        }
        /// <summary>
        /// 添加Oracle路由
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="containerName">连接名称</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="loggerFactory">日志工厂</param>
        public static void UseOracleRouteDb(this IConfiguration config, string containerName = "DB_Oracle_ConnStr",  string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildeOracleConnectionString(serviceName);
            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.Oracle_Managed_ODP,  loggerFactory);
        }
        /// <summary>
        /// 添加Postgre路由
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="containerName">连接名称</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="loggerFactory">日志工厂</param>
        public static void UsePostgreRouteDb(this IConfiguration config, string containerName = "DB_Postgre_ConnStr",  string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildePostgreConnectionString(serviceName);

            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.PostgreSQL,  loggerFactory);
        }

        /// <summary>
        /// 添加SqlServer路由
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="containerName">连接名称</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="loggerFactory">日志工厂</param>
        public static void UseSqlServerRouteDb(this IConfiguration config, string containerName = "DB_SqlServer_ConnStr",  string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildeSqlServerConnectionString(serviceName);

            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.SqlServer,  loggerFactory);
        }


        private static void AddRouteDb(string defaultContainer, string defaultDbConnectionString, string defaultProviderName, ILoggerFactory loggerFactory = null)
        {
            var option = new DbContextOption
            {
                Container = defaultContainer,
                ConnectionString = defaultDbConnectionString,
                Provider = defaultProviderName,
                LogggerFactory = loggerFactory
            };

            var list = option.DbContextOption(GetDefRouteDbContextOptions(option));
            if (list.Count() > 0)
            {
                DbConfiguration.Configure(list);
            }
        }

        private static IDictionary<string, DbContextOption> GetDefRouteDbContextOptions(DbContextOption dbContextOption)
        {
            IDictionary<string, DbContextOption> dbcs = new Dictionary<string, DbContextOption>();

            foreach (NetType item in Enum.GetValues(typeof(NetType)))
            {
                foreach (DataBaseType db in Enum.GetValues(typeof(DataBaseType)))
                {
                    string key = db == DataBaseType.Normal ? string.Format("{0}", (int)item) : string.Format("{0}_{1}", (int)item, (int)db);
                    if (!dbcs.ContainsKey(key))
                    {
                        string mappFile = GetMappingFile(item, db);
                        if (!mappFile.IsNullOrEmpty())
                        {
                            dbcs.Add(key, new DbContextOption()
                            {
                                Container = key,
                                Provider = dbContextOption.Provider,
                                ConnectionString = dbContextOption.ConnectionString,
                                MappingFile = GetMappingFile(item, db),
                                ModuleAssemblyName = dbContextOption.ModuleAssemblyName

                            });
                        }

                    }
                }

            }
            return dbcs;
        }

        private static string GetMappingFile(NetType netType, DataBaseType dataBaseType)
        {

            var path = AppDomain.CurrentDomain.BaseDirectory ?? AppDomain.CurrentDomain.RelativeSearchPath;
            var files = Directory.EnumerateFiles(path, string.Format("*.{0}.{1}.mapping.xml", netType, dataBaseType));
            if (files.Any())
                return string.Format("*.{0}.{1}.mapping.xml", netType, dataBaseType);
            else
                return string.Empty;
        }
    
    }
}
