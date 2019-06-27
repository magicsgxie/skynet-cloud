using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class DateDiffFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DateDiff(yyyy,?1,?2)")},
                        { DateParts.Quarter,FunctionView.Template("DateDiff(q,?1,?2)")},
                        { DateParts.Month,FunctionView.Template("DateDiff(m,?1,?2)")},
                        { DateParts.Day,FunctionView.Template("DateDiff(d,?1,?2)")},
                        { DateParts.Hour,FunctionView.Template("DateDiff(hh,?1,?2)")},
                        { DateParts.Minute,FunctionView.Template("DateDiff(n,?1,?2)")},
                        { DateParts.Second,FunctionView.Template("DateDiff(s,?1,?2)")},
                        { DateParts.DayOfYear,FunctionView.Template("DateDiff(y,?1,?2)")},
                        { DateParts.Week,FunctionView.Template("DateDiff(ww,?1,?2)")},
                        
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)((args[0] as NamedValueExpression).Value as ConstantExpression).Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1], args[2]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
}
