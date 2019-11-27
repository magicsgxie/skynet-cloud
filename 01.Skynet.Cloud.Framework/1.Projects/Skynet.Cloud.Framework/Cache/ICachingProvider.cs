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
        object Get(string cacheKey);

        TItem Get<TItem>(object key) where TItem : class;

        Task<object> GetAsync(string cacheKey);

        void Set(string cacheKey, object cacheValue);

        void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);

        Task SetAsync(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);
    }
}
