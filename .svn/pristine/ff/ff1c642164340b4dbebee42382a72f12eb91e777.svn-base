using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Postgres
{
    class PostgresFunctionRegistry : FunctionRegistry
    {
        private IDateTimeFunctions dateTimeFunctions = new FirebirdDateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get { return dateTimeFunctions; }
        }

        private IDecimalFunctions decimalFunctions = new FirebirdDecimalFunctions();
        protected override IDecimalFunctions Decimal
        {
            get { return decimalFunctions; }
        }

        private IMathFunctions mathFunctions = new FirebirdMathFunctions();
        protected override IMathFunctions Math
        {
            get { return mathFunctions; }
        }

        private IStringFunctions stringFunctions = new FirebirdStringFunctions();
        protected override IStringFunctions String
        {
            get { return stringFunctions; }
        }
    }
    enum DatePartType
    {
        Second, Minute, Hour, Day, Month, Year
    }
    class DateAddFunctionView : IFunctionView
    {
        readonly DatePartType Type;
        public DateAddFunctionView(DatePartType type)
        {
            Type = type;
        }

        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "dateadd", "", "2"));

            var addPart = (args[1] as ConstantExpression).Value;

            visitor.Append("(");
            visitor.Visit(args[0]);
            visitor.Append(" + interval '" + addPart.ToString() + "' " + Type.ToString() + ")");
        }
    }
    class DayOfWeekFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "dateadd", "", "1"));

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

    class DateAddsFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,new DateAddFunctionView(DatePartType.Year)},
                        { DateParts.Quarter,FunctionView.NotSupport("DateAdd.Quarter")},
                        { DateParts.Month,FunctionView.Standard("add_months")},
                        { DateParts.Week,FunctionView.NotSupport("DateAdd.Week")},
                        { DateParts.Day,new DateAddFunctionView(DatePartType.Day)},
                        { DateParts.Hour,new DateAddFunctionView(DatePartType.Hour)},
                        { DateParts.Minute,new DateAddFunctionView(DatePartType.Minute)},
                        { DateParts.Second,new DateAddFunctionView(DatePartType.Second)},
                        { DateParts.Date,FunctionView.NotSupport("DateAdd.Date")},
                       
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
    class ToDate3FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {

        }
    }
    class ToDate6FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {


        }
    }
    class DatePartFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
                    {
                        { DateParts.Year,FunctionView.Template("to_char(?1,'yyyy')")},
                        { DateParts.Quarter,FunctionView.Template("DatePart('q', ?1)")},
                        { DateParts.Month,FunctionView.Template("to_char(?1,'mm')")},
                        { DateParts.Day,FunctionView.Template("to_char(?1,'dd')")},
                        { DateParts.Hour,FunctionView.Template("to_char(?1,'hh')")},
                        { DateParts.Minute,FunctionView.Template("to_char(?1,'mi')")},
                        { DateParts.Second,FunctionView.Template("to_char(?1,'ss')")},
                        { DateParts.DayOfYear,FunctionView.Template("to_char(?1,'DDD')")},
                        { DateParts.DayOfWeek,new DayOfWeekFunctionView()},
                        { DateParts.Week,FunctionView.NotSupport("DatePart.Week")},
                        { DateParts.Date,FunctionView.NotSupport("DatePart.Date")},
                    };
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var datePart = (DateParts)(args[0] as ConstantExpression).Value;
            IFunctionView f;
            if (functions.TryGetValue(datePart, out f))
                f.Render(builder, args[1]);
            else
                throw new NotSupportedException(string.Format(Res.NotSupported, "The 'DatePart." + datePart, ""));
        }
    }
    class ToTimeFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {

        }
    }
    class DateDiffFunctionView : IFunctionView
    {
        static readonly Dictionary<DateParts, IFunctionView> functions = new Dictionary<DateParts, IFunctionView>
        {
            //{ DateParts.Year,FunctionViews.Template("DateDiff('yyyy',?1,?2)")},
            //{ DateParts.Month,FunctionViews.Template("DateDiff('m',?1,?2)")},
            //{ DateParts.Day,FunctionViews.Template("DateDiff('d',?1,?2)")},
            //{ DateParts.Hour,FunctionViews.Template("DateDiff('h',?1,?2)")},
            //{ DateParts.Minute,FunctionViews.Template("DateDiff('n',?1,?2)")},
            //{ DateParts.Second,FunctionViews.Template("DateDiff('s',?1,?2)")},
            //{ DateParts.DayOfYear,FunctionViews.Template("DateDiff('y',?1,?2)")},
            //{ DateParts.Week,FunctionViews.Template("DateDiff('ww',?1,?2)")},

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
    class FirebirdDateTimeFunctions : IDateTimeFunctions
    {
        //public IFunctionView Add
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView AddDays
        //{
        //    get { return new DateAddFunctionView(DatePartType.Day); }
        //}

        //public IFunctionView AddHours
        //{
        //    get { return new DateAddFunctionView(DatePartType.Hour); }
        //}

        //public IFunctionView AddMilliseconds
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView AddMinutes
        //{
        //    get { return new DateAddFunctionView(DatePartType.Minute); }
        //}
        //public IFunctionView AddMonths
        //{
        //    get { return FunctionViews.Standard("add_months"); }
        //}
        //public IFunctionView AddSeconds
        //{
        //    get { return new DateAddFunctionView(DatePartType.Second); }
        //}

        //public IFunctionView AddTicks
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView AddYears
        //{
        //    get { return new DateAddFunctionView(DatePartType.Year); }
        //}

        //public IFunctionView DaysInMonth
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView FromBinary
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView IsLeapYear
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView Day
        //{
        //    get { return FunctionViews.Template("to_char(?1,'dd')"); }
        //}

        //public IFunctionView DayOfWeek
        //{
        //    get { return new DayOfWeekFunctionView(); }
        //}

        //public IFunctionView DayOfYear
        //{
        //    get { return FunctionViews.Template("to_char(?1,'DDD')"); }
        //}

        //public IFunctionView Hour
        //{
        //    get { return FunctionViews.Template("to_char(?1,'hh')"); }
        //}

        //public IFunctionView Millisecond
        //{
        //    get { return FunctionViews.Template("datepart(millisecond, ?1)"); }
        //}

        //public IFunctionView Minute
        //{
        //    get { return FunctionViews.Template("to_char(?1,'mi')"); }
        //}

        //public IFunctionView Month
        //{
        //    get { return FunctionViews.Template("to_char(?1,'mm')"); }
        //}

        //public IFunctionView Now
        //{
        //    get { return FunctionViews.Proxy((m, n) => m.Append("SYSDATE")); }
        //}

        //public IFunctionView New
        //{
        //    get { return FunctionViews.Standard("TO_DATE"); }
        //}

        //public IFunctionView Second
        //{
        //    get { return FunctionViews.Template("to_char(?1,'ss')"); }
        //}

        //public IFunctionView Subtract
        //{
        //    get { return FunctionViews.VarArgs("(", "-", ")"); }
        //}

        //public IFunctionView Ticks
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView TimeOfDay
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView Today
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView UtcNow
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}

        //public IFunctionView Year
        //{
        //    get { return FunctionViews.Template("to_char(?1,'yyyy')"); }
        //}

        //public IFunctionView op_Subtract
        //{
        //    get { return FunctionViews.NotSupport(); }
        //}



        public IFunctionView Now
        {
            get { return FunctionView.Proxy((m, n) => m.Append("SYSDATE")); }
        }

        public IFunctionView ToDate
        {
            get { return FunctionView.Standard("TO_DATE"); }
        }

        public IFunctionView ToDateTime
        {
            get { return FunctionView.Standard("TO_DATE"); }
        }

        public IFunctionView ToTime
        {
            get { return FunctionView.NotSupport("ToTime"); }
        }

        public IFunctionView DateAdd
        {
            get { return new DateAddsFunctionView(); }
        }

        public IFunctionView DateDiff
        {
            get { return FunctionView.NotSupport("DateDiff"); }
        }

        public IFunctionView DatePart
        {
            get { return new DatePartFunctionView(); }
        }
    }
    class FirebirdDecimalFunctions : IDecimalFunctions
    {
        public IFunctionView Add
        {
            get { return FunctionView.NotSupport("Add"); }
        }

        public IFunctionView Subtract
        {
            get { return FunctionView.NotSupport("Subtract"); }
        }

        public IFunctionView Multiply
        {
            get { return FunctionView.NotSupport("Multiply"); }
        }

        public IFunctionView Divide
        {
            get { return FunctionView.NotSupport("Divide"); }
        }

        public IFunctionView Remainder
        {
            get { return FunctionView.NotSupport("Remainder"); }
        }

        public IFunctionView Negate
        {
            get { return FunctionView.NotSupport("Negate"); }
        }


    }
    class FirebirdMathFunctions : IMathFunctions
    {
        public IFunctionView Random
        {
            get { return FunctionView.NotSupport("Random"); }
        }

        public IFunctionView Abs
        {
            get { return FunctionView.Standard("abs"); }
        }

        public IFunctionView Acos
        {
            get { return FunctionView.Standard("acos"); }
        }

        public IFunctionView Asin
        {
            get { return FunctionView.Template("cast(asin(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Atan
        {
            get { return FunctionView.Template("cast(atan(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Atan2
        {
            get { return FunctionView.Template("cast(atan2(?1,?2) as NUMBER(19,9))"); }
        }

        public IFunctionView Ceiling
        {
            get { return FunctionView.Standard("ceil"); }
        }

        public IFunctionView Cos
        {
            get { return FunctionView.Template("cast(cos(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Cosh
        {
            get { return FunctionView.Standard("cosh"); }
        }

        public IFunctionView Exp
        {
            get { return FunctionView.Template("cast(exp(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Floor
        {
            get { return FunctionView.Standard("floor"); }
        }

        public IFunctionView Log
        {
            get { return FunctionView.Template("cast(ln(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Log10
        {
            get { return FunctionView.Template("cast(log(10,?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Power
        {
            get { return FunctionView.Standard("power"); }
        }

        public IFunctionView Round
        {
            get { return FunctionView.Standard("round"); }
        }

        public IFunctionView Sign
        {
            get { return FunctionView.Standard("sign"); }
        }

        public IFunctionView Sin
        {
            get { return FunctionView.Standard("sin"); }
        }

        public IFunctionView Sinh
        {
            get { return FunctionView.Standard("sinh"); }
        }

        public IFunctionView Sqrt
        {
            get { return FunctionView.Standard("sqrt"); }
        }

        public IFunctionView Tan
        {
            get { return FunctionView.Template("cast(tan(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Tanh
        {
            get { return FunctionView.Template("cast(tanh(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.Standard("trunc"); }
        }
    }
    class LocateFunction : IFunctionView
    {
        private static readonly IFunctionView LocateWith2Params = FunctionView.Template("instr(?1, ?2)");

        private static readonly IFunctionView LocateWith3Params = FunctionView.Template("instr(?1, ?2, ?3)");


        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "locate", "", "2 or 3"));
            if (args.Length == 2)
                LocateWith2Params.Render(visitor, args);
            else
                LocateWith3Params.Render(visitor, args);
        }
    }

    class RemoveFunctionView : IFunctionView
    {
        private static readonly IFunctionView LocateWith2Params = FunctionView.Template("REPLACE(?1, SUBSTR(?1, ?2), '')");
        private static readonly IFunctionView LocateWith3Params = FunctionView.Template("REPLACE(?1, SUBSTR(?1, ?2, ?3), '')");

        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "remove", "", "2 or 3"));
            if (args.Length == 2)
                LocateWith2Params.Render(visitor, args);
            else
                LocateWith3Params.Render(visitor, args);
        }
    }
    class FirebirdStringFunctions : IStringFunctions
    {

        public IFunctionView Contains
        {
            get
            {
                return FunctionView.Contains;
            }
        }

        public IFunctionView StartsWith
        {
            get
            {
                return FunctionView.StartsWith;
            }
        }

        public IFunctionView EndsWith
        {
            get
            {
                return FunctionView.EndsWith;
            }
        }
        public IFunctionView Like
        {
            get { return FunctionView.Like; }
        }

        public IFunctionView Concat
        {
            get { return FunctionView.VarArgs("(", "||", ")"); }
        }

        public IFunctionView IndexOf
        {
            get { return new LocateFunction(); }
        }

        public IFunctionView Insert
        {
            get { return FunctionView.NotSupport("Insert"); }
        }

        public IFunctionView LastIndexOf
        {
            get { return FunctionView.NotSupport("LastIndexOf"); }
        }

        public IFunctionView LeftOf
        {
            get { return FunctionView.Template("substr(?1, 1, ?2)"); }
        }

        public IFunctionView Length
        {
            get { return FunctionView.Standard("length"); }
        }

        public IFunctionView PadLeft
        {
            get { return FunctionView.NotSupport("PadLeft"); }
        }

        public IFunctionView PadRight
        {
            get { return FunctionView.NotSupport("PadRight"); }
        }

        public IFunctionView Remove
        {
            get { return new RemoveFunctionView(); }
        }

        public IFunctionView Replace
        {
            get { return FunctionView.Standard("replace"); }
        }

        public IFunctionView Reverse
        {
            get { return FunctionView.NotSupport("Reverse"); }
        }

        public IFunctionView RightOf
        {
            get { return FunctionView.Template("substr(?1, -?2)"); }
        }

        public IFunctionView Substring
        {
            get { return FunctionView.Standard("substr"); }
        }

        public IFunctionView ToLower
        {
            get { return FunctionView.Standard("lower"); }
        }

        public IFunctionView ToUpper
        {
            get { return FunctionView.Standard("upper"); }
        }

        public IFunctionView Trim
        {
            get { return FunctionView.Standard("trim"); }
        }

        public IFunctionView TrimEnd
        {
            get { return FunctionView.Standard("rtrim"); }
        }

        public IFunctionView TrimStart
        {
            get { return FunctionView.Standard("ltrim"); }
        }

        public IFunctionView IsNullOrWhiteSpace
        {
            get { return FunctionView.IsNullOrWhiteSpace; }
        }

        public IFunctionView IsNullOrEmpty
        {
            get { return FunctionView.IsNullOrEmpty; }
        }
    }
}
