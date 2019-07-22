using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace UWay.Skynet.Cloud.Cache.Redis
{

    /// <summary>
    /// 单例
    /// </summary>
    public static class RedisClientSingleton
    {
        private static RedisClient _redisClinet;

        private static object _lockObj = new object();
        public static RedisClient GetInstance(IConfiguration config)
        {
            if (_redisClinet == null)
            {
                lock (_lockObj)
                {
                    if (_redisClinet == null)
                    {
                        _redisClinet = new RedisClient(config);
                    }
                }
            }
            return _redisClinet;
        }

        public static IDatabase GetDefaultDatabase(this RedisClient client)
        {
            return client.GetDatabase("Redis_Default");
        }

        public static IDatabase Redis(this IConfiguration config)
        {
            return RedisClientSingleton.GetInstance(config).GetDefaultDatabase();
        }
    }
}