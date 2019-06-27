using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class IndexOfFunctionView : IFunctionView
    {
        private static readonly IFunctionView LocateWith2Params = FunctionView.Template("INSTR(?1, ?2) - 1");
        private static readonly IFunctionView LocateWith3Params = FunctionView.Template("LOCATE(?1, ?2, ?3) - 1");

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "IndexOf", "", "2 or 3"));

            var strExpression = args[0];
            if (args.Length == 2)
            {
                LocateWith2Params.Render(ctx, args[0], args[1]);
            }
            else
            {
                //args[2] = Expression.Subtract(Expression.Property(args[2], "Value"), Expression.Constant(1, Types.Int32));
                LocateWith3Params.Render(ctx, args[1], args[0], args[2]);
            }
        }
    }
}
