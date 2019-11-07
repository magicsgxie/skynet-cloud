using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steeltoe.Configuration.NacosServerBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Dicovery.Core
{
    /// <summary>
    /// Extension methods for adding services related to Spring Cloud Config Server.
    /// </summary>
    public static class ConfigNacosServiceCollectionExtensions
    {
        /// <summary>
        /// 获取配置服务信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
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

      
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>

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
