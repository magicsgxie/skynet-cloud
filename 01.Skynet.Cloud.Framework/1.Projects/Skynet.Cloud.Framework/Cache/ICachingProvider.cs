using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IoC;

namespace UWay.Skynet.Cloud.Cache
{
    /// <summary>
    /// 内存供应商接口
    /// </summary>
    public interface ICachingProvider
    {
        /// <summary>
        /// 从内存中获取值
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <returns></returns>
        object Get(string cacheKey);

        /// <summary>
        /// 从内存中获取TItem类型对象
        /// </summary>
        /// <typeparam name="TItem">对象类型</typeparam>
        /// <param name="key">Key</param>
        /// <returns></returns>
        TItem Get<TItem>(object key) where TItem : class;

        /// <summary>
        /// 异步从内存中获取值
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <returns></returns>
        Task<object> GetAsync(string cacheKey);

        /// <summary>
        /// 添加内存
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <param name="cacheValue">对象</param>
        void Set(string cacheKey, object cacheValue);

        /// <summary>
        /// 添加内存
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <param name="cacheValue">对象</param>
        /// <param name="absoluteExpirationRelativeToNow">过期时间</param>
        void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);

        /// <summary>
        /// 异步添加内存
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <param name="cacheValue">对象</param>
        /// <param name="absoluteExpirationRelativeToNow">过期时间</param>
        /// <returns>线程任务</returns>
        Task SetAsync(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);
    }
}
