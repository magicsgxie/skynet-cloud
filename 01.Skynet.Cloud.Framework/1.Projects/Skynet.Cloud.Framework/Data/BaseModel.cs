using System;
using UWay.Skynet.Cloud.Helpers;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 聚合接口
    /// </summary>
    public interface IAggregateRoot
    {
        
    }
    /// <summary>
    /// 所有数据表实体类都必须实现此接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [Serializable]
    public abstract class BaseModel<TKey>: IAggregateRoot
    {
        /// <summary>
        /// 聚合ID
        /// </summary>
        public abstract TKey Id { get; set; }
        /// <summary>
        /// 为一值
        /// </summary>
        public virtual string UniqueId { get; set; } = CommonHelper.NewMongodbId().ToString();
    }
}
