using System;

namespace UWay.Skynet.Cloud.Cache
{
    /// <summary>
    /// 内存属性设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : Attribute
    {
        /// <summary>
        /// 过期时间 秒
        /// </summary>
        public int AbsoluteExpiration { get; set; }
    }
}
