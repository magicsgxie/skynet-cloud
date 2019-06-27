using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class ToDate3FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length == 1)
            {
                builder.Append("TO_DATE(TO_CHAR(");
                builder.Visit(args[0]);
                builder.Append(",'YYYY-MM-DD'),'YYYY-MM-DD')");
            }
            else
            {
                builder.Append("TO_DATE((");
                builder.Visit(args[0]);
                builder.Append(") || '-' || (");
                builder.Visit(args[1]);
                builder.Append(") || '-' || (");
                builder.Visit(args[2]);
                builder.Append("),'YYYY-MM-DD')");
            }
        }
    }
}
