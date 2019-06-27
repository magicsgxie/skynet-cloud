using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class DatePartFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'YYYY'))")},
                        { DateParts.Quarter,FunctionView.Template("TO_NUMBER(TO_CHAR(?1, 'Q'))")},
                        { DateParts.Month,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'MM'))")},
                        { DateParts.Day,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'DD'))")},
                        { DateParts.Hour,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'HH24'))")},
                        { DateParts.Minute,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'MI'))")},
                        { DateParts.Second,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'SS'))")},
                        { DateParts.Millisecond,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'FF'))")},
                        { DateParts.DayOfYear,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'DDD'))")},
                        { DateParts.DayOfWeek,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'D'))-1")},
                        { DateParts.Week,FunctionView.Template("TO_NUMBER(TO_CHAR(?1,'WW'))")},
                        { DateParts.Date,FunctionView.Template("TO_DATE(TO_CHAR(?1,'YYYY-MM-DD'),'YYYY-MM-DD')")},
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
