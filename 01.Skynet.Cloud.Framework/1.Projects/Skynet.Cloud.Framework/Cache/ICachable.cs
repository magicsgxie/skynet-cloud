namespace UWay.Skynet.Cloud.Cache
{
    /// <summary>
    /// 内存接口
    /// </summary>
    public interface ICachable
    {
        string CacheKey { get; }
    }
}
