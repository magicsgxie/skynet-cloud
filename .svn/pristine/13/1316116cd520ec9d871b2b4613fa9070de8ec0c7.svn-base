using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class DateDiffFuncionView1 : IFunctionView
    {
        readonly DateParts Type;
        public DateDiffFuncionView1(DateParts type)
        {
            Type = type;
        }
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("function ' datediff takes 2 arguments.");
            switch (Type)
            {
                case DateParts.Year:
                    builder.Append(" YEAR(");
                    builder.Visit(args[1]);
                    builder.Append(") - YEAR(");
                    builder.Visit(args[0]);
                    builder.Append(")");
                    break;
                case DateParts.Month:
                    builder.Append(" (YEAR(");
                    builder.Visit(args[1]);
                    builder.Append(") - YEAR(");
                    builder.Visit(args[0]);
                    builder.Append("))*12 + ");
                    builder.Append(" MONTH(");
                    builder.Visit(args[1]);
                    builder.Append(") - MONTH(");
                    builder.Visit(args[0]);
                    builder.Append(")");
                    break;
                case DateParts.Day:
                    builder.Append(" DATEDIFF(");
                    builder.Visit(args[1]);
                    builder.Append(",");
                    builder.Visit(args[0]);
                    builder.Append(")");
                    break;
                case DateParts.Hour:
                    builder.Append(" DATEDIFF(");
                    builder.Visit(args[1]);
                    builder.Append(",");
                    builder.Visit(args[0]);
                    builder.Append(")*24");
                    builder.Append("  + ");
                    builder.Append(" (HOUR( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" HOUR(");
                    builder.Visit(args[0]);
                    builder.Append("))");
                    break;
                case DateParts.Minute:
                    builder.Append(" DATEDIFF(");
                    builder.Visit(args[1]);
                    builder.Append(",");
                    builder.Visit(args[0]);
                    builder.Append(")*24*60");
                    builder.Append("  + ");
                    builder.Append(" (HOUR( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" HOUR(");
                    builder.Visit(args[0]);
                    builder.Append("))*60");
                    builder.Append(" + ");
                    builder.Append(" (MINUTE( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" MINUTE(");
                    builder.Visit(args[0]);
                    builder.Append("))");
                    break;
                case DateParts.Second:
                    builder.Append(" DATEDIFF(");
                    builder.Visit(args[1]);
                    builder.Append(",");
                    builder.Visit(args[0]);
                    builder.Append(")*24*60*60");
                    builder.Append("  + ");
                    builder.Append(" (HOUR( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" HOUR(");
                    builder.Visit(args[0]);
                    builder.Append("))*60*60");
                    builder.Append(" + ");
                    builder.Append(" (MINUTE( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" MINUTE(");
                    builder.Visit(args[0]);
                    builder.Append("))*60");
                    builder.Append(" + ");
                    builder.Append(" (SECOND( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" SECOND(");
                    builder.Visit(args[0]);
                    builder.Append("))");
                    break;
                case DateParts.Millisecond:
                    builder.Append(" DATEDIFF(");
                    builder.Visit(args[1]);
                    builder.Append(",");
                    builder.Visit(args[0]);
                    builder.Append(")*24*60*60*1000");
                    builder.Append("  + ");
                    builder.Append(" (HOUR( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" HOUR(");
                    builder.Visit(args[0]);
                    builder.Append("))*60*60*1000");
                    builder.Append(" + ");
                    builder.Append(" (MINUTE( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" MINUTE(");
                    builder.Visit(args[0]);
                    builder.Append("))*60*1000");
                    builder.Append(" + ");
                    builder.Append(" (SECOND( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" SECOND(");
                    builder.Visit(args[0]);
                    builder.Append("))*1000");
                    builder.Append(" + ");
                    builder.Append(" (Millisecond( ");
                    builder.Visit(args[1]);
                    builder.Append(")");
                    builder.Append(" - ");
                    builder.Append(" Millisecond(");
                    builder.Visit(args[0]);
                    builder.Append("))*1000");
                    break;
                case DateParts.Week:
                    builder.Append(" FLOOR(DATEDIFF(");
                    builder.Visit(args[1]);
                    builder.Append(",");
                    builder.Visit(args[0]);
                    builder.Append(")/7)");
                    break;
            }
        }
    }
}
