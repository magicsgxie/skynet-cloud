using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UWay.Skynet.Cloud.Nacos;

namespace UWay.Skynet.Cloud.WebCore
{
    /// <summary>
    /// Nacos Config Host扩展
    /// </summary>
    public static class ConfigNacosHostBuilderExtensions
    {
        /// <summary>
        /// Add Config Server and Cloud Foundry as application configuration sources. Add Config Server health check
        /// contributor to the service container.
        /// </summary>
        /// <param name="hostBuilder"><see cref="IWebHostBuilder"/></param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
        /// <returns><see cref="IWebHostBuilder"/> with config server and Cloud Foundry Config Provider attached</returns>
        public static IWebHostBuilder AddConfigNacos(this IWebHostBuilder hostBuilder, ILoggerFactory loggerFactory = null)
        {
            var clientFactory = GetHttpClientFactory();
            var processor = GetProcessor();
            hostBuilder.ConfigureServices((context, service) => {
                service.AddSingleton<ILocalConfigInfoProcessor>(processor);
                service.AddSingleton<IHttpClientFactory>(clientFactory);
            });
            
            hostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                
                config.AddConfigNacosServer(context.HostingEnvironment,clientFactory,processor, loggerFactory);
            });

            return hostBuilder;
        }



        /// <summary>
        /// 初始化配置处理类
        /// </summary>
        /// <returns></returns>

        private static ILocalConfigInfoProcessor GetProcessor()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<ILocalConfigInfoProcessor, MemoryLocalConfigInfoProcessor>();

            return serviceCollection.BuildServiceProvider().GetService<ILocalConfigInfoProcessor>();
        }

        /// <summary>
        /// 初始化Http访问工厂
        /// </summary>
        /// <returns></returns>
        public static IHttpClientFactory GetHttpClientFactory()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddHttpClient(ConstValue.ClientName)
                      .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });

            return collection.BuildServiceProvider().GetService<IHttpClientFactory>();
        }

    }
}
