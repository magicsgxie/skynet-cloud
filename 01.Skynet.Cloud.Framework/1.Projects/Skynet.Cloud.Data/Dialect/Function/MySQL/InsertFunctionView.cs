using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class InsertFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Insert", "", "3"));
            // args[1] = Expression.Constant((int)(args[1] as ConstantExpression).Value + 1);
            ctx.Append("(CASE WHEN ");
            ctx.Visit(args[1]);
            ctx.Append(" > LENGTH(");
            ctx.Visit(args[0]);
            ctx.Append(") THEN null ELSE (INSERT(");
            ctx.Visit(args[0]);
            ctx.Append(",");
            ctx.Visit(args[1]);
            ctx.Append(",0,");
            ctx.Visit(args[2]);
            ctx.Append("))END)");
        }
    }
}
