using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    internal class InsertFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {

            if (args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Insert", "", "3"));
            }
            var secondStartIndex = args[1];// Expression.Constant((int)(args[1] as ConstantExpression).Value + 1);
            ctx.Append("(CASE WHEN ");
            ctx.Visit(secondStartIndex);
            ctx.Append(" > LENGTH(");
            ctx.Visit(args[0]);
            ctx.Append(") THEN null ELSE (SUBSTR(");
            ctx.Visit(args[0]);
            ctx.Append(",1,");
            ctx.Visit(Expression.Subtract(Expression.Property(args[1], "Value"), Expression.Constant(1, Types.Int32)));
            ctx.Append(") || ");
            ctx.Visit(args[2]);
            ctx.Append(" || SUBSTR(");
            ctx.Visit(args[0]);
            ctx.AppendFormat(",");
            ctx.Visit(secondStartIndex);
            ctx.Append(",LENGTH(");
            ctx.Visit(args[0]);
            ctx.Append(")))END)");
        }
    }
}
