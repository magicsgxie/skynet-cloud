using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class CommonFunction : IFunctionView
    {
        internal string Name;
        const int MaxDecimalDigits = 20;

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            builder.Append("TRUNC(");
            builder.Append(Name);
            builder.Append("(");
            builder.Visit(args[0]);
            builder.Append(")");
            builder.Append(",");
            builder.Append(MaxDecimalDigits);
            builder.Append(")");
        }
    }
}
