namespace UWay.Skynet.Cloud.Cache
{
    /// <summary>
    /// 内存接口
    /// </summary>
    public interface ICachable
    {
        /// <summary>
        /// 内存Key
        /// </summary>
        string CacheKey { get; }
    }
}
