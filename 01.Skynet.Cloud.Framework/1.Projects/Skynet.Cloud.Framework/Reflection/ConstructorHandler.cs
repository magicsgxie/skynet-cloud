using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 构造函数扩展类
    /// </summary>
    public static class CounstructorExtensions
    {
        private static readonly Dictionary<ConstructorInfo, ConstructorHandler> factoryMethodCache = new Dictionary<ConstructorInfo, ConstructorHandler>();

        /// <summary>
        /// 快速构造函数方法调用
        /// </summary>
        /// <param name="constructor">构造函数</param>
        /// <param name="args">参数</param>
        /// <returns>返回创建的对象</returns>
        public static object FastInvoke(this ConstructorInfo constructor, params object[] args)
        {
            if (constructor == null)
                throw new ArgumentNullException("constructor");

            if (args == null)
                args = new object[0];

            ConstructorHandler handler;
            if(factoryMethodCache.ContainsKey(constructor))
            {
                handler = factoryMethodCache[constructor];
            }
            else
            {
                handler = constructor.GetCreator();
                factoryMethodCache.Add(constructor,handler);
            }
            return handler(args);
        }
    }
}
