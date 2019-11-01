using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Discovery.Nacos.Discovery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Registry
{
    public class NacosServiceRegistry : INacosServiceRegistry
    {
        private INacosNamingClient _nacosClient;

        private const string UP = "UP";

        private readonly INacosScheduler _scheduler;
        private readonly ILogger<NacosServiceRegistry> _logger;

        private const string OUT_OF_SERVICE = "OUT_OF_SERVICE";

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

        public NacosServiceRegistry(INacosNamingClient nacosClient, NacosDiscoveryOptions options, INacosScheduler scheduler = null, ILogger<NacosServiceRegistry> logger = null)
        {
            _scheduler = scheduler;
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _nacosClient = nacosClient;
            _logger = logger;
        }

        public NacosServiceRegistry(INacosNamingClient nacosClient, IOptionsMonitor<NacosDiscoveryOptions> optionsMonitor,  INacosScheduler scheduler = null, ILogger<NacosServiceRegistry> logger = null)
        {
            _scheduler = scheduler;
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _nacosClient = nacosClient;
            _logger = logger;
        }

        public void Deregister(INacosRegistration registration)
        {
            RemoveInstanceRequest request = new RemoveInstanceRequest()
            {
                Ip = registration.Host,
                Port = registration.Port,
                ServiceName = registration.ServiceId
            };

            _nacosClient.RemoveInstanceAsync(request);
        }

        public void Dispose()
        {
            _nacosClient = null;
        }

        /// <inheritdoc/>
        public async Task<object> GetStatusAsync(INacosRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            
            var request = new ListInstancesRequest() { ServiceName = registration.ServiceId,
            Clusters = registration.ClusterName};
            var result = await _nacosClient.ListInstancesAsync(request);
            //ListInstancesResult result = Task.Run(async () =>
            //{
            //    return 
            //}).Result;
            foreach (Host check in result.Hosts)
            {
                if(check.Ip.Equals(registration.Host))
                {
                    if(check.Healthy == false)
                    {
                        return OUT_OF_SERVICE;
                    }
                }
            }

            return UP;
        }


        public S GetStatus<S>(INacosRegistration registration) where S : class
        {
            var result = Task.Run(async () =>
            {
                return await GetStatusAsync(registration);
            }).Result;

            return (S)result;
        }

        /// <inheritdoc/>
        public Task RegisterAsync(INacosRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            return RegisterAsyncInternal(registration);
        }

        private async Task RegisterAsyncInternal(INacosRegistration registration)
        {
            _logger?.LogInformation("Registering service with nacos {serviceId} ", registration.ServiceId);

            try
            {
                await ResistryAsync(registration).ConfigureAwait(false);
                if (Options.IsHeartBeatEnabled && _scheduler != null)
                {
                    _scheduler.Add(registration.ServiceId);
                }
            }
            catch (Exception e)
            {
                if (Options.FailFast)
                {
                    _logger?.LogError(e, "Error registering service with consul {serviceId} ", registration.ServiceId);
                    throw;
                }

                _logger?.LogWarning(e, "Failfast is false. Error registering service with consul {serviceId} ", registration.ServiceId);
            }
        }

        private async Task<bool> ResistryAsync(INacosRegistration registration)
        {
            RegisterInstanceRequest request = new RegisterInstanceRequest()
            {
                ClusterName = registration.ClusterName,
                Ephemeral = registration.Ephemeral,
                ServiceName = registration.ServiceId,
                Enable = registration.Enable,
                GroupName = registration.GroupName,
                Ip = registration.Host,
                Port = registration.Port,
                NamespaceId = registration.Namespace
            };
            return await _nacosClient.RegisterInstanceAsync(request);
        }

        public Task DeregisterAsync(INacosRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            return DeregisterAsyncInternal(registration);
        }

        private async Task DeregisterAsyncInternal(INacosRegistration registration)
        {
            if (Options.IsHeartBeatEnabled && _scheduler != null)
            {
                _scheduler.Remove(registration.ServiceId);
            }

            _logger?.LogInformation("Deregistering service with consul {instanceId} ", registration.ServiceId);

            ModifyInstanceHealthStatusRequest request = new ModifyInstanceHealthStatusRequest()
            {
                Ip = registration.Host,
                Port = registration.Port,
                ServiceName = registration.ServiceId,
                Healthy = registration.Healthy,
                NamespaceId = registration.Namespace
            };
            await _nacosClient.ModifyInstanceHealthStatusAsync(request);
        }

        public void Register(INacosRegistration registration)
        {
            RegisterAsync(registration).GetAwaiter().GetResult();

        }

        public void SetStatus(INacosRegistration registration, string status)
        {
            DeregisterAsync(registration).GetAwaiter().GetResult();
        }
    }
}
