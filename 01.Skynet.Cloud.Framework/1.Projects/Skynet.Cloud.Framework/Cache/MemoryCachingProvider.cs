using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Cache
{
    public class MemoryCachingProvider : ICachingProvider
    {
        private IMemoryCache _cache;

        public MemoryCachingProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }
        public TItem Get<TItem>(object key) where TItem : class
        {
            return _cache.Get<TItem>(key);
        }
        public async Task<object> GetAsync(string cacheKey)
        {
            return await Task.FromResult<object>(_cache.Get(cacheKey));
        }
        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue);
        }
        public void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            _cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            await Task.Run(() =>
            {
                _cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
            });
        }
    }
}
