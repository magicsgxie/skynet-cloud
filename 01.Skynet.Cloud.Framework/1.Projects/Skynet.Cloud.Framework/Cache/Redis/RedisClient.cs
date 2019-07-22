using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace UWay.Skynet.Cloud.Cache.Redis
{


 

    public class RedisClient : IDisposable
    {
        private readonly int DEFAULT_PORT = 6379;

        private IConfiguration _config;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        public RedisClient(IConfiguration config)
        {
            _config = config;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }
        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <param name="redisConfig">RedisConfig配置文件</param>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect(IConfigurationSection redisConfig)
        {
            var redisInstanceName = redisConfig["instancename"];
            var connStr = GetConnectionString(redisConfig);

            return _connections.GetOrAdd(redisInstanceName, p => ConnectionMultiplexer.Connect(connStr));
        }
        /// <summary>
        /// 检查入参数
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的 Redis_Default/Redis_6 名称</param>
        /// <returns></returns>
        private IConfigurationSection CheckeConfig(string configName)
        {
            IConfigurationSection redisConfig = _config.GetSection("spring:redis");
            if (redisConfig == null)
            {
                throw new ArgumentNullException($"{configName}找不到对应的RedisConfig配置！");
            }
            var redisInstanceName = redisConfig["instancename"];
            var connStr = redisConfig["host"];
            if (string.IsNullOrEmpty(redisInstanceName))
            {
                throw new ArgumentNullException($"{configName}找不到对应的instancename");
            }
            if (string.IsNullOrEmpty(connStr))
            {
                throw new ArgumentNullException($"{configName}找不到对应的Connection");
            }
            return redisConfig;
        }
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase(string configName = null, int? db = null)
        {
            int defaultDb = 0;
            IConfigurationSection redisConfig = CheckeConfig(configName);
            if (db.HasValue)
            {
                defaultDb = db.Value;
            }
            else
            {
                var strDefalutDatabase = redisConfig.GetValue<string>("database");
                if (!string.IsNullOrEmpty(strDefalutDatabase) && Int32.TryParse(strDefalutDatabase, out var intDefaultDatabase))
                {
                    defaultDb = intDefaultDatabase;
                }
            }
            return GetConnect(redisConfig).GetDatabase(defaultDb);
        }

        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            IConfigurationSection redisConfig = CheckeConfig(configName);
            var connStr = GetConnectionString(redisConfig);

            var confOption = ConfigurationOptions.Parse((string)connStr);
            return GetConnect(redisConfig).GetServer(confOption.EndPoints[endPointsIndex]);
        }

        private string GetConnectionString(IConfigurationSection redisConfig)
        {
            var host = redisConfig.GetValue<string>("host");
            var port = redisConfig.GetValue<int>("port", DEFAULT_PORT);
            var dataBase = redisConfig.GetValue<int>("database", 0);
            var password = redisConfig.GetValue<string>("password");
            return string.Format("{0}:{1},database={2},password={3}", host, port, dataBase, password);

        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            IConfigurationSection redisConfig = CheckeConfig(configName);
            return GetConnect(redisConfig).GetSubscriber();
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
}