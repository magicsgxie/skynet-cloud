using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Steeltoe.Security.DataProtection;
using Steeltoe.CloudFoundry.Connector.Redis;
using Microsoft.AspNetCore.DataProtection;
using UWay.Skynet.Cloud.Mvc;

namespace UWay.Skynet.Cloud.Extensions
{
    /// <summary>
    /// Service扩展类
    /// </summary>
    public static class MvcServiceCollectionExtenstion
    {
        private static readonly string SKYNET = "skynet";
        private static readonly string SKYNET_CLOUD = "cloud";
        /// <summary>
        /// 使用Redistribution
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection UserRedis(this IServiceCollection services, IConfiguration config)
        {
            services.AddRedisConnectionMultiplexer(config);
            services.AddDataProtection()
                .PersistKeysToRedis()
                .SetApplicationName("redis-keystore");
            // Use Redis cache on CloudFoundry to store session data
            services.AddDistributedRedisCache(config);
            return services;
        }

 


      
        /// <summary>
        /// 添加验证跳转
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Sys/User/Login");
                options.AccessDeniedPath = new PathString("/Error/NoAuth");
                options.LogoutPath = new PathString("/Sys/User/LogOut");
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
            });

            return services;
        }

        /// <summary>
        /// 添加用户自定义Mvc，主要再建立MvcWeb网站时使用
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs));
            services.AddMvc(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter))).AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写
            services.AddMemoryCache();
            services.AddSession();
            return services;
        }

    }
}
