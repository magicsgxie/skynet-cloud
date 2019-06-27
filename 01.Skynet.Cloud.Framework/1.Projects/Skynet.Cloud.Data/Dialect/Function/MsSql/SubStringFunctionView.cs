using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class SubStringFunctionView : IFunctionView
    {
        //static readonly IFunctionView View = FunctionViews.Template("substring(?1, ?2, ?3)");
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 3 && args.Length != 2)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "substring", "", "2 or 3"));

            builder.Append("SUBSTRING(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            if (args.Length == 2)
            {
                builder.Append(",LEN(");
                builder.Visit(args[0]);
                builder.Append("))");
            }
            else
            {
                builder.Append(",");
                builder.Visit(args[2]);
                builder.Append(")");
            }

        }
    }
}
