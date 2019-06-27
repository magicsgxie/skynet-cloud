
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 计算列映射表达式
    /// </summary>
    class ComputedColumnExpressison : ColumnExpression<ComputedColumnExpressison, ComputedColumnAttribute>
    {
        /// <summary>
        /// 设置为Required
        /// </summary>
        /// <returns></returns>
        public ComputedColumnExpressison Required()
        {
            attribute.IsNullable = false;
            return this;
        }
    }
}
