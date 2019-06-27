using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    public static class ServiceCollectionExtesion
    {

        public static IServiceCollection AddMySwagger(this IServiceCollection services, IConfiguration config = null)
        {

            services.AddSwaggerDocument(p =>
            {
                p.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Test API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "magic.s.g.xie",
                        Email = "xiesg@uway.cn",
                        Url = "https://uway.cn"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under UWay",
                        Url = "https://uway.cn/license"
                    };
                };
            });
            return services;
        }

    }
}
