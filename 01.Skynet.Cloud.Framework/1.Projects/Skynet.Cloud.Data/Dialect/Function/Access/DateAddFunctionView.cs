using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class DateAddFunctionView : IFunctionView
    {
        static readonly IFunctionView AddDate = new AddDateFunctionView();
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("DATEADD('yyyy', ?2,?1)")},
                        { DateParts.Quarter,FunctionView.Template("DATEADD('q', ?2,?1)")},
                        { DateParts.Month,FunctionView.Template("DATEADD('m', ?2,?1)")},
                        { DateParts.Week,FunctionView.Template("DATEADD('ww', ?2,?1)")},
                        { DateParts.Day,FunctionView.Template("DATEADD('d', ?2,?1)")},
                        { DateParts.Hour,FunctionView.Template("DATEADD('h', ?2,?1)")},
                        { DateParts.Minute,FunctionView.Template("DATEADD('n', ?2,?1)")},
                        { DateParts.Second,FunctionView.Template("DATEADD('s', ?2,?1)")},
                       
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
