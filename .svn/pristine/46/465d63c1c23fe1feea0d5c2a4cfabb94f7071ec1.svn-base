using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Discovery.Nacos.Discovery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Registry
{
    public class NacosServiceRegistrar : INacosServiceRegistrar
    {
        internal int _running = 0;

        private const int NOT_RUNNING = 0;
        private const int RUNNING = 1;

        private readonly ILogger<NacosServiceRegistrar> _logger;
        private readonly INacosServiceRegistry _registry;
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


        /// <inheritdoc/>
        public INacosRegistration Registration { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulServiceRegistrar"/> class.
        /// </summary>
        /// <param name="registry">the Consul service registry to use when doing registrations</param>
        /// <param name="optionsMonitor">configuration options to use</param>
        /// <param name="registration">the registration to register with Consul</param>
        /// <param name="logger">optional logger</param>
        public NacosServiceRegistrar(INacosServiceRegistry registry, IOptionsMonitor<NacosDiscoveryOptions> optionsMonitor, INacosRegistration registration, ILogger<NacosServiceRegistrar> logger = null)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            Registration = registration ?? throw new ArgumentNullException(nameof(registration));
            _logger = logger;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ConsulServiceRegistrar"/> class.
        ///// </summary>
        ///// <param name="registry">the Consul service registry to use when doing registrations</param>
        ///// <param name="options">configuration options to use</param>
        ///// <param name="registration">the registration to register with Consul</param>
        ///// <param name="logger">optional logger</param>
        //public NacosServiceRegistrar(INacosServiceRegistry registry, NacosDiscoveryOptions options, INacosRegistration registration, ILogger<NacosServiceRegistrar> logger = null)
        //{
        //    _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        //    _options = options ?? throw new ArgumentNullException(nameof(options));
        //    Registration = registration ?? throw new ArgumentNullException(nameof(registration));
        //    _logger = logger;
        //}


        public void Deregister()
        {
            if (!Options.Register || !Options.Deregister)
            {
                _logger?.LogDebug("Deregistration disabled");
                return;
            }

            _registry.Deregister(Registration);
        }

        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref _running, NOT_RUNNING, RUNNING) == RUNNING)
            {
                Deregister();
            }

            _registry.Dispose();
        }

        public void Register()
        {
            if(!Options.Register)
            {
                _logger?.LogDebug("Registration disabled");
                return;
            }

            _registry.Register(Registration);
        }

        public void Start()
        {
            
            if (Interlocked.CompareExchange(ref _running, RUNNING, NOT_RUNNING) == NOT_RUNNING)
            {

                Register();

            }
        }
    }
}
