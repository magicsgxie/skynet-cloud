using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class RightFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Right", "", "2"));
            //rightstr(t0.descript,(CASE WHEN charindex('a',t0.descript) =0 THEN 0 
            //else (length(t0.descript)- (charindex('a',t0.descript)+length('a'))+1) END))
            ctx.Append("RIGHTSTR(");
            ctx.Visit(args[0]);
            ctx.Append(",(CASE WHEN CHARINDEX(");
            ctx.Visit(args[1]);
            ctx.Append(",");
            ctx.Visit(args[0]);
            ctx.Append(") = 0 THEN 0 ELSE (LENGTH(");
            ctx.Visit(args[0]);
            ctx.Append(") - (CHARINDEX(");
            ctx.Visit(args[1]);
            ctx.Append(",");
            ctx.Visit(args[0]);
            ctx.Append(") + LENGTH(");
            ctx.Visit(args[1]);
            ctx.Append("))+1) END))");
        }
    }
}
