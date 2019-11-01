using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Common.Discovery;
using UWay.Skynet.Cloud.Nacos;
using Steeltoe.Discovery.Nacos.Registry;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    public class NacosDiscoveryClient : IDiscoveryClient
    {
        private readonly IServiceInstance _thisServiceInstance;
        private readonly INacosNamingClient _client;
        private readonly ILogger<NacosDiscoveryClient> _logger;
        private readonly NacosDiscoveryOptions _options;
        private readonly INacosServiceRegistrar _registrar;

        public NacosDiscoveryClient(
             IOptionsMonitor<NacosDiscoveryOptions> optionAccs
            , INacosNamingClient nacosNamingClient
            ,INacosServiceRegistrar registrar = null,
            ILogger<NacosDiscoveryClient> logger = null) :this(optionAccs.CurrentValue, nacosNamingClient, registrar,logger)
        {
            
        }



        public NacosDiscoveryClient(NacosDiscoveryOptions options
            , INacosNamingClient nacosNamingClient
             , INacosServiceRegistrar registrar = null,
            ILogger<NacosDiscoveryClient> logger = null
            )
        {
            _client = nacosNamingClient ?? throw new ArgumentNullException(nameof(nacosNamingClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            this._logger = logger;
            //_thisServiceInstance = new ThisServiceInstance(options);
            _registrar = registrar;
            if (_registrar != null)
            {
                
                _registrar.Start();
                _thisServiceInstance = new ThisServiceInstance(_registrar.Registration);
            }
        }

        public string Description =>  "Spring Cloud Nacos";

        public IList<string> Services =>Task.Run(async () =>
                {
                    return await _client.ListServiceAsync();
         }).Result;

        public IList<IServiceInstance> GetInstances(string serviceId)
        {
            var request = new ListInstancesRequest() {
                ServiceName = serviceId
            };

            ListInstancesResult result = Task.Run(async () =>
            {
                return await _client.ListInstancesAsync(request);
            }).Result;
            IList<IServiceInstance> instances = new List<IServiceInstance>();
            if(result != null&& result.Hosts != null)
            {
                foreach(var host in result.Hosts)
                {
                    instances.Add(new NacosServiceInstance(host));
                }
            }
            
            return instances;

        }

        public IServiceInstance GetLocalServiceInstance()
        {
            return _thisServiceInstance;
        }

        public Task ShutdownAsync()
        {
            var tempRegistration = _registrar.Registration;
            var removeRequest = new RemoveInstanceRequest
            {
                ServiceName = tempRegistration.ServiceId,
                Ip = tempRegistration.Host,
                Port = tempRegistration.Port,
                GroupName = tempRegistration.GroupName,
                NamespaceId = tempRegistration.Namespace,
                ClusterName = tempRegistration.ClusterName,
                Ephemeral = tempRegistration.Ephemeral
            };

            return _client.RemoveInstanceAsync(removeRequest);
        }
    }
}
