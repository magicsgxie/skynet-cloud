using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.MySql;
using Steeltoe.CloudFoundry.Connector.Oracle;
using Steeltoe.CloudFoundry.Connector.PostgreSql;
using Steeltoe.CloudFoundry.Connector.Relational;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.CloudFoundry.Connector.SqlServer;
using Steeltoe.Common.HealthChecks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UWay.Skynet.Cloud.ConnectorBase.Relational
{
    public class SkynetCloudRelationalHealthContributor : IHealthContributor
    {
        public static IHealthContributor GetMySqlContributor(IConfiguration configuration, ILogger<SkynetCloudRelationalHealthContributor> logger = null)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var info = configuration.GetSingletonServiceInfo<MySqlServiceInfo>();
            Type mySqlConnection = ConnectorHelpers.FindType(MySqlTypeLocator.Assemblies, MySqlTypeLocator.ConnectionTypeNames);
            var mySqlConfig = new MySqlProviderConnectorOptions(configuration);
            var factory = new MySqlProviderConnectorFactory(info, mySqlConfig, mySqlConnection);
            var connection = factory.Create(null) as IDbConnection;
            return new SkynetCloudRelationalHealthContributor(connection, logger);
        }

        public static IHealthContributor GetOracleContributor(IConfiguration configuration, ILogger<SkynetCloudRelationalHealthContributor> logger = null)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var info = configuration.GetSingletonServiceInfo<OracleServiceInfo>();
            Type oracleConnection = ConnectorHelpers.FindType(OracleTypeLocator.Assemblies, OracleTypeLocator.ConnectionTypeNames);
            var oracleConfig = new OracleProviderConnectorOptions(configuration);
            var factory = new OracleProviderConnectorFactory(info, oracleConfig, oracleConnection);
            var connection = factory.Create(null) as IDbConnection;
            return new SkynetCloudRelationalHealthContributor(connection, logger);
        }

        public static IHealthContributor GetPostgreSqlContributor(IConfiguration configuration, ILogger<SkynetCloudRelationalHealthContributor> logger = null)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var info = configuration.GetSingletonServiceInfo<PostgresServiceInfo>();
            Type postgresConnection = ConnectorHelpers.FindType(PostgreSqlTypeLocator.Assemblies, PostgreSqlTypeLocator.ConnectionTypeNames);
            var postgresConfig = new PostgresProviderConnectorOptions(configuration);
            var factory = new PostgresProviderConnectorFactory(info, postgresConfig, postgresConnection);
            var connection = factory.Create(null) as IDbConnection;
            return new SkynetCloudRelationalHealthContributor(connection, logger);
        }

        public static IHealthContributor GetSqlServerContributor(IConfiguration configuration, ILogger<SkynetCloudRelationalHealthContributor> logger = null)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var info = configuration.GetSingletonServiceInfo<SqlServerServiceInfo>();
            Type sqlServerConnection = SqlServerTypeLocator.SqlConnection;
            var sqlServerConfig = new SqlServerProviderConnectorOptions(configuration);
            var factory = new SqlServerProviderConnectorFactory(info, sqlServerConfig, sqlServerConnection);
            var connection = factory.Create(null) as IDbConnection;
            return new SkynetCloudRelationalHealthContributor(connection, logger);
        }

        public readonly IDbConnection _connection;
        private readonly ILogger<SkynetCloudRelationalHealthContributor> _logger;

        public SkynetCloudRelationalHealthContributor(IDbConnection connection, ILogger<SkynetCloudRelationalHealthContributor> logger = null)
        {
            _connection = connection;
            _logger = logger;
            Id = GetDbName(connection);
        }


        public string Id { get; }

        public HealthCheckResult Health()
        {
            _logger?.LogTrace("Checking {DbConnection} health", Id);
            var result = new HealthCheckResult();
            result.Details.Add("database", Id);
            try
            {
                _connection.Open();
                var cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT 1;";
                var qresult = cmd.ExecuteScalar();
                result.Details.Add("status", HealthStatus.UP.ToString());
                result.Status = HealthStatus.UP;
                _logger?.LogTrace("{DbConnection} up!", Id);
            }
            catch (Exception e)
            {
                _logger?.LogError("{DbConnection} down! {HealthCheckException}", Id, e.Message);
                result.Details.Add("error", e.GetType().Name + ": " + e.Message);
                result.Details.Add("status", HealthStatus.DOWN.ToString());
                result.Status = HealthStatus.DOWN;
                result.Description = $"{Id} health check failed";
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        private string GetDbName(IDbConnection connection)
        {
            var result = "db";
            switch (connection.GetType().Name)
            {
                case "NpgsqlConnection":
                    result = "PostgreSQL";
                    break;
                case "SqlConnection":
                    result = "SqlServer";
                    break;
                case "MySqlConnection":
                    result = "MySQL";
                    break;
                case "OracleConnection":
                    result = "Oracle";
                    break;
            }

            return string.Concat(result, "-", connection.Database);
        }


    }
}
