using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class DatePartFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Standard("YEAR")},
                        { DateParts.Quarter,FunctionView.Standard("QUARTER")},
                        { DateParts.Month,FunctionView.Standard("MONTH")},
                        { DateParts.Day,FunctionView.Standard("DAY")},
                        { DateParts.Hour,FunctionView.Standard("HOUR")},
                        { DateParts.Minute,FunctionView.Standard("MINUTE")},
                        { DateParts.Second,FunctionView.Standard("SECOND")},
                        { DateParts.Millisecond,FunctionView.Template("(Millisecond(?1)/1000)")},
                        { DateParts.DayOfWeek,FunctionView.Template("DayOfWeek(?1)-1")},
                        { DateParts.DayOfYear,FunctionView.Template("(DayOfYear(?1))")},
                        
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)(args[0] as ConstantExpression).Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
}
