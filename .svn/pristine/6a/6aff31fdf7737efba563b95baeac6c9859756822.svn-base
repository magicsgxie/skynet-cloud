
namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class FirebirdMathFunctions : IMathFunctions
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
            get { return FunctionView.Template("cast(asin(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Atan
        {
            get { return FunctionView.Template("cast(atan(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Atan2
        {
            get { return FunctionView.Template("cast(atan2(?1,?2) as NUMBER(19,9))"); }
        }

        public IFunctionView Ceiling
        {
            get { return FunctionView.Standard("ceil"); }
        }

        public IFunctionView Cos
        {
            get { return FunctionView.Template("cast(cos(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Cosh
        {
            get { return FunctionView.Standard("cosh"); }
        }

        public IFunctionView Exp
        {
            get { return FunctionView.Template("cast(exp(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Floor
        {
            get { return FunctionView.Standard("floor"); }
        }

        public IFunctionView Log
        {
            get { return FunctionView.Template("cast(ln(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Log10
        {
            get { return FunctionView.Template("cast(log(10,?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Power
        {
            get { return FunctionView.Standard("power"); }
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
            get { return FunctionView.Template("cast(tan(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Tanh
        {
            get { return FunctionView.Template("cast(tanh(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.Standard("trunc"); }
        }
    }
}
