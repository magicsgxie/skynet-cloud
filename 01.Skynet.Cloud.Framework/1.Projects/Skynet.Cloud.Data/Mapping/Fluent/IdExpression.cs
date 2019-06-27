
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 主键映射表达式
    /// </summary>
    public class IdExpression : ColumnExpression<IdExpression, IdAttribute>
    {
        /// <summary>
        /// 设置主键是否为数据库自动生成的
        /// </summary>
        /// <returns></returns>
        public IdExpression DbGenerated()
        {
            attribute.IsDbGenerated = true;
            return this;
        }
    }
}
