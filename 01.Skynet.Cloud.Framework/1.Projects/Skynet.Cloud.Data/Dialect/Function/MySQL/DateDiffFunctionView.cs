using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class DateDiffFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,new DateDiffFuncionView1(DateParts.Year)},
                        { DateParts.Month,new DateDiffFuncionView1(DateParts.Month)},
                        { DateParts.Day,new DateDiffFuncionView1(DateParts.Day)},
                        { DateParts.Hour,new DateDiffFuncionView1(DateParts.Hour)},
                        { DateParts.Minute,new DateDiffFuncionView1(DateParts.Minute)},
                        { DateParts.Second,new DateDiffFuncionView1(DateParts.Second)},
                        //{DateParts.Millisecond,new DateDiffFuncionView1(DatePartType.Millisecond)},
                        //{ DateParts.Microsecond,FunctionViews.Standard("DateDiff('mcs',?1,?2)")},
                        //{ DateParts.Nanosecond,FunctionViews.Standard("DateDiff('ns',?1,?2)")},
                        { DateParts.Week,new DateDiffFuncionView1(DateParts.Week)},
                        { DateParts.DayOfYear,FunctionView.NotSupport("DateAdd.DayOfYear")},
                        { DateParts.Quarter,FunctionView.NotSupport("DateAdd.Quarter")},
                        
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
