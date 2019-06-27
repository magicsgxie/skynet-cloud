
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{

    /// <summary>
    ///  Column映射表达式
    /// </summary>
    public class ColumnExpression : ColumnExpression<ColumnExpression, ColumnAttribute>
    {
        internal ColumnExpression() { }
        /// <summary>
        /// 设置为Required
        /// </summary>
        /// <returns></returns>
        public ColumnExpression Required()
        {
            attribute.IsNullable = false;
            return this;
        }
    }
}
