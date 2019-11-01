using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    /// <summary>
    /// The default scheduler used to issue TTL requests to the Consul server
    /// </summary>
    public class NacosTtlScheduler : INacosScheduler
    {
        internal readonly ConcurrentDictionary<string, Timer> _serviceHeartbeats = new ConcurrentDictionary<string, Timer>(StringComparer.OrdinalIgnoreCase);

        internal readonly INacosNamingClient _client;

        private readonly IOptionsMonitor<NacosDiscoveryOptions> _optionsMonitor;
        private readonly NacosDiscoveryOptions _options;
        private readonly ILogger<NacosTtlScheduler> _logger;

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

        internal NacosHeartbeatOptions HeartbeatOptions
        {
            get
            {
                return Options.Heartbeat;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NacosTtlScheduler"/> class.
        /// </summary>
        /// <param name="optionsMonitor">configuration options</param>
        /// <param name="client">the Consul client</param>
        /// <param name="logger">optional logger</param>
        public NacosTtlScheduler(IOptionsMonitor<NacosDiscoveryOptions> optionsMonitor, INacosNamingClient client, ILogger<NacosTtlScheduler> logger = null)
        {
            _optionsMonitor = optionsMonitor;
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NacosTtlScheduler"/> class.
        /// </summary>
        /// <param name="options">configuration options</param>
        /// <param name="client">the Consul client</param>
        /// <param name="logger">optional logger</param>
        public NacosTtlScheduler(NacosDiscoveryOptions options, INacosNamingClient client, ILogger<NacosTtlScheduler> logger = null)
        {
            _options = options;
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Add(string instanceId)
        {
            if (string.IsNullOrWhiteSpace(instanceId))
            {
                throw new ArgumentException(nameof(instanceId));
            }

            _logger?.LogDebug("Add {instanceId}", instanceId);

            if (HeartbeatOptions != null)
            {
                var interval = HeartbeatOptions.ComputeHearbeatInterval();

                var checkId = instanceId;
                if (!checkId.StartsWith("service:"))
                {
                    checkId = "service:" + checkId;
                }

                var timer = new Timer(async s => { await PassTtl(s.ToString()).ConfigureAwait(false); }, checkId, TimeSpan.Zero, interval);
                _serviceHeartbeats.AddOrUpdate(instanceId, timer, (key, oldTimer) =>
                {
                    oldTimer.Dispose();
                    return timer;
                });
            }
        }

        /// <inheritdoc/>
        public void Remove(string instanceId)
        {
            if (string.IsNullOrWhiteSpace(instanceId))
            {
                throw new ArgumentException(nameof(instanceId));
            }

            _logger?.LogDebug("Remove {instanceId}", instanceId);

            if (_serviceHeartbeats.TryRemove(instanceId, out var timer))
            {
                timer.Dispose();
            }
        }

        private bool disposed = false;

        /// <summary>
        /// Remove all heart beats from scheduler
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Cleanup
                    foreach (var instance in _serviceHeartbeats.Keys)
                    {
                        Remove(instance);
                    }
                }

                disposed = true;
            }
        }

        ~NacosTtlScheduler()
        {
            Dispose(false);
        }

        private Task PassTtl(string serviceId)
        {
            _logger?.LogDebug("Sending consul heartbeat for: {serviceId} ", serviceId);

            try
            {
                SendHeartbeatRequest sendHeartbeatRequest = new SendHeartbeatRequest()
                {
                    Ephemeral = false,
                    ServiceName = Options.ServiceName,
                    GroupName = Options.GroupName,
                    BeatInfo = new BeatInfo
                    {
                        Ip = Options.Host,
                        Port = Options.Port,
                        ServiceName = Options.ServiceName,
                        Scheduled = true,
                        Cluster = Options.ClusterName,
                    },

                };

                return _client.SendHeartbeatAsync(sendHeartbeatRequest);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Exception sending consul heartbeat for: {serviceId} ", serviceId);
            }

            return Task.CompletedTask;
        }
    }
}
