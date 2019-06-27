using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class ToDate6FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
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
            builder.Append(" || ' ' || (CASE WHEN ");
            builder.Visit(args[3]);
            builder.Append(" < 10 THEN '0' || ");
            builder.Visit(args[3]);
            builder.Append(" ELSE ");
            builder.Visit(args[3]);
            builder.Append(" END)");
            builder.Append(" || ':' || (CASE WHEN ");
            builder.Visit(args[4]);
            builder.Append(" < 10 THEN '0' || ");
            builder.Visit(args[4]);
            builder.Append(" ELSE ");
            builder.Visit(args[4]);
            builder.Append(" END)");
            builder.Append(" || ':' || (CASE WHEN ");
            builder.Visit(args[5]);
            builder.Append(" < 10 THEN '0' || ");
            builder.Visit(args[5]);
            builder.Append(" ELSE ");
            builder.Visit(args[5]);
            builder.Append(" END)");
            builder.Append(")");
//            builder.Append("(");
//            builder.Visit(args[0]);
//            builder.Append(" || '-' || (CASE WHEN ");
//            builder.Visit(args[1]);
//            builder.Append(" < 10 THEN '0' || '");
//            builder.Visit(args[1]);
//            builder.Append("' ELSE '");
//            builder.Visit(args[1]);
//            builder.Append("' END)");
//            builder.Append(" || '-' || (CASE WHEN ");
//            builder.Visit(args[2]);
//            builder.Append(" < 10 THEN '0' || '");
//            builder.Visit(args[2]);
//            builder.Append("' ELSE '");
//            builder.Visit(args[2]);
//            builder.Append("' END)");
//            builder.Append(" || ' ' || (CASE WHEN ");
//            builder.Visit(args[3]);
//            builder.Append(" < 10 THEN '0' || '");
//            builder.Visit(args[3]);
//            builder.Append("' ELSE '");
//            builder.Visit(args[3]);
//            builder.Append("' END)");
//            builder.Append(" || ':' || (CASE WHEN ");
//            builder.Visit(args[4]);
//            builder.Append(" < 10 THEN '0' || '");
//            builder.Visit(args[4]);
//            builder.Append("' ELSE '");
//            builder.Visit(args[4]);
//            builder.Append("' END)");
//            builder.Append(" || ':' || (CASE WHEN ");
//            builder.Visit(args[5]);
//            builder.Append(" < 10 THEN '0' || '");
//            builder.Visit(args[5]);
//            builder.Append("' ELSE '");
//            builder.Visit(args[5]);
//            builder.Append("' END)");
//            builder.Append(")");
        }
    }
}
