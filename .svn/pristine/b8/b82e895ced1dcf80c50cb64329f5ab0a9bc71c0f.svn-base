using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class IsNullOrWhiteSpaceFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 1)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "IsNullOrWhiteSpace", "", "1"));
            builder.Append("(");
            builder.Visit(args[0]);
            builder.Append(" IS NULL OR ");
            builder.Visit(args[0]);
            builder.Append(" = '' OR LTRIM(");
            builder.Visit(args[0]);
            builder.Append(") IS NULL)");
        }
    }
}
