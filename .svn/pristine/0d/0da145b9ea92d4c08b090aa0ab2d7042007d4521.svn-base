
namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class MySqlDateTimeFunctions : IDateTimeFunctions
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
            get { return new ToTimeFunctionView(); }
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
