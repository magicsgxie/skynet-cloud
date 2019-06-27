using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Configuration.NacosServerBase;
using System;
using System.Net.Http;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Extensions.ConfigNacosCore
{
    public static class ConfigNacosConfigurationBuilderExtensionsCore
    {
        public static IConfigurationBuilder AddConfigNacosServer(this IConfigurationBuilder configurationBuilder, IHostingEnvironment environment, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {

            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            var settings = new ConfigNacosClientSettings()
            {
                Name = environment.ApplicationName,
                Environment = environment.EnvironmentName
            };

            return configurationBuilder.AddNacosConfigServer(settings, clientFactory, processor,logFactory);
        }


        //
        // 摘要:
        //     /// Adds a delegate for configuring the provided Microsoft.Extensions.Logging.ILoggingBuilder.
        //     This may be called multiple times. ///
        //
        // 参数:
        //   hostBuilder:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder to configure.
        //
        //   configureLogging:
        //     The delegate that configures the Microsoft.Extensions.Logging.ILoggingBuilder.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
        public static IWebHostBuilder ConfigureProcessor(this IWebHostBuilder hostBuilder, Action<ILocalConfigInfoProcessor> processor)
        {
            return hostBuilder.ConfigureServices(delegate (IServiceCollection collection)
            {
                collection.AddSingleton(processor);
            });
        }

        public static IWebHostBuilder ConfigureHttpClientFactory(this IWebHostBuilder hostBuilder, Action<IHttpClientFactory> clientFactory)
        {
            return hostBuilder.ConfigureServices(delegate (IServiceCollection collection)
            {
                collection.AddSingleton(clientFactory);
            });
        }
    }
}
