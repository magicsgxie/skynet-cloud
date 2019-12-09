using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.Common.HealthChecks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UWay.Skynet.Cloud.ConnectorBase.Relational;

namespace Steeltoe.CloudFoundry.Connector.Oracle
{
    /// <summary>
    /// Oracle数据提供配置扩展
    /// </summary>
    public static class OracleProviderServiceCollectionExtensions
    {


        /// <summary>
        /// Add Oracle and its IHealthContributor to a ServiceCollection
        /// </summary>
        /// <param name="services">Service collection to add to</param>
        /// <param name="config">App configuration</param>
        /// <param name="contextLifetime">Lifetime of the service to inject</param>
        /// <param name="logFactory">logging factory</param>
        /// <param name="builder">Microsoft HealthChecksBuilder</param>
        /// <returns>IServiceCollection for chaining</returns>
        /// <remarks>OracleConnection is retrievable as both OracleConnection and IDbConnection</remarks>
        public static IServiceCollection AddOracleConnection(this IServiceCollection services, IConfiguration config, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ILoggerFactory logFactory = null, IHealthChecksBuilder builder = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            OracleServiceInfo info = config.GetSingletonServiceInfo<OracleServiceInfo>();

            DoAdd(services, info, config, contextLifetime, builder);
            return services;
        }

        /// <summary>
        /// Add Oracle and its IHealthContributor to a ServiceCollection.
        /// </summary>
        /// <param name="services">Service collection to add to</param>
        /// <param name="config">App configuration</param>
        /// <param name="serviceName">cloud foundry service name binding</param>
        /// <param name="contextLifetime">Lifetime of the service to inject</param>
        /// <param name="logFactory">logging factory</param>
        /// <param name="builder">Microsoft HealthChecksBuilder</param>
        /// <returns>IServiceCollection for chaining</returns>
        /// <remarks>OracleConnection is retrievable as both OracleConnection and IDbConnection</remarks>
        public static IServiceCollection AddOracleConnection(this IServiceCollection services, IConfiguration config, string serviceName, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ILoggerFactory logFactory = null, IHealthChecksBuilder builder = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            OracleServiceInfo info = config.GetRequiredServiceInfo<OracleServiceInfo>(serviceName);

            DoAdd(services, info, config, contextLifetime, builder);
            return services;
        }

        private static void DoAdd(IServiceCollection services, OracleServiceInfo info, IConfiguration config, ServiceLifetime contextLifetime, IHealthChecksBuilder builder)
        {
            Type OracleConnection = ConnectorHelpers.FindType(OracleTypeLocator.Assemblies, OracleTypeLocator.ConnectionTypeNames);
            var OracleConfig = new OracleProviderConnectorOptions(config);
            var factory = new OracleProviderConnectorFactory(info, OracleConfig, OracleConnection);
            services.Add(new ServiceDescriptor(typeof(IDbConnection), factory.Create, contextLifetime));
            services.Add(new ServiceDescriptor(OracleConnection, factory.Create, contextLifetime));
            if (builder == null)
            {
                services.Add(new ServiceDescriptor(typeof(IHealthContributor), ctx => new SkynetCloudRelationalHealthContributor((IDbConnection)factory.Create(ctx), ctx.GetService<ILogger<SkynetCloudRelationalHealthContributor>>()), ServiceLifetime.Singleton));
            }
            else
            {
                builder.AddOracle(factory.CreateConnectionString());
            }
        }
    }
}
