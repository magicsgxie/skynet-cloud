
namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class MySqlMathFunctions : IMathFunctions
    {
        public IFunctionView Random
        {
            get { return FunctionView.NotSupport("Random"); }
        }

        public IFunctionView Abs
        {
            get { return FunctionView.Standard("abs"); }
        }

        public IFunctionView Acos
        {
            get { return FunctionView.Standard("acos"); }
        }

        public IFunctionView Asin
        {
            get { return FunctionView.Standard("asin"); }
        }

        public IFunctionView Atan
        {
            get { return FunctionView.Standard("atan"); }
        }

        public IFunctionView Atan2
        {
            get { return FunctionView.Standard("atan2"); }
        }

        public IFunctionView Ceiling
        {
            get { return FunctionView.Standard("ceil"); }
        }

        public IFunctionView Cos
        {
            get { return FunctionView.Standard("cos"); }
        }

        public IFunctionView Cosh
        {
            get { return FunctionView.Standard("cosh"); }
        }

        public IFunctionView Exp
        {
            get { return FunctionView.Standard("exp"); }
        }

        public IFunctionView Floor
        {
            get { return FunctionView.Standard("floor"); }
        }

        public IFunctionView Log
        {
            get { return FunctionView.Standard("log"); }
        }

        public IFunctionView Log10
        {
            get { return FunctionView.Standard("log10"); }
        }

        public IFunctionView Power
        {
            get { return FunctionView.Standard("pow"); }
        }

        public IFunctionView Round
        {
            get { return FunctionView.Standard("round"); }
        }

        public IFunctionView Sign
        {
            get { return FunctionView.Standard("sign"); }
        }

        public IFunctionView Sin
        {
            get { return FunctionView.Standard("sin"); }
        }

        public IFunctionView Sinh
        {
            get { return FunctionView.Standard("sinh"); }
        }

        public IFunctionView Sqrt
        {
            get { return FunctionView.Standard("sqrt"); }
        }

        public IFunctionView Tan
        {
            get { return FunctionView.Standard("tan"); }
        }

        public IFunctionView Tanh
        {
            get { return FunctionView.Standard("tanh"); }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.Template("truncate(?1,0)"); }
        }
    }
}
