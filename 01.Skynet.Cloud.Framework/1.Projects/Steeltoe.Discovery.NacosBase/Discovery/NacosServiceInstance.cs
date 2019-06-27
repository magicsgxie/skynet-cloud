using Steeltoe.Common.Discovery;
using System;
using System.Collections.Generic;
using System.Text;
using Steeltoe.Common.Discovery;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    public class NacosServiceInstance : IServiceInstance
    {

        /// <summary>
        /// Initializes a new instance of the<see cref="NacosServiceInstance"/> class.
        /// </summary>
        /// <param name = "GetInstanceResult" > the service entry from the Nacos server</param>
        public NacosServiceInstance(GetInstanceResult getInstanceResult)
        {
            // TODO: 3.0  ID = healthService.ID;
            Host = getInstanceResult.Ip;
            var metadata = getInstanceResult.Metadata;
            IsSecure = metadata.TryGetValue("secure", out var secureString) && bool.Parse(secureString);
            ServiceId = getInstanceResult.Service;
            Port = getInstanceResult.Port;
            Metadata = metadata;
            var scheme = IsSecure ? "https" : "http";
            Uri = new Uri($"{scheme}://{Host}:{Port}");
        }

        public NacosServiceInstance(Host host)
        {
            // TODO: 3.0  ID = healthService.ID;
            Host = host.Ip;
            var metadata = host.Metadata;
            IsSecure = metadata.TryGetValue("secure", out var secureString) && bool.Parse(secureString);
            ServiceId = host.ServiceName;
            Port = host.Port;
            Metadata = metadata;
            var scheme = IsSecure ? "https" : "http";
            Uri = new Uri($"{scheme}://{Host}:{Port}");
        }


        #region Implementation of IServiceInstance

        /// <inheritdoc/>
        public string ServiceId { get; }

        /// <inheritdoc/>
        public string Host { get; }

        /// <inheritdoc/>
        public int Port { get; }

        /// <inheritdoc/>
        public bool IsSecure { get; }

        /// <inheritdoc/>
        public Uri Uri { get; }

        /// <inheritdoc/>
        public IDictionary<string, string> Metadata { get; }

        #endregion Implementation of IServiceInstance
    }
}
