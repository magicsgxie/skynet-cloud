using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class SubstringFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null || (args.Length != 2 && args.Length != 3))
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "substring", "", "2 or 3"));
            }
            builder.Append("CASE WHEN LENGTH(");
            builder.Visit(args[1]);
            builder.Append(") > LENGTH(");
            builder.Visit(args[0]);
            builder.Append(") THEN NULL ELSE SUBSTR(");
            builder.Visit(args[0]);
            builder.Append(",").Visit(args[1]);
            if (args.Length == 2)
            {
                builder.Append(",LENGTH(").Visit(args[0]);
                builder.Append(")");

            }
            else
            {
                builder.Append(",").Visit(args[2]);
            }
            builder.Append(") END");
        }
    }
}
