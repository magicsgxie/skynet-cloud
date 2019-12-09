using Microsoft.Extensions.Configuration;
using UWay.Skynet.Cloud.IoC;

namespace UWay.Skynet.Cloud.Helpers
{
    /// <summary>
    /// 配置信息获取
    /// </summary>
    public class ConfigHelper
    {
        private static IConfiguration configuration = AspectCoreContainer.Resolve<IConfiguration>();

        /// <summary>
        /// 配置节点获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns>IConfigurationSection</returns>
        public static IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }

        /// <summary>
        /// 获取配置节点值
        /// </summary>
        /// <param name="key"></param>
        /// <returns>string</returns>
        public static string GetConfigurationValue(string key)
        {
            return configuration[key];
        }

        /// <summary>
        /// 获取指定节点下的配置信息值
        /// </summary>
        /// <param name="section">键</param>
        /// <param name="key"></param>
        /// <returns>string</returns>
        public static string GetConfigurationValue(string section, string key)
        {
            return GetSection(section)?[key];
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>string</returns>
        public static string GetConnectionString(string key)
        {
            return configuration.GetConnectionString(key);
        }
    }
}
