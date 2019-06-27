
namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
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
}
