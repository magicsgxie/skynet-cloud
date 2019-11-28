namespace UWay.Skynet.Cloud.Cache.Redis
{
    /// <summary>
    /// Redis JSON Setting
    /// </summary>
    public class RedisSetting
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }
    }
}
