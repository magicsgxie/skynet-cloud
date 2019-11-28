using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Common
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = DbExpressionType.Insert,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = DbExpressionType.Delete,
        /// <summary>
        /// 更新
        /// </summary>
        Update = DbExpressionType.Update,
        /// <summary>
        /// 查询
        /// </summary>
        Select = DbExpressionType.Select,
    }
}
