using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class LocateFunction : IFunctionView
    {
        private static readonly IFunctionView LocateWith2Params = FunctionView.Template("instr(?1, ?2)");

        private static readonly IFunctionView LocateWith3Params = FunctionView.Template("instr(?1, ?2, ?3)");


        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "locate", "", " 2 or 3 "));
            if (args.Length == 2)
                LocateWith2Params.Render(visitor, args);
            else
                LocateWith3Params.Render(visitor, args);
        }
    }
}
