
namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class OracleDateTimeFunctions : IDateTimeFunctions
    {
        public const string IgonreIntDoubleConvert = "IgonreIntDoubleConvert";


        public IFunctionView Now
        {
            get { return FunctionView.Proxy((m, n) => m.Append("SYSDATE")); }
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
            get { return new DateAddsFunctionView(); }
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
