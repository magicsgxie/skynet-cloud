
namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class SqlCEDateTimeFunctions : IDateTimeFunctions
    {
        public IFunctionView Now
        {
            get { return FunctionView.NoArgs("GetDate"); }
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
