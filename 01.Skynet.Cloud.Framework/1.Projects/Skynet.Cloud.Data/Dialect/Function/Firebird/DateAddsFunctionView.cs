using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    enum DatePartType
    {
        Second, Minute, Hour, Day, Month, Year
    }
    class DateAddsFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,new DateAddFunctionView(DatePartType.Year)},
                        { DateParts.Month,FunctionView.Standard("add_months")},
                        { DateParts.Day,new DateAddFunctionView(DatePartType.Day)},
                        { DateParts.Hour,new DateAddFunctionView(DatePartType.Hour)},
                        { DateParts.Minute,new DateAddFunctionView(DatePartType.Minute)},
                        { DateParts.Second,new DateAddFunctionView(DatePartType.Second)},
                        { DateParts.Millisecond,FunctionView.NotSupport("DateAdd.Millisecond")},
                        { DateParts.Date,FunctionView.VarArgs("(", "-", ")")},
                       
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)(args[0] as ConstantExpression).Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1], args[2]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DateAdd." + datePart, ""));
        }
    }
}
