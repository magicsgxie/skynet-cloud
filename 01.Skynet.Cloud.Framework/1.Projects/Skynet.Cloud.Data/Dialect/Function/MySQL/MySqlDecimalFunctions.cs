
namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class MySqlDecimalFunctions : IDecimalFunctions
    {
        public IFunctionView Add
        {
            get { return FunctionView.VarArgs("(", "+", ")"); }
        }

        public IFunctionView Subtract
        {
            get { return FunctionView.VarArgs("(", "-", ")"); }
        }

        public IFunctionView Multiply
        {
            get { return FunctionView.VarArgs("(", "*", ")"); }
        }

        public IFunctionView Divide
        {
            get { return FunctionView.VarArgs("(", "/", ")"); }
        }

        public IFunctionView Remainder
        {
            get { return FunctionView.VarArgs("(", "%", ")"); }
        }

        public IFunctionView Negate
        {
            get { return FunctionView.Proxy((ctx, args) => ctx.Append("-").Do(() => ctx.Visit(args[0]))); }
        }
    }
}
