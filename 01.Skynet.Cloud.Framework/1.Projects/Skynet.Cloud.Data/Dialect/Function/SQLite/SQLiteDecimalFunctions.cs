
namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class SQLiteDecimalFunctions : IDecimalFunctions
    {


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
