using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Configuration.NacosServerBase
{

    public static class ConfigurationSettingsHelper
    {
        private const string SPRING_APPLICATION_PREFIX = "spring:application";
        private const string VCAP_SERVICES_CONFIGSERVER_PREFIX = "vcap:services:p-config-server:0";

        public static void Initialize(string configPrefix, ConfigNacosClientSettings settings, IConfiguration config)
        {
            if (configPrefix == null)
            {
                throw new ArgumentNullException(nameof(configPrefix));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var clientConfigsection = config.GetSection(configPrefix);

            
            settings.Name = GetApplicationName(clientConfigsection, config, settings.Name);
            settings.Environment = GetEnvironment(clientConfigsection, settings.Environment);
            settings.IsSecure = IsSecure(clientConfigsection);
            settings.GroupName = GetGroupName(clientConfigsection,settings.GroupName);
            settings.FileExtension = GetFileExtension(clientConfigsection, settings.FileExtension);
            settings.Namespace= GetNameSpace(clientConfigsection);
            settings.ServerAddr = GetServerAddress(clientConfigsection, settings.ServerAddr);
            settings.ClusterName = GetClusterName(clientConfigsection, settings.ClusterName);
            settings.Timeout = GetTimeout(clientConfigsection, settings.Timeout);
            settings.Interval = GetInterval(clientConfigsection, settings.Interval);
            settings.ContextPath = GetContextPath(clientConfigsection);
            settings.ExtConfigs = GetExtConfigs(clientConfigsection);
            settings.ShareDataIds = GetShareDataIds(clientConfigsection);
            settings.Prefix = GetPrefix(clientConfigsection);
            settings.Endpoint = GetEndpoint(clientConfigsection);
            settings.Enabled = GetEnabled(clientConfigsection, settings.Enabled);
            settings.RefreshableDataIds = GetRefreshableDataIds(clientConfigsection);

            // Override Config server URI
            settings.ServerAddr = GetCloudFoundryUri(clientConfigsection, config, settings.ServerAddr);
        }

        private static IList<ExtConfig> GetExtConfigs(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<IList<ExtConfig>>("ext-config");
        }


        private static string GetRefreshableDataIds(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<string>("refreshable-dataids");
        }


        private static string GetShareDataIds(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<string>("shared-dataids");
        }

        private static string GetPrefix(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<string>("prefix");
        }

        private static string GetEndpoint(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<string>("endpoint");
        }

        private static bool GetEnabled(IConfigurationSection clientConfigsection, bool def)
        {
            return clientConfigsection.GetValue<bool>("enabled", def);
        }

        private static bool GetEncode(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<bool>("encode");
        }

        private static string GetClusterName(IConfigurationSection clientConfigsection, string def)
        {
            return clientConfigsection.GetValue("cluster-name", def);
        }

   

        private static int GetTimeout(IConfigurationSection clientConfigsection, int def)
        {
            return clientConfigsection.GetValue("timeout", def);
        }

        private static string GetFileExtension(IConfigurationSection clientConfigsection, string def)
        {
            return clientConfigsection.GetValue("file-extension", def);
        }

        private static string GetServerAddress(IConfigurationSection clientConfigsection, string def)
        {
            return clientConfigsection.GetValue("server-addr", def);
        }

        private static string GetNameSpace(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<string>("namespace");
        }

        private static string GetGroupName(IConfigurationSection clientConfigsection, string def)
        {
            return clientConfigsection.GetValue<string>("group-name", def);
        }

        private static string GetContextPath(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<string>("context-path");
        }
        private static bool IsSecure(IConfigurationSection clientConfigsection)
        {
            return clientConfigsection.GetValue<bool>("is-secure");
        }

        private static string GetEnvironment(IConfigurationSection section, string def)
        {
            return section.GetValue("env", string.IsNullOrEmpty(def) ? ConfigNacosClientSettings.DEFAULT_ENVIRONMENT : def);
        }

        private static int GetInterval(IConfigurationSection clientConfigsection, int def)
        {
            return clientConfigsection.GetValue("interval", def);
        }

       

        private static string GetApplicationName(IConfigurationSection primary, IConfiguration config, string defName)
        {
            return GetSetting("name", primary, config.GetSection(SPRING_APPLICATION_PREFIX), defName);
        }

        private static string GetCloudFoundryUri(IConfiguration configServerSection, IConfiguration config, string def)
        {
            return GetSetting("credentials:uri", config.GetSection(VCAP_SERVICES_CONFIGSERVER_PREFIX), configServerSection, def);
        }

        private static string GetSetting(string key, IConfiguration primary, IConfiguration secondary, string def)
        {
            var result = primary.GetValue<string>(key);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            result = secondary.GetValue<string>(key);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            return def;
        }
    }
    
}
