using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class RemoveFunctionView : IFunctionView
    {
        private static readonly IFunctionView LocateWith2Params = FunctionView.Template("SUBSTR(?1, ?2)");
        private static readonly IFunctionView LocateWith3Params = FunctionView.Template("SUBSTR(?1, ?2, ?3)");

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "remove", "", "2 or 3"));



            if (args.Length == 2)
            {
                args[1] = Expression.Subtract(Expression.Property(args[1], "Value"), Expression.Constant(1, Types.Int32));
                ctx.Append("CASE WHEN LENGTH(");
                ctx.Visit(args[0]);
                ctx.Append(") <= ");
                ctx.Visit(args[1]);
                ctx.Append(" THEN null ELSE SUBSTR(");
                ctx.Visit(args[0]);
                ctx.Append(",1,");
                ctx.Visit(args[1]);
                ctx.Append(") END");
            }
            else
            {

                ctx.Append("CASE WHEN LENGTH(");
                ctx.Visit(args[0]);
                ctx.Append(") < ");
                ctx.Visit(args[1]);
                ctx.Append(" THEN null ELSE REPLACE(");
                ctx.Visit(args[0]);
                ctx.Append(",SUBSTR(");
                ctx.Visit(args[0]);
                ctx.Append(",");
                ctx.Visit(args[1]);
                ctx.Append(",");
                ctx.Visit(args[2]);
                ctx.Append("),'')END");

            }
        }
    }
}
