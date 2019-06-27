
namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class MsSqlMathFunctions : IMathFunctions
    {
        public IFunctionView Random
        {
            get { return FunctionView.NoArgs("rand"); }
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
            get { return FunctionView.Standard("atn2"); }
        }

        public IFunctionView Ceiling
        {
            get { return FunctionView.Standard("ceiling"); }
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
            get { return FunctionView.Standard("power"); }
        }

        public IFunctionView Round
        {
            get
            {
                return FunctionView.Proxy((builder, args) =>
                {
                    if (args.Length == 1)
                    {
                        builder.Append("ROUND(");
                        builder.Visit(args[0]);
                        builder.Append(", 0)");
                    }
                    else if (args.Length == 2 && args[1].Type == typeof(int))
                    {
                        builder.Append("ROUND(");
                        builder.Visit(args[0]);
                        builder.Append(", ");
                        builder.Visit(args[1]);
                        builder.Append(")");
                    }
                });
            }
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
            get { return FunctionView.NotSupport("Sinh"); }
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
            get { return FunctionView.NotSupport("Tanh"); }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.Template("ROUND(?1,0,1)"); }
        }
    }
}
