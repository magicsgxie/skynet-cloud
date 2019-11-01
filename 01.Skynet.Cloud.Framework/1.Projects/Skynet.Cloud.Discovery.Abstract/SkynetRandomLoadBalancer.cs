using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using Steeltoe.Common.LoadBalancer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Discovery.Abstract
{
    public class SkynetRandomLoadBalancer : ILoadBalancer
    {
        private static readonly Random _random = new Random();
        private readonly IServiceInstanceProvider _serviceInstanceProvider;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLoadBalancer"/> class.
        /// Returns random service instances, with option caching of service lookups
        /// </summary>
        /// <param name="serviceInstanceProvider">Provider of service instance information</param>
        /// <param name="distributedCache">For caching service instance data</param>
        /// <param name="logger">For logging</param>
        public SkynetRandomLoadBalancer(IServiceInstanceProvider serviceInstanceProvider, IDistributedCache distributedCache = null, ILogger logger = null)
        {
            _serviceInstanceProvider = serviceInstanceProvider ?? throw new ArgumentNullException(nameof(serviceInstanceProvider));
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public virtual async Task<Uri> ResolveServiceInstanceAsync(Uri request)
        {
            _logger?.LogTrace("ResolveServiceInstance {serviceInstance}", request.Host);
            var availableServiceInstances = await _serviceInstanceProvider.GetSkynetCloudInstancesWithCacheAsync(request.Host, _distributedCache).ConfigureAwait(false);
            if (availableServiceInstances.Count > 0)
            {
                // load balancer instance selection predictability is not likely to be a security concern
                var resolvedUri = availableServiceInstances[_random.Next(availableServiceInstances.Count)].Uri;
                _logger?.LogDebug("Resolved {url} to {service}", request.Host, resolvedUri.Host);
                return new Uri(resolvedUri, request.PathAndQuery);
            }
            else
            {
                _logger?.LogWarning("Attempted to resolve service for {url} but found 0 instances", request.Host);
                return request;
            }
        }

        public virtual Task UpdateStatsAsync(Uri originalUri, Uri resolvedUri, TimeSpan responseTime, Exception exception)
        {
            return Task.CompletedTask;
        }
    }
}
