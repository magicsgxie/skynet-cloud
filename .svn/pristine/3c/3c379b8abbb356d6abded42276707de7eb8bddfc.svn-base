using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Discovery.Nacos.Registry
{
    public interface IServiceRegistrar : IDisposable
    {
        /// <summary>
        /// Start the service registrar
        /// </summary>
        void Start();

        /// <summary>
        /// Register any registrations configured
        /// </summary>
        void Register();

        /// <summary>
        /// Deregister any registrations configured
        /// </summary>
        void Deregister();
    }
}
