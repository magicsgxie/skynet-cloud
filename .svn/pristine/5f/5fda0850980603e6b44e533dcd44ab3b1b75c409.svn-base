using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class ToDate3FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length == 1)
            {
                builder.Append("DATE_FORMAT(");
                builder.Visit(args[0]);
                builder.Append(",'%Y-%m-%d')");
            }
            else
            {
                builder.Append("STR_TO_DATE(CONCAT(");
                builder.Visit(args[1]);
                builder.Append(" , '/' ,");
                builder.Visit(args[2]);
                builder.Append(" , '/' ,");
                builder.Visit(args[0]);
                builder.Append("),'%m/%d/%Y')");
            }
        }
    }
}
