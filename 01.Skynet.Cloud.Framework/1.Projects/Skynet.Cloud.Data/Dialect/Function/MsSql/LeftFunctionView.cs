using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class LeftFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Left", "", "2 or 3"));
            }
            builder.Append("Left(");
            builder.Visit(args[0]);
            builder.Append(",(CharIndex(");
            builder.Visit(args[1]);
            builder.Append(",");
            builder.Visit(args[0]);
            builder.Append(")-1))");
        }
    }
}
