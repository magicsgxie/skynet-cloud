namespace UWay.Skynet.Cloud.Nacos
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;
    using System.Net.Http;
    using UWay.Skynet.Cloud.Nacos.Config;

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddNacos(this IServiceCollection services, Action<NacosClientConfiguration> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();
            services.Configure(configure);

            services.AddHttpClient(ConstValue.ClientName)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });
                
            services.TryAddSingleton<ILocalConfigInfoProcessor, MemoryLocalConfigInfoProcessor>();
            services.AddSingleton<INacosNamingClient, NacosNamingClient>();
            services.AddSingleton<INacosConfigClient, NacosConfigClient>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static IServiceCollection AddNacos(this IServiceCollection services, IConfiguration configuration, string sectionName = "nacos")
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<NacosClientConfiguration>(configuration.GetSection(sectionName));            

            services.AddHttpClient(ConstValue.ClientName)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });
                
            services.TryAddSingleton<ILocalConfigInfoProcessor, MemoryLocalConfigInfoProcessor>();
            services.AddSingleton<INacosConfigClient, NacosConfigClient>();
            services.AddSingleton<INacosNamingClient, NacosNamingClient>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <param name="httpClientAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddNacos(this IServiceCollection services, Action<NacosClientConfiguration> configure, Action<HttpClient> httpClientAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();
            services.Configure(configure);

            services.AddHttpClient(ConstValue.ClientName)
                .ConfigureHttpClient(httpClientAction)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });
                
            services.TryAddSingleton<ILocalConfigInfoProcessor, MemoryLocalConfigInfoProcessor>();
            services.AddSingleton<INacosConfigClient, NacosConfigClient>();
            services.AddSingleton<INacosNamingClient, NacosNamingClient>();

            return services;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="httpClientAction"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static IServiceCollection AddNacos(this IServiceCollection services, IConfiguration configuration, Action<HttpClient> httpClientAction, string sectionName = "UWay.Skynet.Cloud.Nacos")
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<NacosClientConfiguration>(configuration.GetSection(sectionName));   

            services.AddHttpClient(ConstValue.ClientName)
                .ConfigureHttpClient(httpClientAction)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });

            services.TryAddSingleton<ILocalConfigInfoProcessor, MemoryLocalConfigInfoProcessor>();
            services.AddSingleton<INacosConfigClient, NacosConfigClient>();
            services.AddSingleton<INacosNamingClient, NacosNamingClient>();


            return services;
        }
    }
}
