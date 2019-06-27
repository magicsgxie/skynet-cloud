
namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class SQLiteDateTimeFunctions : IDateTimeFunctions
    {
        public const string IgonreIntDoubleConvert = "IgonreIntDoubleConvert";

        public IFunctionView Now
        {
            get { return FunctionView.Proxy((v, a) => v.Append("datetime('now')")); }
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
