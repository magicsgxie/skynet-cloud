using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class LastIndexOfFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "LastIndexOf", "", "2 or 3"));
            builder.Append("CASE WHEN INSTR(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(") = 0 then  -1  else");
            if (args.Length == 3)
            {
                builder.Append("(CASE WHEN (INSTR(");
                builder.Append("SUBSTRING(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                //args[2] = Expression.Subtract(Expression.Property(args[2], "Value"), Expression.Constant(1, Types.Int32));
                builder.Visit(args[2]);
                builder.Append("),");
                builder.Visit(args[1]);

                builder.Append(")) = 0 or (");
                builder.Visit(args[2]);
                builder.Append(" > LENGTH(");
                builder.Visit(args[0]);
                builder.Append(")) THEN -1 ELSE LENGTH(SUBSTRING(");

                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append(")) - (INSTR(");
                builder.Append("REVERSE(SUBSTRING(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append(")),");
                builder.Append("REVERSE(");
                builder.Visit(args[1]);
                builder.Append(")) + LENGTH(");
                builder.Visit(args[1]);
                builder.Append(") -1) END ) END");

            }
            else
            {
                builder.Append("  (LENGTH(");
                builder.Visit(args[0]);
                builder.Append(")- (INSTR(");
                builder.Append("REVERSE(");
                builder.Visit(args[0]);
                builder.Append("),REVERSE(");
                builder.Visit(args[1]);
                builder.Append(")) + LENGTH(");
                builder.Visit(args[1]);
                builder.Append(") - 1)) END");
            }
        }
    }
}
