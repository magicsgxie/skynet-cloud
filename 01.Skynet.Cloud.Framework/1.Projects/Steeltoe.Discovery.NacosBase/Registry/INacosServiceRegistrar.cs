using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Discovery.Nacos.Registry
{
    public interface INacosServiceRegistrar : IServiceRegistrar
    {
        INacosRegistration Registration { get; }
    }
}
