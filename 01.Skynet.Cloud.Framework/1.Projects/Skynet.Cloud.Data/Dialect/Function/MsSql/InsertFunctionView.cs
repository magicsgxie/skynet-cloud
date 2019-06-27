using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    internal class InsertFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            //SUBSTRING(userName,1,2)+'ddd'+SUBSTRING(userName,3,LEN(userName))
            if (args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Insert", "", "3"));
            }
            //var secondStartIndex = Expression.Constant((int)(args[1] as ConstantExpression).Value + 1);
            builder.Append("(CASE WHEN ");
            builder.Visit(args[1]);
            builder.Append(" > LEN(");
            builder.Visit(args[0]);
            builder.Append(") THEN null ELSE (SUBSTRING(");
            builder.Visit(args[0]);
            builder.Append(",1,");
            builder.Visit(Expression.Subtract(Expression.Property(args[1], "Value"), Expression.Constant(1, Types.Int32)));
            builder.Append(") + ");
            builder.Visit(args[2]);
            builder.Append(" + SUBSTRING(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(",LEN(");
            builder.Visit(args[0]);
            builder.Append("))) END)");
        }
    }
}
