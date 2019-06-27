using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Configuration.NacosServerBase;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.WebCore
{
    /// <summary>
    /// Extension methods for adding services related to Spring Cloud Config Server.
    /// </summary>
    public static class ConfigNacosServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureConfigServerClientOptions(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddOptions();

            var section = config.GetSection(ConfigNacosClientSettingsOptions.CONFIGURATION_PREFIX);
            services.Configure<ConfigNacosClientSettingsOptions>(section);

            return services;
        }

      

        [Obsolete("No longer necessary; IConfiguration added by default")]
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddOptions();

            services.TryAddSingleton<IConfiguration>(config);

            var root = config as IConfigurationRoot;
            if (root != null)
            {
                services.TryAddSingleton<IConfigurationRoot>(root);
            }

            return services;
        }
    }
}
