using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using Steeltoe.Common.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos;

namespace UWay.Skynet.Cloud.Dicovery.Core
{
    public static class DiscoveryApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDiscoveryClient(this IApplicationBuilder app)
        {
            //var nacosClient = app.ApplicationServices.GetRequiredService<INacosNamingClient>();
            
            var service = app.ApplicationServices.GetRequiredService<IDiscoveryClient>();
            var lifecycle = app.ApplicationServices.GetService<IDiscoveryLifecycle>();
            return app;
        }
    }
}
