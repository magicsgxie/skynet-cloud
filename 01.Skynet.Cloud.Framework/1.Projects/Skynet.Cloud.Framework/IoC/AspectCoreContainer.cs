using System;
using System.Collections.Generic;
using System.Linq;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using Microsoft.Extensions.DependencyInjection;

namespace UWay.Skynet.Cloud.IoC
{
    /// <summary>
    /// IoC容器初始化
    /// </summary>
    public class AspectCoreContainer
    {
        private static IServiceResolver resolver { get; set; }

        /// <summary>
        /// 构建Ioc容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceProvider BuildServiceProvider(IServiceCollection services, Action<IAspectConfiguration> configure = null)
        {
            if(services==null)throw new ArgumentNullException(nameof(services));
            services.ConfigureDynamicProxy(configure);
            services.AddAspectCoreContainer();
            return resolver = services.ToServiceContainer().Build();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            return resolver.Resolve<T>();
        }

        /// <summary>
        /// 获取Service 实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ResolveServices<T>()
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            return resolver.GetServices<T>().ToList();
        }
    }
}
