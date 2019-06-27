
namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class MsSql2005FunctionRegistry : MsSqlFunctionRegistry
    {
        private IDateTimeFunctions dateTimeFunctions = new MsSql2005DateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get
            {
                return dateTimeFunctions;
            }
        }

        class MsSql2005DateTimeFunctions : MsSqlDateTimeFunctions
        {
            public override IFunctionView Now
            {
                get { return FunctionView.NoArgs("sysdatetime"); }
            }
        }
    }
}
