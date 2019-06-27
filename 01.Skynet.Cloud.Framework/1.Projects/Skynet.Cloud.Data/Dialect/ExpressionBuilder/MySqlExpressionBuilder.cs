using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Dialect.ExpressionBuilder
{
    class MySqlExpressionBuilder : DbExpressionBuilder
    {
        public override Expression Translate(Expression expression)
        {
            expression = OrderByRewriter.Rewrite(expression);
            expression = base.Translate(expression);
            expression = UnusedColumnRemover.Remove(expression);
            return expression;
        }

        public override Expression GetGeneratedIdExpression(IMemberMapping member)
        {
            return new FunctionExpression(member.MemberType, "LAST_INSERT_ID()", null);
        }

        public override Expression GetRowsAffectedExpression(Expression command)
        {
            return new FunctionExpression(typeof(int), "ROW_COUNT()", null);
        }

        public override bool IsRowsAffectedExpressions(Expression expression)
        {
            FunctionExpression fex = expression as FunctionExpression;
            return fex != null && fex.Name == "ROW_COUNT()";
        }

    }
}
