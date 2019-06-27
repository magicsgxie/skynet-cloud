using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class RemoveFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args == null || (args.Length != 2 && args.Length != 3))
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "remove()", "", " 2 or 3 "));
            }
            visitor.Append("IIF((LEN(");
            visitor.Visit(args[0]);
            visitor.Append(") < ");
            visitor.Visit(args[1]);
            visitor.Append("),NULL,REPLACE(");
            visitor.Visit(args[0]);
            visitor.Append(",MID(");
            visitor.Visit(args[0]);
            visitor.Append(",");
            visitor.Visit(args[1]);
            if (args.Length == 2)
            {
                visitor.Append(",8000)");
            }
            else
            {
                visitor.Append(",");
                visitor.Visit(args[2]);
                visitor.Append(")");
            }
            visitor.Append(",''))");
        }
    }
}
