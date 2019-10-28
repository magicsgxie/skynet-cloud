using Microsoft.Extensions.Configuration;
using Steeltoe.CloudFoundry.Connector.MySql;
using Steeltoe.CloudFoundry.Connector.Oracle;
using Steeltoe.CloudFoundry.Connector.PostgreSql;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.CloudFoundry.Connector.SqlServer;
using Steeltoe.CloudFoundry.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Protocal;

namespace UWay.Skynet.Cloud.Extensions
{
    public static class ConnectionStringBuildExtension
    {
        public static string BuildeOracleConnectionString(this IConfiguration config, string serviceName = null)
        {
            OracleServiceInfo info = string.IsNullOrEmpty(serviceName)
                  ? config.GetSingletonServiceInfo<OracleServiceInfo>()
                  : config.GetRequiredServiceInfo<OracleServiceInfo>(serviceName);
            OracleProviderConnectorOptions oracleProviderConnectorOptions = new OracleProviderConnectorOptions(config);
            OracleProviderConnectorFactory factory = new OracleProviderConnectorFactory(info, oracleProviderConnectorOptions, null);
            return factory.CreateConnectionString();
        }

        public static string BuildeMysqlConnectionString(this IConfiguration config, string serviceName = null)
        {
            MySqlServiceInfo info = string.IsNullOrEmpty(serviceName)
                    ? config.GetSingletonServiceInfo<MySqlServiceInfo>()
                    : config.GetRequiredServiceInfo<MySqlServiceInfo>(serviceName);
            MySqlProviderConnectorOptions mySqlConfig = new MySqlProviderConnectorOptions(config);
            MySqlProviderConnectorFactory factory = new MySqlProviderConnectorFactory(info, mySqlConfig, null);
            return factory.CreateConnectionString();
        }

        public static string BuildePostgreConnectionString(this IConfiguration config, string serviceName = null)
        {
            PostgresServiceInfo info = string.IsNullOrEmpty(serviceName)
                    ? config.GetSingletonServiceInfo<PostgresServiceInfo>()
                    : config.GetRequiredServiceInfo<PostgresServiceInfo>(serviceName);

            PostgresProviderConnectorOptions mySqlConfig = new PostgresProviderConnectorOptions(config);
            PostgresProviderConnectorFactory factory = new PostgresProviderConnectorFactory(info, mySqlConfig, null);
            return factory.CreateConnectionString();
        }

        public static string BuildeSqlServerConnectionString(this IConfiguration config, string serviceName = null)
        {
            SqlServerServiceInfo info = string.IsNullOrEmpty(serviceName)
           ? config.GetSingletonServiceInfo<SqlServerServiceInfo>()
           : config.GetRequiredServiceInfo<SqlServerServiceInfo>(serviceName);

            SqlServerProviderConnectorOptions sqlServerConfig = new SqlServerProviderConnectorOptions(config);

            SqlServerProviderConnectorFactory factory = new SqlServerProviderConnectorFactory(info, sqlServerConfig, null);
            var dbConnectionString = factory.CreateConnectionString();
            return factory.CreateConnectionString();
        }

        public static IEnumerable<DbContextOption> DbContextOption(this DbContextOption dbContextOption, IDictionary<string, DbContextOption> dbcs)
        {
            if (dbcs.Count > 0)
            {
                using (var connContext = new ProtocolDbContext(dbContextOption))
                {
                    var conn = connContext.Set<ProtocolInfo>().Where(p => p.ProtocalType == ProtocalType.DB && dbcs.Keys.Contains(p.ContainerName)).ToList();
                    List<int> cfgIds = conn.Select(p => p.CfgID).ToList();
                    var details = connContext.Set<ProtocolCfgInfo>().Where(p => cfgIds.Contains(p.CfgID)).ToList();
                    if (conn != null && conn.Count > 0)
                    {
                        foreach (var item in conn)
                        {
                            if (details != null && details.Count > 0)
                            {
                                var connDetail = details.FirstOrDefault(p => p.CfgID == item.CfgID);
                                if (connDetail != null)
                                {

                                    if (connDetail.ProviderName == DbProviderNames.Oracle && !string.IsNullOrWhiteSpace(connDetail.ServerName))
                                    {
                                        connDetail.Driver = DbProviderNames.Oracle_Managed_ODP;
                                        item.DataBaseName = connDetail.ServerName;
                                    }
                                    var connectStrings = dbContextOption.ConnectionString;
                                    if (connDetail.ProviderName == DbProviderNames.Oracle)
                                        connectStrings = BuildOracleClientConnectionString(item.DataBaseName, connDetail.DesUserID, connDetail.DesPassword, item.IsConnPool, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                                    else if (connDetail.ProviderName == DbProviderNames.Oracle_Managed_ODP)
                                        connectStrings = BuildOracleManagedODPConnectionString(connDetail.Url, connDetail.Port, item.DataBaseName, connDetail.DesUserID, connDetail.DesPassword, item.IsConnPool, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                                    else if (connDetail.ProviderName == DbProviderNames.Oracle_ODP)
                                        connectStrings = BuildOracleODPConnectionString(connDetail.Url, connDetail.Port, item.DataBaseName, connDetail.DesUserID, connDetail.DesPassword, item.IsConnPool, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                                    else if (connDetail.ProviderName == DbProviderNames.SqlServer)
                                        connectStrings = BuildSqlServerConnectionString(connDetail.Url, connDetail.DesUserID, connDetail.DesPassword, item.DataBaseName, connDetail.Port);
                                    else if (connDetail.ProviderName == DbProviderNames.MySQL)
                                        connectStrings = BuildMySqlConnectionString(connDetail.Url, connDetail.DesUserID, connDetail.DesPassword, item.DataBaseName, connDetail.Port, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                                    if (dbcs.ContainsKey(item.ContainerName))
                                    {
                                        dbcs[item.ContainerName].ConnectionString = connectStrings;
                                        dbcs[item.ContainerName].Provider = connDetail.ProviderName;
                                    }
                                    else
                                    {
                                        var key = dbcs.Keys.FirstOrDefault(p => p.StartsWith(item.ContainerName));
                                        if (!string.IsNullOrEmpty(key))
                                        {
                                            dbcs[key].ConnectionString = connectStrings;
                                            dbcs[key].Provider = connDetail.ProviderName;
                                            dbcs[key].LogggerFactory = dbContextOption.LogggerFactory;
                                        }
                                    }


                                }
                            }
                        }


                    }
                }
            }


            return dbcs.Values;
        }

        public static DbContextOption SingleDbContextOption(this DbContextOption dbContextOption, string defaultContainer)
        {
            using (var connContext = new ProtocolDbContext(dbContextOption))
            {
                var conn = connContext.Set<ProtocolInfo>().Where(p => p.ProtocalType == ProtocalType.DB && p.ContainerName.Equals(defaultContainer)).ToList();

                if (conn != null && conn.Count > 0)
                {
                    var item = conn.FirstOrDefault();
                    var connDetail = connContext.Set<ProtocolCfgInfo>().Get(item.CfgID);
                    if (connDetail != null)
                    {

                        if (connDetail.ProviderName == DbProviderNames.Oracle && !string.IsNullOrWhiteSpace(connDetail.ServerName))
                        {
                            connDetail.Driver = DbProviderNames.Oracle_Managed_ODP;
                            item.DataBaseName = connDetail.ServerName;
                        }
                        var connectStrings = dbContextOption.ConnectionString;
                        if (connDetail.ProviderName == DbProviderNames.Oracle)
                            connectStrings = BuildOracleClientConnectionString(item.DataBaseName, connDetail.DesUserID, connDetail.DesPassword, item.IsConnPool, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                        else if (connDetail.ProviderName == DbProviderNames.Oracle_Managed_ODP)
                            connectStrings = BuildOracleManagedODPConnectionString(connDetail.Url, connDetail.Port, item.DataBaseName, connDetail.DesUserID, connDetail.DesPassword, item.IsConnPool, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                        else if (connDetail.ProviderName == DbProviderNames.Oracle_ODP)
                            connectStrings = BuildOracleODPConnectionString(connDetail.Url, connDetail.Port, item.DataBaseName, connDetail.DesUserID, connDetail.DesPassword, item.IsConnPool, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                        else if (connDetail.ProviderName == DbProviderNames.SqlServer)
                            connectStrings = BuildSqlServerConnectionString(connDetail.Url, connDetail.DesUserID, connDetail.DesPassword, item.DataBaseName, connDetail.Port);
                        else if (connDetail.ProviderName == DbProviderNames.MySQL)
                            connectStrings = BuildMySqlConnectionString(connDetail.Url, connDetail.DesUserID, connDetail.DesPassword, item.DataBaseName, connDetail.Port, connDetail.CONNET_POOL_MAXACTIVE, connDetail.CONNET_POOL_MAXIDLE);
                        return new DbContextOption()
                        {
                            Container = "upms",
                            Provider = connectStrings,
                            ConnectionString = connDetail.ProviderName,
                            ModuleAssemblyName = dbContextOption.ModuleAssemblyName,
                            MappingFile = dbContextOption.MappingFile,
                            LogggerFactory = dbContextOption.LogggerFactory
                        };

                    }
                }

                return new DbContextOption()
                {
                    Container = defaultContainer,
                    Provider = dbContextOption.Provider,
                    ConnectionString = dbContextOption.ConnectionString,
                    ModuleAssemblyName = dbContextOption.ModuleAssemblyName,
                    MappingFile = dbContextOption.MappingFile,
                    LogggerFactory = dbContextOption.LogggerFactory
                };

            }
        }

        #region
        private static string BuildOracleClientConnectionString(string datasource, string uid, string pwd, bool IsConnPool, int CONNET_POOL_MAXACTIVE, int CONNET_POOL_MAXIDLE)
        {
            return string.Format("data source={0};user id={1};password={2};Pooling={3};Max Pool Size={4};Min Pool Size={5};", datasource, uid, pwd, IsConnPool, CONNET_POOL_MAXIDLE, CONNET_POOL_MAXACTIVE);
        }

        private static string BuildOracleManagedODPConnectionString(string ip, int port, string datasource, string uid, string pwd, bool IsConnPool, int CONNET_POOL_MAXACTIVE, int CONNET_POOL_MAXIDLE)
        {
            return string.Format("data source={0}:{1}/{2};user id={3};password={4};Pooling={5};Max Pool Size={6};Min Pool Size={7};", ip, port, datasource, uid, pwd, IsConnPool, CONNET_POOL_MAXACTIVE, CONNET_POOL_MAXIDLE);
        }

        private static string BuildOracleODPConnectionString(string ip, int port, string datasource, string uid, string pwd, bool IsConnPool, int CONNET_POOL_MAXACTIVE, int CONNET_POOL_MAXIDLE)
        {
            return string.Format(@"(DESCRIPTION =(ADDRESS_LIST =
                                    (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))
                                    )
                                    (CONNECT_DATA =
                                    (SERVICE_NAME = {2})
                                   )
                                    );Persist Security Info=True;User ID={3};Password={4};;Pooling={5};Max Pool Size={6};Min Pool Size={7};", ip,
                                    port,
                                     datasource,
                                     uid, pwd, IsConnPool, CONNET_POOL_MAXIDLE, CONNET_POOL_MAXACTIVE);
        }
        #endregion

        #region
        private static string BuildMySqlConnectionString(string url, string uid, string pwd, string database, int port, int CONNET_POOL_MAXACTIVE, int CONNET_POOL_MAXIDLE)
        {
            return string.Format("server = {0}; User Id = {1}; password = {2}; database = {3}; port = {4}; Charset = utf8; Persist Security Info = True",
                url, uid, pwd, database, port);
        }

        private static string BuildSqlServerConnectionString(string url, string uid, string pwd, string database, int port)
        {
            return string.Format("Data Source = {0};Port={4};Initial Catalog = {1};User ID = {2}; Password={3};Integrated Security =false",
                url, database, uid, pwd, port);
        }


        private static string BuildSqlServerConnectionString(string url, string uid, string pwd, string database)
        {
            return string.Format("Data Source = {0};Initial Catalog = {1};User ID = {2}; Password={3};Integrated Security =false",
                url, database, uid, pwd);
        }
        #endregion

    }
}
