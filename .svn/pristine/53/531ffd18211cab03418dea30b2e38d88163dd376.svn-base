using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class LastIndexOfFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "LastIndexOf", "", "2 or 3"));
            ctx.Append("CASE WHEN CHARINDEX(");
            ctx.Visit(args[1]);
            ctx.Append(",");
            ctx.Visit(args[0]);
            ctx.Append(") = 0 THEN  -1  ELSE");
            if (args.Length == 3)
            {
                ctx.Append("(CASE WHEN (CHARINDEX(");
                ctx.Visit(args[1]);
                ctx.Append(",SUBSTR(");
                ctx.Visit(args[0]);
                ctx.Append(",1,");
                ctx.Visit(args[2]);

                ctx.Append(")) = 0) OR (");
                ctx.Visit(args[2]);
                ctx.Append(" > LENGTH(");
                ctx.Visit(args[0]);
                ctx.Append(")) THEN -1 ELSE LENGTH(SUBSTR(");

                ctx.Visit(args[0]);
                ctx.Append(",1,");
                ctx.Visit(args[2]);
                ctx.Append(")) - (CHARINDEX(REVERSE(");
                ctx.Visit(args[1]);
                ctx.Append("),REVERSE(SUBSTR(");
                ctx.Visit(args[0]);
                ctx.Append(",1,");
                ctx.Visit(args[2]);
                ctx.Append("))) + LENGTH(");
                ctx.Visit(args[1]);
                ctx.Append(") -1) END) END");
            }
            else
            {
                ctx.Append("  (LENGTH(");
                ctx.Visit(args[0]);
                ctx.Append(")- (CHARINDEX(REVERSE(");
                ctx.Visit(args[1]);
                ctx.Append("),REVERSE(");
                ctx.Visit(args[0]);
                ctx.Append(")) + LENGTH(");
                ctx.Visit(args[1]);
                ctx.Append(") - 1)) END ");
            }
        }
    }
}
