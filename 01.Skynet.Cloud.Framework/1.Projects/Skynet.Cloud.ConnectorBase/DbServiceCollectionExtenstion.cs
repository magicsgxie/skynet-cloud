﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.CloudFoundry.Connector;
using UWay.Skynet.Cloud.Data;
using System.Linq;
using Steeltoe.CloudFoundry.Connector.MySql;
using Steeltoe.CloudFoundry.Connector.SqlServer;
using Steeltoe.CloudFoundry.Connector.PostgreSql;
using Steeltoe.CloudFoundry.Connector.Oracle;
using UWay.Skynet.Cloud.Protocal;

namespace UWay.Skynet.Cloud.Extensions
{
    public  static partial class DbServiceCollectionExtenstion
    {
        private static readonly string SKYNET = "skynet";
        private static readonly string SKYNET_CLOUD = "cloud";
        private static readonly string SKYNET_CLOUD_SERVICE_DBNAME = "db";
        private static readonly string SKYNET_CLOUD_SERVICE_FROM = "from";
        private static readonly string SKYNET_CLOUD_SERVICE_INTERFACE = "interface";
        private static readonly string SKYNET_CLOUD_SERVICE_IMPL = "impl";
        private static readonly string SKYNET_CLOUD_SERVICE_ENTITY = "entity";

        /// <summary>
        /// 使用Oracle
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static IServiceCollection UseOracle(this IServiceCollection services, IConfiguration config, string serviceName = null)
        {
            ILoggerFactory loggerFactory = services.BuildAspectCoreServiceProvider().GetService<ILoggerFactory>();
            AddDataBaseInfo(config, "DB_Oracle_ConnStr", DbProviderNames.Oracle_Managed_ODP, serviceName, loggerFactory);
            return RegistryService(config, services);
        }


        /// <summary>
        /// 使用Mysql数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static IServiceCollection UseMysql(this IServiceCollection services, IConfiguration config, string serviceName = null)
        {
            ILoggerFactory loggerFactory = services.BuildAspectCoreServiceProvider().GetService<ILoggerFactory>();

            AddDataBaseInfo(config, "DB_MySql_ConnStr", DbProviderNames.MySQL, serviceName, loggerFactory);
            return RegistryService(config, services);
        }

        private static IServiceCollection RegistryService(IConfiguration config, IServiceCollection services)
        {
            var section = config.GetSection(SKYNET).GetSection(SKYNET_CLOUD);
            var serviceInterfaceAssmbly = section.GetValue<string>(SKYNET_CLOUD_SERVICE_INTERFACE);
            var serviceImplAssembly = section.GetValue<string>(SKYNET_CLOUD_SERVICE_IMPL);
            if (!serviceInterfaceAssmbly.IsNullOrEmpty() && !serviceImplAssembly.IsNullOrEmpty())
                services
                    .AddScopedAssembly(serviceInterfaceAssmbly, serviceImplAssembly);
            return services;
        }

        private static void AddDataBaseInfo(IConfiguration config, string containerName, string providerName, string serviceName = null, ILoggerFactory loggerFactory = null)
        {
            var section = config.GetSection(SKYNET).GetSection(SKYNET_CLOUD);
            var defaultContainer = section.GetValue<string>(SKYNET_CLOUD_SERVICE_DBNAME, "upms");
            var isFromDB = section.GetValue<bool>(SKYNET_CLOUD_SERVICE_FROM, false);
            var moduleAssmbly = section.GetValue<string>(SKYNET_CLOUD_SERVICE_ENTITY);
            var dbConnectionString = config.GetConnectionString(containerName);
            if (dbConnectionString.IsNullOrEmpty())
            {
                switch (providerName)
                {
                    case DbProviderNames.Oracle:
                        dbConnectionString = config.BuildeOracleConnectionString(serviceName);
                        break;
                    case DbProviderNames.MySQL:
                        dbConnectionString = config.BuildeMysqlConnectionString( serviceName);
                        break;
                    case DbProviderNames.SqlServer:
                        dbConnectionString = config.BuildeSqlServerConnectionString(serviceName);
                        break;
                    case DbProviderNames.PostgreSQL:
                        dbConnectionString = config.BuildePostgreConnectionString(serviceName);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(defaultContainer))
            {
                AddMainDb(containerName, dbConnectionString, providerName, moduleAssmbly, isFromDB, defaultContainer, loggerFactory);
            }
        }


        private static void AddMainDb(string container, string dbConnectionString, string providerName, string entityAssmbly, bool isFromDB, string defaultContainer, ILoggerFactory loggerFactory = null)
        {
            //var entityAssmbly = config.GetSection("appSettings").GetValue<string>("ENTITY_ASSMBLY");
            DbContextOption dbContextOption = new DbContextOption
            {
                Container = container,
                ConnectionString = dbConnectionString,
                ModuleAssemblyName = entityAssmbly,
                Provider = providerName,
                LogggerFactory = loggerFactory
            };

            if (isFromDB)
            {
                
                dbContextOption = dbContextOption.SingleDbContextOption(defaultContainer);

            }

            DbConfiguration.Configure(dbContextOption);

        }

        /// <summary>
        /// 使用SQL Server数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static IServiceCollection UseSqlServer(this IServiceCollection services, IConfiguration config, string serviceName = null)
        {
            ILoggerFactory loggerFactory = services.BuildAspectCoreServiceProvider().GetService<ILoggerFactory>();

            AddDataBaseInfo(config, "DB_SqlServer_ConnStr", DbProviderNames.SqlServer, serviceName, loggerFactory);

            return RegistryService(config, services);
        }

       

    }
}