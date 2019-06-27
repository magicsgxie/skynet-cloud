using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    internal class MsSqlFunctionRegistry : FunctionRegistry
    {
        public MsSqlFunctionRegistry()
        {
            SqlFunctions.Add(FunctionType.Case, FunctionView.Case);
            //SqlFunctions.Add(FunctionType.ToString, FunctionViews.VarArgs("CONVERT(NVARCHAR,", ",", ")"));
            SqlFunctions.Add(FunctionType.Compare, FunctionView.Compare);
            SqlFunctions.Add(FunctionType.NewGuid, FunctionView.Standard("NewID"));
            SqlFunctions.Add(FunctionType.Length, FunctionView.Standard("DataLength"));
        }
        private IDateTimeFunctions dateTimeFunctions = new MsSqlDateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get { return dateTimeFunctions; }
        }

        private IDecimalFunctions decimalFunctions = new MsSqlDecimalFunctions();
        protected override IDecimalFunctions Decimal
        {
            get { return decimalFunctions; }
        }

        private IMathFunctions mathFunctions = new MsSqlMathFunctions();
        protected override IMathFunctions Math
        {
            get { return mathFunctions; }
        }

        private IStringFunctions stringFunctions = new MsSqlStringFunctions();
        protected override IStringFunctions String
        {
            get { return stringFunctions; }
        }



    }

    internal class MsSqlDateTimeFunctions : IDateTimeFunctions
    {


        public virtual IFunctionView Now
        {

            get { return FunctionView.NoArgs("GETDATE"); }
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
