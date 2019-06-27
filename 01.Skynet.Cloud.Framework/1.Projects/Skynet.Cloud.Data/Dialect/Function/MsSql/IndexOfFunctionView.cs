using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class IndexOfFunctionView : IFunctionView
    {
        public virtual void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "left", "", "2 or 3"));
            }
            visitor.Append("CHARINDEX(");
            visitor.Visit(args[1]);
            visitor.Append(", ");
            visitor.Visit(args[0]);
            if (args.Length == 3)
            {
                //args[2] = Expression.Constant((int)(args[2] as ConstantExpression).Value + 1);
                visitor.Append(", ");
                visitor.Visit(args[2]);
            }
            //changed
            visitor.Append(") - 1");
        }
    }
}
