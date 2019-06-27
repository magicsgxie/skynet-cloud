using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class DateDiffFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
        {
            //{ DateParts.Year,FunctionViews.Standard("DateDiff('yyyy',?1,?2)")},
            //{ DateParts.Month,FunctionViews.Standard("DateDiff('m',?1,?2)")},
            //{ DateParts.Day,FunctionViews.Standard("DateDiff('d',?1,?2)")},
            //{ DateParts.Hour,FunctionViews.Standard("DateDiff('h',?1,?2)")},
            //{ DateParts.Minute,FunctionViews.Standard("DateDiff('m',?1,?2)")},
            //{ DateParts.Second,FunctionViews.Standard("DateDiff('s',?1,?2)")},
            //{ DateParts.DayOfYear,FunctionViews.Standard("DateDiff('y',?1,?2)")},
            //{ DateParts.Week,FunctionViews.Standard("DateDiff('ww',?1,?2)")},

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
