using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class DateAddFunctionView : IFunctionView
    {
        static readonly IFunctionView AddDate = new AddDateFunctionView();
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 YEAR)")},
                        { DateParts.Month,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 MONTH)")},
                        { DateParts.Day,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 DAY)")},
                        { DateParts.Hour,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 HOUR)")},
                        { DateParts.Minute,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 MINUTE)")},
                        { DateParts.Second,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 SECOND)")},
                        { DateParts.Millisecond,FunctionView.Template("DATE_ADD(?1, INTERVAL ?2 microsecond)")},
                        { DateParts.TimeSpan,new AddDateFunctionView()},
                        
                       
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)(args[0] as ConstantExpression).Value;
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
