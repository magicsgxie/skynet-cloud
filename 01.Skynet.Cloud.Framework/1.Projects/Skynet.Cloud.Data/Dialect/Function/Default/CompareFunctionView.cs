using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class CompareFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            builder.Append("(CASE WHEN ");
            builder.Visit(args[0]);
            builder.Append(" = ");
            builder.Visit(args[1]);
            builder.Append(" THEN 0 WHEN ");
            builder.Visit(args[0]);
            builder.Append(" < ");
            builder.Visit(args[1]);
            builder.Append(" THEN -1 ELSE 1 END)");
        }
    }
}
