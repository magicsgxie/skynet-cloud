using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class IndexOfFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            //INSTR(descript, 'zhao') = 1
            //instr(1,descript,'zhao')
            if (args == null || (args.Length != 2 && args.Length != 3))
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "indexof()", "", " 2 or 3 "));
            }
            builder.Append("((INSTR(");
            if (args.Length == 2)
                builder.Visit(args[0]);
            else
            {

                builder.Visit(args[2]);
                builder.Append(",");
                builder.Visit(args[0]);
            }
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(")) - 1)");
        }
    }
}
