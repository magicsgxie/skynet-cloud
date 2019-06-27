using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
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
                        {DateParts.Millisecond,new DateDiffFuncionView1(DateParts.Millisecond)},
                        {DateParts.Microsecond,FunctionView.NotSupport("DateDiff.Microsecond")},
                        {DateParts.Nanosecond,FunctionView.NotSupport("DateDiff.Nanosecond")},
                        { DateParts.DayOfYear,FunctionView.NotSupport("DateDiff.DayOfYear")},
                        { DateParts.Week,new DateDiffFuncionView1(DateParts.Week)},
                        
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
           var constExp = args[0] as ConstantExpression;
        	if(constExp == null)
        		constExp = (args[0] as NamedValueExpression).Value as ConstantExpression;
        	
            var datePart = (DateParts)constExp.Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1], args[2]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
}
