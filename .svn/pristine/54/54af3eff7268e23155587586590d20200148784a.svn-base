using Microsoft.Extensions.Configuration;
using Steeltoe.Common.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Configuration.NacosServerBase
{
    public class ConfigNacosClientSettingsOptions : AbstractOptions
    {
        public const string CONFIGURATION_PREFIX = "spring:cloud:nacos:config";

        public ConfigNacosClientSettingsOptions(IConfigurationRoot root)
            : base(root, CONFIGURATION_PREFIX)
        {
        }

        public ConfigNacosClientSettingsOptions(IConfiguration config)
            : base(config)
        {
        }

        public string ServerAddr { set; get; }

        public string Env { get; set; }

        public string Name { get; set; }

        public string GroupName { get; set; }

        public string ClusterName { get; set; }

        public string Namespace { get; set; }


        public string Prefix { get; set; }

        public string Endpoint { get; set; }

        public string ShareDataIds { get; set; }

        public string ContextPath { get; set; }

        public bool Enabled { get; set; }


        public string FileExtension { set; get; }
        public string SecretKey { set; get; }
        public virtual IList<ExtConfig> ExtConfigs { get; set; }
        public string AccessKey { set; get; }
        public int Interval { get; set; } = 8000;
        public bool IsSecure { get; set; }

        public int Timeout { get; set; } = ConfigNacosClientSettings.DEFAULT_TIMEOUT_MILLISECONDS;

        public ConfigNacosClientSettings Settings
        {
            get
            {
                
                ConfigNacosClientSettings settings = new ConfigNacosClientSettings();
                settings.ServerAddr = ServerAddr;
                settings.GroupName = GroupName;
                settings.ClusterName = ClusterName;
                settings.Timeout = Timeout;
                settings.IsSecure = IsSecure;
                settings.Namespace = Namespace;
                settings.Interval = Interval;
                settings.FileExtension = FileExtension;
                settings.ExtConfigs = ExtConfigs;
                settings.ShareDataIds = ShareDataIds;
                settings.ContextPath = ContextPath;
                settings.Prefix = Prefix;
                settings.Endpoint = Endpoint;
                settings.Enabled = Enabled;
                settings.Environment = Env;
                settings.Name = Name;
                return settings;
            }
        }
    }
}
