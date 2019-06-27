using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class FirebirdFunctionRegistry : FunctionRegistry
    {
        public FirebirdFunctionRegistry()
        {
            SqlFunctions.Add(FunctionType.NewGuid, FunctionView.Standard("Gen_Uuid"));
            SqlFunctions.Add(FunctionType.Length, FunctionView.Standard("Octet_Length"));
        }
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
            get { return FunctionView.NotSupport("ToDate"); }
        }

        public IFunctionView ToDateTime
        {
            get { return FunctionView.NotSupport("ToDateTime"); }
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
            get { return FunctionView.NotSupport("DatePart"); }
        }
    }
}
