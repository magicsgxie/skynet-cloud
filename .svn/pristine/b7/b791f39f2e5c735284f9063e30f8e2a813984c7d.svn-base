using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class LastIndexOfFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "LastIndexOf", "", "2 or 3"));
            builder.Append("CASE WHEN CHARINDEX(");
            builder.Visit(args[1]);
            builder.Append(",");
            builder.Visit(args[0]);
            builder.Append(") = 0 THEN  -1  ELSE");
            if (args.Length == 3)
            {
                builder.Append("(CASE WHEN (CHARINDEX(");
                builder.Visit(args[1]);
                builder.Append(",SUBSTRING(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                //args[2] = Expression.Constant((int)(args[2] as ConstantExpression).Value + 1);
                builder.Visit(args[2]);
                builder.Append(")) = 0) or (");
                builder.Visit(args[2]);
                builder.Append(" > LEN(");
                builder.Visit(args[0]);
                builder.Append(")) THEN -1 ELSE LEN(SUBSTRING(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append(")) - (CHARINDEX(REVERSE(");
                builder.Visit(args[1]);
                builder.Append("),REVERSE(SUBSTRING(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append("))) + LEN(");
                builder.Visit(args[1]);
                builder.Append(") -1) END) END");
            }
            else
            {
                builder.Append("  (LEN(");
                builder.Visit(args[0]);
                builder.Append(")- (CHARINDEX(REVERSE(");
                builder.Visit(args[1]);
                builder.Append("),REVERSE(");
                builder.Visit(args[0]);
                builder.Append(")) + LEN(");
                builder.Visit(args[1]);
                builder.Append(") )) END ");
            }
        }
    }
}
