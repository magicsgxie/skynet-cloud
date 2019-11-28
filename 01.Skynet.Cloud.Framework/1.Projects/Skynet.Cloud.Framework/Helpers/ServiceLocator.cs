using System;
using Microsoft.Extensions.DependencyInjection;

namespace UWay.Skynet.Cloud.Helpers
{
    /// <summary>
    /// .Net Core自带的DI辅助类
    /// </summary>
    public class ServiceLocator
    {
        private static IServiceCollection _container;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ServiceLocator()
        {
            _container = new ServiceCollection();
        }
        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection CreateServiceBuilder(IServiceCollection services = null)
        {
            var factory = new DefaultServiceProviderFactory();
            if (services == null) services = _container;
            _container = factory.CreateBuilder(services);
            return _container;
        }

        /// <summary>
        /// 获取Service Provider
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(IServiceCollection container = null)
        {
            return CreateServiceBuilder(container).BuildServiceProvider();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static T GetInstance<T>(IServiceProvider provider=null)
        {
            if (provider == null) provider = GetServiceProvider();
            return provider.GetService<T>();
        }
    }
}
