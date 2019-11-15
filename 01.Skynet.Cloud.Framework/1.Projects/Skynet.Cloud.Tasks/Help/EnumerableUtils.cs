using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Tasks.Help
{
    /// <summary>
    /// 集合通用扩展
    /// </summary>
    public static class EnumerableUtils
    {
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">当前枚举对象</param>
        /// <param name="action">处理函数</param>
        /// <returns>当前集合</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (T item in instance)
            {
                action(item);
            }
            return instance;
        }
    }
}
