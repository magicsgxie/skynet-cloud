
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 序列表达式
    /// </summary>
    public class SequenceExpression : ColumnExpression<SequenceExpression, IdAttribute>
    {
        internal SequenceExpression()
        {
            attribute.IsDbGenerated = true;
        }
        /// <summary>
        /// 设置序列名称
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        public SequenceExpression SequenceName(string sequenceName)
        {
            Guard.NotNullOrEmpty(sequenceName, "sequenceName");
            attribute.SequenceName = sequenceName;
            return this;
        }
    }
}
