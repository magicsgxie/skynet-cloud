namespace UWay.Skynet.Cloud.Extensions
{
    using System;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Unicode;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.WebEncoders;
    using Microsoft.IdentityModel.Tokens;
    using UWay.Skynet.Cloud.IoC;
    using UWay.Skynet.Cloud.Mvc;

    /// <summary>
    /// Service扩展类.
    /// </summary>
    public static class MvcServiceCollectionExtenstion
    {
        /// <summary>
        /// 添加验证.
        /// </summary>
        /// <param name="services">服务列表.</param>
        /// <param name="configuration">配置信息.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // 由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            // 将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtOption();
            configuration.Bind("JwtOptions", jwtSettings);
            services.AddAuthentication(options =>
            {
                // 认证middleware配置
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

                    // Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,

                    // 这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                    // ValidateIssuerSigningKey=true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                    ////允许的服务器时间偏移量
                    ClockSkew = TimeSpan.FromSeconds(10),
                };
            });
            return services;
        }

        /// <summary>
        /// 添加用户自定义Mvc，主要再建立MvcWeb网站时使用.
        /// </summary>
        /// <param name="services">服务列表.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs));
            services.AddMvc(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter))).AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            // 修改默认首字母为大写
            services.AddMemoryCache();
            services.AddSession();
            return services;
        }

        /// <summary>
        /// IoC初始化.
        /// </summary>
        /// <param name="services">服务列表.</param>
        /// <returns>IServiceProvider.</returns>
        public static IServiceProvider InitIoC(this IServiceCollection services)
        {
            // 接入AspectCore.Injector
            return AspectCoreContainer.BuildServiceProvider(services);
        }
    }
}
