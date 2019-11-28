namespace UWay.Skynet.Cloud.Nacos
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class MemoryLocalConfigInfoProcessor : ILocalConfigInfoProcessor
    {        
        private readonly ConcurrentDictionary<string, string> _cache;

        /// <summary>
        /// 
        /// </summary>
        public MemoryLocalConfigInfoProcessor()
        {
            _cache = new ConcurrentDictionary<string, string>();            
        }

        private string GetCacheKey(string dataId, string group, string tenant)
        {
            return $"{tenant}-{group}-{dataId}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<string> GetFailoverAsync(string dataId, string group, string tenant)
        {
            var cacheKey = GetCacheKey(dataId, group, tenant);

            _cache.TryGetValue(cacheKey, out string config);

            return await Task.FromResult(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<string> GetSnapshotAync(string dataId, string group, string tenant)
        {
            var cacheKey = GetCacheKey(dataId, group, tenant);

            _cache.TryGetValue(cacheKey, out string config);

            return await Task.FromResult(config);
        }       


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SaveSnapshotAsync(string dataId, string group, string tenant, string config)
        {
            var cacheKey = GetCacheKey(dataId, group, tenant);
            
            if (string.IsNullOrEmpty(config))
            {
                _cache.TryRemove(cacheKey,out _);
            }
            else
            {
                _cache.AddOrUpdate(cacheKey, config, (k, v) => config);
            }

            await Task.Yield();
        }

    }
}
