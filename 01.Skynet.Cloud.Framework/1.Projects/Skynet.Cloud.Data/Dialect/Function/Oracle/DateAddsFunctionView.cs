using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class DateAddsFunctionView : IFunctionView
    {
        static readonly IFunctionView AddDate = new AddDateFunctionView();
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,new DateAddFunctionView(DateParts.Year)},
                        { DateParts.Quarter,FunctionView.NotSupport("DateAdd.Quarter")},
                        { DateParts.Month,FunctionView.Standard("add_months")},
                        { DateParts.Week,FunctionView.NotSupport("DateAdd.Week")},
                        { DateParts.Day,new DateAddFunctionView(DateParts.Day)},
                        { DateParts.Hour,new DateAddFunctionView(DateParts.Hour)},
                        { DateParts.Minute,new DateAddFunctionView(DateParts.Minute)},
                        { DateParts.Second,new DateAddFunctionView(DateParts.Second)},
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
