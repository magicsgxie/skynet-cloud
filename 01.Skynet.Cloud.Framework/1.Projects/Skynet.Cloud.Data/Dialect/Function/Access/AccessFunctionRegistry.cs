
namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class AccessFunctionRegistry : FunctionRegistry
    {
        public AccessFunctionRegistry()
        {
            SqlFunctions.Add(FunctionType.Case, new IFFunctionView());
            // SqlFunctions.Add(FunctionType.ToString, FunctionViews.StandardSafe("cstr", 1));
            SqlFunctions.Add(FunctionType.Compare, CompareFunctionView.Instance);
            SqlFunctions.Add(FunctionType.NewGuid, FunctionView.Standard("newID"));
            SqlFunctions.Add(FunctionType.Length, FunctionView.Standard("len"));
        }
        private IDateTimeFunctions dateTimeFunctions = new AccessDateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get { return dateTimeFunctions; }
        }

        private IDecimalFunctions decimalFunctions = new AccessDecimalFunctions();
        protected override IDecimalFunctions Decimal
        {
            get { return decimalFunctions; }
        }

        private IMathFunctions mathFunctions = new AccessMathFunctions();
        protected override IMathFunctions Math
        {
            get { return mathFunctions; }
        }

        private IStringFunctions stringFunctions = new AccessStringFunctions();
        protected override IStringFunctions String
        {
            get { return stringFunctions; }
        }



    }

    class AccessDateTimeFunctions : IDateTimeFunctions
    {
        public IFunctionView Now
        {
            get { return FunctionView.NoArgs("now"); }
        }

        public IFunctionView ToDate
        {
            get { return new ToDate3FunctionView(); }
        }

        public IFunctionView ToDateTime
        {
            get { return new ToDate6FunctionView(); }
        }

        public IFunctionView ToTime
        {
            get { return FunctionView.NotSupport("ToTime"); }
        }

        public IFunctionView DateAdd
        {
            get { return new DateAddFunctionView(); }
        }

        public IFunctionView DateDiff
        {
            get { return new DateDiffFunctionView(); }
        }

        public IFunctionView DatePart
        {
            get { return new DatePartFunctionView(); }
        }
    }



}
