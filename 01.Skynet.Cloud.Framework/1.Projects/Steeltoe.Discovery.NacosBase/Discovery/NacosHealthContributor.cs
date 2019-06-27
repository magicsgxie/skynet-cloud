using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Common.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    public class NacosHealthContributor : IHealthContributor
    {
        public string Id => "nacos";


        private readonly INacosNamingClient _client;
        private readonly ILogger<NacosHealthContributor> _logger;
        private readonly IOptionsMonitor<NacosDiscoveryOptions> _optionsMonitor;
        private readonly NacosDiscoveryOptions _options;

        internal NacosDiscoveryOptions Options
        {
            get
            {
                if (_optionsMonitor != null)
                {
                    return _optionsMonitor.CurrentValue;
                }

                return _options;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NacosHealthContributor"/> class.
        /// </summary>
        /// <param name="client">a Nacos client to use for health checks</param>
        /// <param name="options">configuration options</param>
        /// <param name="logger">optional logger</param>
        public NacosHealthContributor(INacosNamingClient client, NacosDiscoveryOptions options, ILogger<NacosHealthContributor> logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;
        }

        public HealthCheckResult Health()
        {
            var result = new HealthCheckResult();
            var leaderStatus = GetLeaderStatusAsync().Result;
            var services = GetCatalogServicesAsync().Result.Doms;
            result.Status = HealthStatus.UP;
            result.Details.Add("leader", leaderStatus);
            result.Details.Add("services", services);
            return result;
        }

        internal async Task<GetCurrentClusterLeaderResult> GetLeaderStatusAsync()
        {
            var result = await _client.GetCurrentClusterLeaderAsync().ConfigureAwait(false);
            return result;
        }

        internal async Task<ListServicesResult> GetCatalogServicesAsync()
        {
            
            return await _client.ListServicesAsync(new ListServicesRequest() { PageNo = 1, PageSize = int.MaxValue });
            
        }
    }
}
