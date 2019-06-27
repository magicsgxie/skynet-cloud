using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class DateAddFunctionView : IFunctionView
    {
        readonly DatePartType Type;
        public DateAddFunctionView(DatePartType type)
        {
            Type = type;
        }

        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "dateadd", "", " 2 "));

            var addPart = (args[1] as ConstantExpression).Value;

            visitor.Append("(");
            visitor.Visit(args[0]);
            visitor.Append(" + interval '" + addPart.ToString() + "' " + Type.ToString() + ")");
        }
    }
}
