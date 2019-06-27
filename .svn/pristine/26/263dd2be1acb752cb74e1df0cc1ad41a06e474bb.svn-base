using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Configuration.NacosServerBase
{
    public class ConfigNacosClientSettings
    {
        /// <summary>
        /// Default certifcate validation enabled setting
        /// </summary>
        public const bool DEFAULT_CERTIFICATE_VALIDATION = false;


        /// <summary>
        /// Default timeout in milliseconds
        /// </summary>
        public const int DEFAULT_TIMEOUT_MILLISECONDS = 6 * 1000;

        /// <summary>
        /// Default discovery first service id setting
        /// </summary>
        public const string DEFAULT_CONFIGSERVER_SERVICEID = "nacosserver";

        /// <summary>
        /// Default enironment used when accessing configuration data
        /// </summary>
        public const string DEFAULT_ENVIRONMENT = "Production";

        /// <summary>
        /// Default enironment used when accessing configuration data
        /// </summary>
        public const bool DEFAULT_SECUE  = false;

        public const string DEFAULT_SERVER_ADDRESS = "localhost:8848";

        public const string DEFAULT_GROUP_NAME = "DEFAULT_GROUP";

        public const string DEFAULT_NAMESPACE = "";

        public const string DEFAULT_CLUSTER_NAME = "";

        /// <summary>
        /// Nacos server addresses.
        /// </summary>
        /// <example>
        /// 10.1.12.123:8848,10.1.12.124:8848
        /// </example>
        public virtual string ServerAddr { get; set; } = "";

        /// <summary>
        /// Gets or sets the environment used when accessing configuration data
        /// </summary>
        public virtual string Environment { get; set; }

        /// <summary>
        /// default timeout, unit is second.
        /// </summary>
        public virtual int Timeout { get; set; } = 15;

        /// <summary>
        /// default namespace
        /// </summary>
        public virtual string Namespace { get; set; } = "";
        public virtual bool IsSecure { set; get; }

        /// <summary>
        /// listen interval, unit is millisecond.
        /// </summary>
        public virtual int Interval { get; set; } = 8000;


        /// <summary>
        /// the name of the service.
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// the name of the service.
        /// </summary>
        public virtual string GroupName { get; set; } = DEFAULT_GROUP_NAME;

        /// <summary>
        /// the name of the service.
        /// </summary>
        public virtual string ClusterName { get; set; }


        public virtual string FileExtension { get; set; }



        public virtual string RefreshableDataIds { get; set; }



        public virtual string SecretKey { get; set; }

        public virtual string AccessKey { get; set; }

        public virtual IList<ExtConfig> ExtConfigs { get; set; }

        public virtual string ShareDataIds { get; set; }

        public virtual string ContextPath { get; set; }

        public virtual string Prefix { get; set; }

        public virtual string Endpoint { get; set; }

        public virtual bool Enabled { get; set; } = true;

        public virtual string Encode { get; set; }

        public ConfigNacosClientSettings()
    : base()
        {
            Interval = DEFAULT_TIMEOUT_MILLISECONDS;
            GroupName = DEFAULT_GROUP_NAME;
            Namespace = DEFAULT_NAMESPACE;
            IsSecure = DEFAULT_SECUE;
            Environment = DEFAULT_ENVIRONMENT;
            ClusterName = DEFAULT_CLUSTER_NAME;
        }
    }

    public class ExtConfig
    {
        public string DataId { set; get; }

        public string GroupName { set; get; } = "DEFAULT_GROUP";

        public bool Refresh { set; get; } = false;
    }
}
