using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Configuration.NacosServerBase
{
    public class ConfigNacosConfigurationSource : IConfigurationSource
    {
        public IHttpClientFactory ClientFactory { set; get; }

        public ILocalConfigInfoProcessor Processor { set; get; }

        /// <summary>
        /// Gets or sets gets the configuration the Config Server client uses to contact the Config Server.
        /// Values returned override the default values provided in <see cref="DefaultSettings"/>
        /// </summary>
        public IConfiguration Configuration { get; protected set; }


        protected internal IDictionary<string, object> _properties = new Dictionary<string, object>();


        protected internal IList<IConfigurationSource> _sources = new List<IConfigurationSource>();
        /// <summary>
        /// Gets the logger factory used by the Config Server client
        /// </summary>
        public ILoggerFactory LogFactory { get; }


        /// <summary>
        /// Gets the default settings the Config Server client uses to contact the Config Server
        /// </summary>
        public ConfigNacosClientSettings DefaultSettings { get; }


        public ConfigNacosConfigurationSource(IConfiguration configuration, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
            :this(new ConfigNacosClientSettings(), configuration, clientFactory, processor, logFactory)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationSource"/> class.
        /// </summary>
        /// <param name="defaultSettings">the default settings used by the Config Server client</param>
        /// <param name="configuration">configuration used by the Config Server client. Values will override those found in default settings</param>
        /// <param name="logFactory">optional logger factory used by the client</param>
        public ConfigNacosConfigurationSource(ConfigNacosClientSettings defaultSettings, IConfiguration configuration, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            DefaultSettings = defaultSettings ?? throw new ArgumentNullException(nameof(defaultSettings));
            LogFactory = logFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationSource"/> class.
        /// </summary>
        /// <param name="sources">configuration sources used by the Config Server client. The <see cref="Configuration"/> will be built from these sources and the
        /// values will override those found in <see cref="DefaultSettings"/></param>
        /// <param name="properties">properties to be used when sources are built</param>
        /// <param name="logFactory">optional logger factory used by the client</param>
        public ConfigNacosConfigurationSource(IList<IConfigurationSource> sources, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, IDictionary<string, object> properties = null, ILoggerFactory logFactory = null)
            : this(new ConfigNacosClientSettings(), sources, clientFactory, processor, properties, logFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationSource"/> class.
        /// </summary>
        /// <param name="defaultSettings">the default settings used by the Config Server client</param>
        /// <param name="sources">configuration sources used by the Config Server client. The <see cref="Configuration"/> will be built from these sources and the
        /// values will override those found in <see cref="DefaultSettings"/></param>
        /// <param name="properties">properties to be used when sources are built</param>
        /// <param name="logFactory">optional logger factory used by the client</param>
        public ConfigNacosConfigurationSource(ConfigNacosClientSettings defaultSettings, IList<IConfigurationSource> sources, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, IDictionary<string, object> properties = null, ILoggerFactory logFactory = null)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            _sources = new List<IConfigurationSource>(sources);

            if (properties != null)
            {
                _properties = new Dictionary<string, object>(properties);
            }
            Processor = processor;
            ClientFactory = clientFactory;
            DefaultSettings = defaultSettings ?? throw new ArgumentNullException(nameof(defaultSettings));
            LogFactory = logFactory;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            if (Configuration == null)
            {
                // Create our own builder to build sources
                ConfigurationBuilder configBuilder = new ConfigurationBuilder();
                foreach (IConfigurationSource s in _sources)
                {
                    configBuilder.Add(s);
                }

                // Use properties provided
                foreach (var p in _properties)
                {
                    configBuilder.Properties.Add(p);
                }

                // Create configuration
                Configuration = configBuilder.Build();
            }

            return new ConfigNacosConfigurationProvider(this);
        }
    }
}
