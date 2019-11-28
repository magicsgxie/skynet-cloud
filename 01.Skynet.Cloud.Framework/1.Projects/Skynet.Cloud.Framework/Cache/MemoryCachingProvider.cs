using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Cache
{
    /// <summary>
    /// 内存提供者
    /// </summary>
    public class MemoryCachingProvider : ICachingProvider
    {
        private IMemoryCache _cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache">内存</param>
        public MemoryCachingProvider(IMemoryCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// 从内存中获取值
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <returns></returns>
        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }
        /// <summary>
        /// 从内存中获取TItem类型对象
        /// </summary>
        /// <typeparam name="TItem">对象类型</typeparam>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public TItem Get<TItem>(object key) where TItem : class
        {
            return _cache.Get<TItem>(key);
        }
        /// <summary>
        /// 异步从内存中获取值
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <returns></returns>
        public async Task<object> GetAsync(string cacheKey)
        {
            return await Task.FromResult<object>(_cache.Get(cacheKey));
        }

        /// <summary>
        /// 添加内存
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <param name="cacheValue">对象</param>
        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue);
        }

        /// <summary>
        /// 添加内存
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <param name="cacheValue">对象</param>
        /// <param name="absoluteExpirationRelativeToNow">过期时间</param>
        public void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            _cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
        }

        /// <summary>
        /// 异步添加内存
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <param name="cacheValue">对象</param>
        /// <param name="absoluteExpirationRelativeToNow">过期时间</param>
        /// <returns>线程任务</returns>
        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            await Task.Run(() =>
            {
                _cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
            });
        }
    }
}
