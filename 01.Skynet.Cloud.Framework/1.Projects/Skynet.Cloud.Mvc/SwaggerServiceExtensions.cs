using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// swagger配置文件
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// 增加swagger文档生成
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            var version = configuration.GetSection("swagger").GetValue<string>("version");
            var apiName = configuration.GetSection("swagger").GetValue<string>("name");
            var description = configuration.GetSection("swagger").GetValue<string>("description");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new Info { Title = apiName + " " + version, Version = version });
                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = description,
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            return services;
        }

        /// <summary>
        /// app swagger use
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            var version = configuration.GetSection("swagger").GetValue<string>("version");
            var apiName = configuration.GetSection("swagger").GetValue<string>("name");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", apiName+" " + version);

                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
