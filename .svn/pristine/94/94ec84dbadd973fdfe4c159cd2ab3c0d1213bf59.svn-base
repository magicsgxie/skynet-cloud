using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class DateAddFunctionView : IFunctionView
    {
        static readonly IFunctionView AddDate = new AddDateFunctionView();
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DateAdd(year, ?2,?1)")},
                        { DateParts.Quarter,FunctionView.Template("DateAdd(q,?2,?1)")},
                        { DateParts.Month,FunctionView.Template("DateAdd(month, ?2,?1)")},
                        { DateParts.Day,FunctionView.Template("DateAdd(day, ?2,?1)")},
                        { DateParts.Hour,FunctionView.Template("DateAdd(hour, ?2,?1)")},
                        { DateParts.Minute,FunctionView.Template("DateAdd(minute, ?2,?1)")},
                        { DateParts.Second,FunctionView.Template("DateAdd(second, ?2,?1)")},
                        { DateParts.Millisecond,FunctionView.Template("DateAdd(ms, ?2,?1)")},
                        { DateParts.TimeSpan,new AddDateFunctionView()},
                       
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)(args[0] as ConstantExpression).Value;
            if (datePart == DateParts.TimeSpan)
            {
                AddDate.Render(builder, args);
                return;
            }
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1], args[2]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DateAdd." + datePart, ""));
        }
    }
}
