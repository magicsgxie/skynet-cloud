using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class AddDateFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var flag = args.Length == 3 ? "" : "-";
            var value = (TimeSpan)(args[2] as ConstantExpression).Value;
            builder.AppendFormat("DATEADD('d',{4}{0},DATEADD('h',{4}{1},DATEADD('n',{4}{2},DATEADD('s',{4}{3},"
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
