using Microsoft.Extensions.Options;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Nacos.Registry;
using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    public class ThisServiceInstance : IServiceInstance
    {


        public ThisServiceInstance(INacosRegistration registration)
        {
            ServiceId = registration.ServiceId;
            Host = registration.Host;
            IsSecure = registration.IsSecure;
            Port = registration.Port;
            Metadata = registration.Metadata;
            Uri = registration.Uri;
        }


        #region Implementation of IServiceInstance

        /// <inheritdoc/>
        public string ServiceId { get; }

        /// <inheritdoc/>
        public string Host  {get;protected set;}

        /// <inheritdoc/>
        public int Port { get; protected set; }

        /// <inheritdoc/>
        public bool IsSecure { get; protected set; }

        public IDictionary<string, string> Metadata { set; get; }


        /// <inheritdoc/>
        public Uri Uri
        {
            get;
        }

        /// <inheritdoc/>
        //public IDictionary<string, string> Metadata { protected set; get; }

        #endregion Implementation of IServiceInstance
    }
}
