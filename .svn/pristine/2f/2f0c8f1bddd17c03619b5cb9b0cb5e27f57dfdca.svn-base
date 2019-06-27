using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class DatePartFunctionView : IFunctionView
    {
        protected Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DatePart(year, ?1)")},
                        { DateParts.Quarter,FunctionView.Template("DatePart(quarter, ?1)")},
                        { DateParts.Month,FunctionView.Template("DatePart(month, ?1)")},
                        { DateParts.Day,FunctionView.Template("DatePart(day, ?1)")},
                        { DateParts.Hour,FunctionView.Template("DatePart(hour, ?1)")},
                        { DateParts.Minute,FunctionView.Template("DatePart(minute, ?1)")},
                        { DateParts.Second,FunctionView.Template("DatePart(second, ?1)")},
                        { DateParts.Millisecond,FunctionView.Template("DatePart(ms, ?1)")},
                        { DateParts.DayOfYear,FunctionView.Template("DatePart(dayofyear, ?1)")},
                        //{ DateParts.DayOfWeek,FunctionViews.Template("DatePart(dayofweek, ?1)-1")},
                        { DateParts.Week,FunctionView.Template("DatePart(ww, ?1)")},
                        //{ DateParts.Date,FunctionViews.Standard("Date")},
                    };

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)((args[0] as NamedValueExpression).Value as ConstantExpression).Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
}
