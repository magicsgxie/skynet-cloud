using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class DatePartFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("to_char(?1,'yyyy')")},
                        { DateParts.Month,FunctionView.Template("to_char(?1,'mm')")},
                        { DateParts.Day,FunctionView.Template("to_char(?1,'dd')")},
                        { DateParts.Hour,FunctionView.Template("to_char(?1,'hh')")},
                        { DateParts.Minute,FunctionView.Template("to_char(?1,'mi')")},
                        { DateParts.Second,FunctionView.Template("to_char(?1,'ss')")},
                        { DateParts.Millisecond,FunctionView.Template("datepart(millisecond, ?1)")},
                        { DateParts.DayOfWeek,new DayOfWeekFunctionView()},
                        { DateParts.DayOfYear,FunctionView.Template("to_char(?1,'DDD')")},
                        
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)(args[0] as ConstantExpression).Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1], args[2]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
}
