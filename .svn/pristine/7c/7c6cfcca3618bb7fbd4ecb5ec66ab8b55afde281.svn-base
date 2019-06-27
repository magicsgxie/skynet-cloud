using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class DateAddFunctionView : IFunctionView
    {
        static readonly IFunctionView AddDate = new AddDateFunctionView();
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DATEADD(yyyy, ?2,?1)")},
                        { DateParts.Quarter,FunctionView.Template("DATEADD(q, ?2,?1)")},
                        { DateParts.Month,FunctionView.Template("DATEADD(m, ?2,?1)")},
                        { DateParts.Week,FunctionView.Template("DATEADD(wk, ?2,?1)")},
                        { DateParts.Day,FunctionView.Template("DATEADD(d, ?2,?1)")},
                        { DateParts.Hour,FunctionView.Template("DATEADD(hh, ?2,?1)")},
                        { DateParts.Minute,FunctionView.Template("DATEADD(n, ?2,?1)")},
                        { DateParts.Second,FunctionView.Template("DATEADD(s, ?2,?1)")},
                        { DateParts.DayOfYear,FunctionView.Template("DATEADD(dy, ?2,?1)")},
                        { DateParts.TimeSpan,new AddDateFunctionView()},

                       
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
        	var constExp = args[0] as ConstantExpression;
        	if(constExp == null)
        		constExp = (args[0] as NamedValueExpression).Value as ConstantExpression;
        	
            var datePart = (DateParts)constExp.Value;
            if (datePart == DateParts.TimeSpan)
            {
                AddDate.Render(builder, args);
                return;
            }
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1], args[2]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DateAdd." + datePart, ""));
        }
    }
}
