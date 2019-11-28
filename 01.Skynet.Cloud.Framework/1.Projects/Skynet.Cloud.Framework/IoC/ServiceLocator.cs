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

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return Current.Resolve<T>();
        }

        /// <summary>
        /// 是否注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsRegistered<T>()
        {
            return Current.IsRegistered<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsRegistered<T>(string key)
        {
            return Current.IsRegisteredWithKey<T>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRegistered(Type type)
        {
            return Current.IsRegistered(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static bool IsRegisteredWithKey(string key, Type type)
        {
            return Current.IsRegisteredWithKey(key, type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetService<T>(string key)
        {

            return Current.ResolveKeyed<T>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return Current.Resolve(type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetService(string key, Type type)
        {
            return Current.ResolveKeyed(key, type);
        }
    }
}
