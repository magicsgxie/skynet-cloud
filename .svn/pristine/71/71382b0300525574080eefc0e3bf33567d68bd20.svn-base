using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class RemoveFunctionView : IFunctionView
    {
        public virtual void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "STUFF", "", "2 or 3"));
            }

            visitor.Append("STUFF(");
            visitor.Visit(args[0]);
            visitor.Append(", ");
            visitor.Visit(args[1]);
            visitor.Append(", ");
            if (args.Length == 3)
            {
                visitor.Visit(args[2]);
            }
            else
            {
                visitor.Append("8000");
            }
            visitor.Append(", '')");

        }
    }
}
