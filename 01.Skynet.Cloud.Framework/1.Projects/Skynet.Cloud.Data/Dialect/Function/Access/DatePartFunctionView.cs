using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class DatePartFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DatePart('yyyy', ?1)")},
                        { DateParts.Quarter,FunctionView.Template("DatePart('q', ?1)")},
                        { DateParts.Month,FunctionView.Template("DatePart('m', ?1)")},
                        { DateParts.Day,FunctionView.Template("DatePart('d', ?1)")},
                        { DateParts.Hour,FunctionView.Template("DatePart('h', ?1)")},
                        { DateParts.Minute,FunctionView.Template("DatePart('n', ?1)")},
                        { DateParts.Second,FunctionView.Template("DatePart('s', ?1)")},
                        { DateParts.DayOfYear,FunctionView.Template("DatePart('y', ?1)")},
                        { DateParts.DayOfWeek,FunctionView.Template("DatePart('w', ?1)-1")},
                        { DateParts.Week,FunctionView.Template("DatePart('ww', ?1)")},
                        { DateParts.Date,FunctionView.Standard("Date")},
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
