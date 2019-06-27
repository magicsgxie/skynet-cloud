
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{

    /// <summary>
    /// Column映射表达式
    /// </summary>
    /// <typeparam name="TColumnAttribute"></typeparam>
    public class ColumnExpression<TExpression, TColumnAttribute> : MemberExpression<TExpression, TColumnAttribute>
        where TColumnAttribute : ColumnAttribute, new()
        where TExpression : ColumnExpression<TExpression, TColumnAttribute>
    {

        protected ColumnExpression() { }
        /// <summary>
        /// 设置列名
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public TExpression ColumnName(string columnName)
        {
            Guard.NotNullOrEmpty(columnName, "columnName");
            attribute.Name = columnName;
            return this as TExpression;
        }


        /// <summary>
        /// 设置列的别名
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public TExpression Alias(string alias)
        {
            Guard.NotNullOrEmpty(alias, "alias");
            attribute.Alias = alias;
            return this as TExpression;
        }

        /// <summary>
        /// 设置数据类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TExpression DbType(DBType type)
        {
            attribute.DbType = type;
            return this as TExpression;
        }

        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public TExpression Length(int length)
        {
            attribute.Length = length;
            return this as TExpression;
        }

        /// <summary>
        /// 设置precision
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public TExpression Precision(byte precision)
        {
            attribute.Precision = precision;
            return this as TExpression;
        }

        /// <summary>
        /// 设置scale
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public TExpression Scale(byte scale)
        {
            attribute.Scale = scale;
            return this as TExpression;
        }
    }
}
