using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class DayOfWeekFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "dateadd", "", " 1 "));

            visitor.Append("(case ")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'MON' THEN cast(1 as NUMBER(10,0))")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'TUE' THEN cast(2  as NUMBER(10,0))")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'WED' THEN cast(3  as NUMBER(10,0))")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'THU' THEN cast(4  as NUMBER(10,0))")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'FRI' THEN cast(5  as NUMBER(10,0))")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'SAT' THEN cast(6  as NUMBER(10,0))")
                .Append("when to_char(").Do(() => visitor.Visit(args[0])).Append(",'DY') = 'SUN' THEN cast(7  as NUMBER(10,0))")
                .Append(" end)");
        }
    }
}
