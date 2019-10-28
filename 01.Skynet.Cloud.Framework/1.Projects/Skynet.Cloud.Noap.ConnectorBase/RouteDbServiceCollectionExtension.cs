using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UWay.Skynet.Cloud.Data;
using System.Linq;
using UWay.Skynet.Cloud.Protocal;
using System.Collections.Generic;
using System;
using System.IO;
using Skynet.Cloud.Noap;

namespace UWay.Skynet.Cloud.Extensions
{
    public  static partial class RouteDbServiceCollectionExtension
    {

        public static void UseMysqlRouteDb(this IConfiguration config, string containerName = "DB_Mysql_ConnStr", string defaultEntityAssmbly = null, string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildeMysqlConnectionString(serviceName);
            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.MySQL, defaultEntityAssmbly, loggerFactory);
        }

        public static void UseOracleRouteDb(this IConfiguration config, string containerName = "DB_Oracle_ConnStr", string defaultEntityAssmbly = null, string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildeOracleConnectionString(serviceName);
            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.Oracle_Managed_ODP, defaultEntityAssmbly, loggerFactory);
        }

        public static void UsePostgreRouteDb(this IConfiguration config, string containerName = "DB_Postgre_ConnStr", string defaultEntityAssmbly = null, string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildePostgreConnectionString(serviceName);

            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.PostgreSQL, defaultEntityAssmbly, loggerFactory);
        }



        public static void UseSqlServerRouteDb(this IConfiguration config, string containerName = "DB_SqlServer_ConnStr", string defaultEntityAssmbly = null, string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                dbConnectionString = config.BuildeSqlServerConnectionString(serviceName);

            }
            AddRouteDb(containerName, dbConnectionString, DbProviderNames.SqlServer, defaultEntityAssmbly, loggerFactory);
        }


        private static void AddRouteDb(string defaultContainer, string defaultDbConnectionString, string defaultProviderName, string defaultEntityAssmbly, ILoggerFactory loggerFactory = null)
        {
            var option = new DbContextOption
            {
                Container = defaultContainer,
                ConnectionString = defaultDbConnectionString,
                ModuleAssemblyName = defaultEntityAssmbly,
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

            var path = System.AppDomain.CurrentDomain.BaseDirectory ?? AppDomain.CurrentDomain.RelativeSearchPath;
            var files = Directory.EnumerateFiles(path, string.Format("*.{0}.{1}.mapping.xml", netType, dataBaseType));
            if (files.Any())
                return string.Format("*.{0}.{1}.mapping.xml", netType, dataBaseType);
            else
                return string.Empty;
        }
    
    }
}
