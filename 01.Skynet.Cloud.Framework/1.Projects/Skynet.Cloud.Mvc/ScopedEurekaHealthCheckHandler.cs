using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery.Eureka.AppInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UWay.Skynet.Cloud.Mvc
{
    public class ScopedEurekaHealthCheckHandler : EurekaHealthCheckHandler
    {
        internal IServiceScopeFactory _scopeFactory;

        public ScopedEurekaHealthCheckHandler(IServiceScopeFactory scopeFactory, ILogger<ScopedEurekaHealthCheckHandler> logger = null)
            : base(logger)
        {
            _scopeFactory = scopeFactory;
        }

        public override InstanceStatus GetStatus(InstanceStatus currentStatus)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                _contributors = scope.ServiceProvider.GetServices<IHealthContributor>().ToList();
                var result = base.GetStatus(currentStatus);
                _contributors = null;
                return result;
            }
        }
    }
}
