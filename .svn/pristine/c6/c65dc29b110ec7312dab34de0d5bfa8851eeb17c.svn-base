using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Dialect.ExpressionBuilder
{
    class FirebirdExpressionBuilder : DbExpressionBuilder
    {
        public override Expression Translate(Expression expression)
        {
            expression = OrderByRewriter.Rewrite(expression);
            expression = base.Translate(expression);
            expression = SkipToRowNumberRewriter.Rewrite(expression);
            expression = OrderByRewriter.Rewrite(expression);
            return expression;
        }

        public override Expression GetGeneratedIdExpression(IMemberMapping member)
        {
            return new FunctionExpression(member.MemberType, "SCOPE_IDENTITY()", null);
        }
    }
}
