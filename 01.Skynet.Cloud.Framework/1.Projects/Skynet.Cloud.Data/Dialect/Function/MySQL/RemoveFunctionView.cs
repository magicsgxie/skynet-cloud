using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class RemoveFunctionView : IFunctionView
    {
        //SELECT concat(substring('abcdefg',1,2),SUBSTRING('abcdefg', 6, LENGTH('abcdefg'))) AS Expr1
        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "remove", "", "2 or 3"));

            var strExpression = args[0];
            ctx.Append("CASE WHEN (LENGTH(");
            ctx.Visit(args[0]);
            ctx.Append(") < ");
            ctx.Visit(args[1]);
            if (args.Length == 3)
                ctx.Append(") THEN NULL ELSE CONCAT(");
            else
                ctx.Append(") THEN NULL ELSE ");
            args[1] = Expression.Subtract(Expression.Property(args[1], "Value"), Expression.Constant(1, Types.Int32));
            ctx.Append("SUBSTRING(");
            ctx.Visit(strExpression);
            ctx.Append(",1,");
            ctx.Visit(args[1]);

            if (args.Length == 3)
            {
                ctx.Append("),SUBSTRING(");
                ctx.Visit(strExpression);
                ctx.Append(",");
                // var secondStartIndexExpression = Expression.Constant((int)(args[1] as ConstantExpression).Value + (int)(args[2] as ConstantExpression).Value + 1);
                ctx.Visit(Expression.Add(Expression.Add(args[1], Expression.Property(args[2], "Value")), Expression.Constant(1, Types.Int32)));
                ctx.Append(",LENGTH(");
                ctx.Visit(strExpression);
                ctx.Append("))) END");
            }
            else
                ctx.Append(")END");
        }
    }
}
