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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UWay.Skynet.Cloud.Authentication.CloudFoundry;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

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
        /// 添加验证
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddCloudFoundryJwtBearer(configuration);
            //services.Configure<JwtOption>(configuration.GetSection("JwtOptions"));
            //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            //将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtOption();
            configuration.Bind("JwtOptions", jwtSettings);
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                var key = jwtSettings.SecretKey;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    //Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,
                    //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    //ValidateIssuerSigningKey=true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime=true,
                    ////允许的服务器时间偏移量
                    ClockSkew=TimeSpan.FromSeconds(1)

                };
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
