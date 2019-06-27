using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class PadLeftFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            //(CASE WHEN LEN(userName) <= 3 THEN userName WHEN LEN(userName) > 3 THEN SPACE(6-LEN(userName))+userName END) = '  uuuu'
            if (args.Length != 2 && args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "PadLeft", "", "2 or 3"));
            }
            ctx.Append("CASE WHEN LENGTH(");
            ctx.Visit(args[0]);
            ctx.Append(") > ");
            ctx.Visit(args[1]);
            ctx.Append(" THEN ");
            ctx.Visit(args[0]);
            ctx.Append(" ELSE ( ");
            if (args.Length == 2)
            {
                ctx.Append("REPLICATE(' ',");
                ctx.Visit(args[1]);
                ctx.Append(" - LENGTH(");
                ctx.Visit(args[0]);
                ctx.Append("))");
            }
            else
            {
                ctx.Append("REPLICATE(");
                ctx.Visit(args[2]);
                ctx.Append(",");
                ctx.Visit(args[1]);
                ctx.Append(" - LENGTH(");
                ctx.Visit(args[0]);
                ctx.Append("))");
            }
            ctx.Append("||");
            ctx.Visit(args[0]);
            ctx.Append(")");
            ctx.Append(" END");
        }
    }
}
