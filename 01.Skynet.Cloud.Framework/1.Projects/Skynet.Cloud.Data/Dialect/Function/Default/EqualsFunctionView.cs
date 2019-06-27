using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class EqualsFunctionView : IFunctionView
    {
        internal bool isNot;
        public void Render(ISqlBuilder builder, params Expression[] args)
        {

            ConstantExpression right = args[1] as ConstantExpression;
            Expression left = null;

            if (right != null)//查找常量表达式，如果有那么设置常量表达式为右表达式
                left = args[0];
            else
            {
                right = args[0] as ConstantExpression;
                if (right != null)
                    left = args[1];
            }

            if (left == null && right == null)
            {
                builder.Append("(");
                builder.Visit(args[0]);
                if (isNot)
                    builder.Append("<>");
                else
                    builder.Append("=");
                builder.Visit(args[1]);
                builder.Append(")");
                return;
            }

            if (right != null)//如果右表达式是常量表达式
            {
                if (right.Value != null)
                {
                    builder.Append("(");
                    builder.Visit(args[0]);
                    if (isNot)
                        builder.Append("<>");
                    else
                        builder.Append("=");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    return;
                }
                else
                {
                    builder.Append("(");
                    builder.Visit(left);
                    if (isNot)
                        builder.Append(" IS NOT NULL)");
                    else
                        builder.Append(" IS NULL)");
                }

            }
            else//如果右表达式不是常量表达式
            {
                var cl = left as ConstantExpression;
                if (cl == null || cl.Value != null)
                {
                    //var leftType = left.Type;
                    //leftType = leftType.IsNullable() ? Nullable.GetUnderlyingType(leftType) : leftType;

                    //if (leftType == Types.Boolean)
                    //{
                    //    while (ExpressionType.Convert == left.NodeType)
                    //        left = ((UnaryExpression)left).Operand;

                    //    builder.Append("( NOT (");
                    //    builder.Visit(left);
                    //    builder.Append("))");
                    //}
                    //else
                    {
                        builder.Append("(");
                        builder.Visit(left);
                        if (isNot)
                            builder.Append(" IS NOT NULL)");
                        else
                            builder.Append(" IS NULL)");
                    }
                }
                else
                {
                    if (isNot)
                        builder.Append(" 1<>1");
                    else
                        builder.Append(" 1=1");
                }
            }
        }
    }

}
