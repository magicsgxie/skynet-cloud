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

        private const string OUT_OF_SERVICE = "OUT_OF_SERVICE";

        public NacosServiceRegistry(INacosNamingClient nacosClient)
        {
            _nacosClient = nacosClient;
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

        public void Register(INacosRegistration registration)
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
            _nacosClient.RegisterInstanceAsync(request);

        }

        public void SetStatus(INacosRegistration registration, string status)
        {
            ModifyInstanceHealthStatusRequest request = new ModifyInstanceHealthStatusRequest() {
                Ip = registration.Host,
                Port = registration.Port,
                ServiceName = registration.ServiceId,
                Healthy = registration.Healthy,
                NamespaceId = registration.Namespace
            };
            _nacosClient.ModifyInstanceHealthStatusAsync(request);
        }
    }
}
