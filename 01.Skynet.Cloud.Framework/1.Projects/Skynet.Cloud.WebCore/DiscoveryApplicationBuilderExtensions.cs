using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.Discovery;
using System;
using UWay.Skynet.Cloud.Nacos;

namespace UWay.Skynet.Cloud.WebCore
{
    public static class DiscoveryApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDiscoveryClient(this IApplicationBuilder app)
        {
            //var nacosClient = app.ApplicationServices.GetRequiredService<INacosNamingClient>();
            
            var service = app.ApplicationServices.GetRequiredService<IDiscoveryClient>();

            // make sure that the lifcycle object is created
            var lifecycle = app.ApplicationServices.GetService<IDiscoveryLifecycle>();
            return app;
        }
    }
}
