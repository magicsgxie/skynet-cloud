using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class ToDate3FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            builder.Append("CDate(");
            builder.Visit(args[0]);
            builder.Append(" & '/' & ");
            builder.Visit(args[1]);
            builder.Append(" & '/' & ");
            builder.Visit(args[2]);
            builder.Append(")");
        }
    }
}
