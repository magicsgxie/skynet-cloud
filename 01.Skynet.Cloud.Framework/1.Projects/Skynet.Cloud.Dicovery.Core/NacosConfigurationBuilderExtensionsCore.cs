using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Configuration.NacosServerBase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UWay.Skynet.Cloud.Nacos;

namespace UWay.Skynet.Cloud.Dicovery.Core
{
    /// <summary>
    /// 配置服务端获取扩展
    /// </summary>
    public static class NacosConfigurationBuilderExtensionsCore
    {
        /// <summary>
        /// 添加配置服务器
        /// </summary>
        /// <param name="configurationBuilder">基础配置信息</param>
        /// <param name="environment">环境</param>
        /// <param name="clientFactory">Http客户端工厂</param>
        /// <param name="processor">配置处理器</param>
        /// <param name="logFactory">日志工厂</param>
        /// <returns></returns>
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

    }
}
