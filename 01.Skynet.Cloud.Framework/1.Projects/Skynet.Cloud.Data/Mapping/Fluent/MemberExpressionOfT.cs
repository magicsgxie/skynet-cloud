
namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 成员映射表达式
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public class MemberExpression<TExpression, TAttribute>
        where TAttribute : MemberAttribute, new()
        where TExpression : MemberExpression<TExpression, TAttribute>
    {
        internal TAttribute attribute;
        internal MemberExpression()
        {
            attribute = new TAttribute();
        }

        /// <summary>
        /// sets a private storage field to hold the value from a column.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        internal TExpression Storate(string storage)
        {
            attribute.Storage = storage;
            return this as TExpression;
        }


    }
}
