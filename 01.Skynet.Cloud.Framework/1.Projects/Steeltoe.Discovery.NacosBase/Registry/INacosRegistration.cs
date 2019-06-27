using Steeltoe.Common.Discovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Discovery.Nacos.Registry
{
    public interface INacosRegistration : IServiceInstance
    {

        
        
        string ClusterName { get; }

        bool Ephemeral {  get; }


        bool Enable { get; }

        bool Healthy { get; }

        string GroupName { get; }

        string Namespace { get; }

    }
}
