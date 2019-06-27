using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
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
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "datediff", "", "2"));
            switch (Type)
            {
                case DateParts.Year:
                    builder.Append(" CAST(STRFTIME('%Y',");
                    builder.Visit(args[1]);
                    builder.Append(") AS INT) - ");
                    builder.Append(" CAST(STRFTIME('%Y',");
                    builder.Visit(args[0]);
                    builder.Append(") AS INT)");
                    break;
                case DateParts.Month:
                    builder.Append(" (CAST(STRFTIME('%Y',");
                    builder.Visit(args[1]);
                    builder.Append(") AS INT) - ");
                    builder.Append(" CAST(STRFTIME('%Y',");
                    builder.Visit(args[0]);
                    builder.Append(") AS INT))*12");
                    builder.Append(" + ");
                    builder.Append(" CAST(STRFTIME('%m',");
                    builder.Visit(args[1]);
                    builder.Append(") AS INT) - ");
                    builder.Append(" CAST(STRFTIME('%m',");
                    builder.Visit(args[0]);
                    builder.Append(") AS INT)");
                    break;
                case DateParts.Day:
                    builder.Append(" FLOOR((");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[1]);
                    builder.Append(") - ");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[0]);
                    builder.Append(")))");

                    break;
                case DateParts.Hour:
                    builder.Append(" FLOOR((");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[1]);
                    builder.Append(") - ");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[0]);
                    builder.Append("))*24)");
                    break;
                case DateParts.Minute:
                    builder.Append(" FLOOR((");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[1]);
                    builder.Append(") - ");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[0]);
                    builder.Append("))*24*60)");
                    break;
                case DateParts.Second:
                    builder.Append(" FLOOR((");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[1]);
                    builder.Append(") - ");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[0]);
                    builder.Append("))*24*60*60)");
                    break;
                case DateParts.Millisecond:
                    builder.Append(" FLOOR((");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[1]);
                    builder.Append(") - ");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[0]);
                    builder.Append("))*24*60*60*1000)");
                    break;
                case DateParts.Week:
                    builder.Append(" FLOOR(ROUND((");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[1]);
                    builder.Append(") - ");
                    builder.Append(" JULIANDAY(");
                    builder.Visit(args[0]);
                    builder.Append(")) , 0)/7)");
                    break;
            }
        }
    }
}
