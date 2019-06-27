using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class DatePartFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("CAST(STRFTIME('%Y', ?1) AS INT)")},
                        { DateParts.Quarter,new DatePartQuarterFunctionView()},
                        { DateParts.Month,FunctionView.Template("CAST(STRFTIME('%m', ?1) AS INT)")},
                        { DateParts.Day,FunctionView.Template("CAST(STRFTIME('%d', ?1) AS INT)")},
                        { DateParts.Hour,FunctionView.Template("CAST(STRFTIME('%H', ?1) AS INT)")},
                        { DateParts.Minute,FunctionView.Template("CAST(STRFTIME('%M', ?1) AS INT)")},
                        { DateParts.Second,FunctionView.Template("CAST(STRFTIME('%S', ?1) AS INT)")},
                        { DateParts.Millisecond,FunctionView.Template("CAST(STRFTIME('%f', ?1) AS INT)")},
                        { DateParts.DayOfWeek,FunctionView.Template("CAST(STRFTIME('%w', ?1) AS INT)")},
                        { DateParts.DayOfYear,FunctionView.Template("CAST((STRFTIME('%j', ?1)) AS INT)")},
                        { DateParts.Week,FunctionView.Template("DatePart('w', ?1)")},
                        { DateParts.Date,FunctionView.Standard("Date")},

                        
                        
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var constExp = args[0] as ConstantExpression;
        	if(constExp == null)
        		constExp = (args[0] as NamedValueExpression).Value as ConstantExpression;
        	
            var datePart = (DateParts)constExp.Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
}
