using System;
using Autofac;

namespace UWay.Skynet.Cloud.IoC
{
    /// <summary>
    /// 服务查询类
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// 当前容器
        /// </summary>
        public static IContainer Current { get; set; }

        public static T GetService<T>()
        {
            return Current.Resolve<T>();
        }

        public static bool IsRegistered<T>()
        {
            return Current.IsRegistered<T>();
        }

        public static bool IsRegistered<T>(string key)
        {
            return Current.IsRegisteredWithKey<T>(key);
        }

        public static bool IsRegistered(Type type)
        {
            return Current.IsRegistered(type);
        }

        public static bool IsRegisteredWithKey(string key, Type type)
        {
            return Current.IsRegisteredWithKey(key, type);
        }

        public static T GetService<T>(string key)
        {

            return Current.ResolveKeyed<T>(key);
        }

        public static object GetService(Type type)
        {
            return Current.Resolve(type);
        }

        public static object GetService(string key, Type type)
        {
            return Current.ResolveKeyed(key, type);
        }
    }
}
