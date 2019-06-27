using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class IndexOfFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null || (args.Length != 2 && args.Length != 3))
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "indexof", "", "2 or 3"));
            }
            builder.Append("(INSTR(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            if (args.Length == 3)
            {
                args[2] = Expression.Constant((int)(args[2] as ConstantExpression).Value + 1);
                builder.Append(",");
                builder.Visit(args[2]);
            }
            builder.Append(") - 1)");
        }
    }
}
