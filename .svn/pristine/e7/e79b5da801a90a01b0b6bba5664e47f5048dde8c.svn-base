using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class IsNullOrWhiteSpaceFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            //if (args.Length != 1)
            //    throw new NotSupportedException("'IsNullOrWhiteSpace' function takes  1 arguments");
            var isNot = args.Length == 2;
            if (isNot)
            {
                builder.Append("(");
                builder.Visit(args[0]);
                builder.Append(" IS NOT NULL OR ");
                builder.Visit(args[0]);
                builder.Append(" <> '' OR LTRIM(");
                builder.Visit(args[0]);
                builder.Append(") = '')");
            }
            else
            {
                builder.Append("(");
                builder.Visit(args[0]);
                builder.Append(" IS NULL OR ");
                builder.Visit(args[0]);
                builder.Append(" = '' OR LTRIM(");
                builder.Visit(args[0]);
                builder.Append(") = '')");
            }
        }
    }
}
