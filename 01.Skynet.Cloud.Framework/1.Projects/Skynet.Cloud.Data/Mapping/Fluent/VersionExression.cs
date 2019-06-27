
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 版本映射表达式
    /// </summary>
    public class VersionExression : ColumnExpression<VersionExression, VersionAttribute>
    {
        internal VersionExression() { }
        /// <summary>
        /// 设置为Required
        /// </summary>
        /// <returns></returns>
        public VersionExression Required()
        {
            attribute.IsNullable = false;
            return this;
        }
    }
}
