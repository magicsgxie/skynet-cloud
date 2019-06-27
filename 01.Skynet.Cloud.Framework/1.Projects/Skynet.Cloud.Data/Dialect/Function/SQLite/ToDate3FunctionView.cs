using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class ToDate3FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length == 1)
            {
                builder.Append("DATE(STRFTIME('%Y-%m-%d',");
                builder.Visit(args[0]);
                builder.Append("))");
            }
            else
            {
            	 builder.Append("(");
                builder.Visit(args[0]);
                builder.Append(" || '-' || (CASE WHEN ");
                builder.Visit(args[1]);
                builder.Append(" < 10 THEN '0' || ");
                builder.Visit(args[1]);
                builder.Append(" ELSE ");
                builder.Visit(args[1]);
                builder.Append(" END)");
                builder.Append(" || '-' || (CASE WHEN ");
                builder.Visit(args[2]);
                builder.Append(" < 10 THEN '0' || ");
                builder.Visit(args[2]);
                builder.Append(" ELSE ");
                builder.Visit(args[2]);
                builder.Append(" END)");
                builder.Append(")");
//                builder.Append("(");
//                builder.Visit(args[0]);
//                builder.Append(" || '-' || (CASE WHEN ");
//                builder.Visit(args[1]);
//                builder.Append(" < 10 THEN '0' || '");
//                builder.Visit(args[1]);
//                builder.Append("' ELSE '");
//                builder.Visit(args[1]);
//                builder.Append("' END)");
//                builder.Append(" || '-' || (CASE WHEN ");
//                builder.Visit(args[2]);
//                builder.Append(" < 10 THEN '0' || '");
//                builder.Visit(args[2]);
//                builder.Append("' ELSE '");
//                builder.Visit(args[2]);
//                builder.Append("' END)");
//                builder.Append(")");
            }
        }
    }
}
