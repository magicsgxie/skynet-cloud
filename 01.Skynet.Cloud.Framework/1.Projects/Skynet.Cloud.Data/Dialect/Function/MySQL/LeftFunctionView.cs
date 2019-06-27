using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class LeftFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Left", "", "2"));
            }
            builder.Append("LEFT(");
            builder.Visit(args[0]);
            builder.Append(",(INSTR(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(")-1))");
        }
    }
}
