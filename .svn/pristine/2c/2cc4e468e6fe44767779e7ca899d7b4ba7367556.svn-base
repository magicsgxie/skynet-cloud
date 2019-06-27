using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class AddDateFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var flag = args.Length == 3 ? "" : "-";
            var value = (TimeSpan)(args[2] as ConstantExpression).Value;
            builder.AppendFormat("DATEADD(DAY,{4}{0},DATEADD(HOUR,{4}{1},DATEADD(MINUTE,{4}{2},DATEADD(SECOND,{4}{3},"
                              , value.Days
                              , value.Hours
                              , value.Minutes
                              , value.Seconds
                              , flag);
            builder.Visit(args[1]);
            builder.Append("))))");
        }
    }
}
