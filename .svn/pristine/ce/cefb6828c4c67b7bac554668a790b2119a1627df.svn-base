using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class AddDateFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var flag = args.Length == 3 ? "" : "-";
            var value = (TimeSpan)(args[2] as ConstantExpression).Value;
            builder.Append("(");
            builder.Visit(args[1]);
            builder.AppendFormat(" + INTERVAL '{4}{0}' DAY + INTERVAL '{4}{1}' HOUR + INTERVAL '{4}{2}' MINUTE + INTERVAL '{4}{3}' SECOND"
                              , value.Days
                              , value.Hours
                              , value.Minutes
                              , value.Seconds
                              , flag);
            builder.Append(")");
        }
    }
}
