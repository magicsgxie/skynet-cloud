using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
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
                    builder.Append(" TO_NUMBER(TO_CHAR(");
                    builder.Visit(args[1]);
                    builder.Append(",'YYYY'))");
                    builder.Append(" - ");
                    builder.Append(" TO_NUMBER(TO_CHAR(");
                    builder.Visit(args[0]);
                    builder.Append(",'YYYY'))");
                    break;
                case DateParts.Month:
                    builder.Append(" (TO_NUMBER(TO_CHAR(");
                    builder.Visit(args[1]);
                    builder.Append(",'YYYY'))");
                    builder.Append(" - ");
                    builder.Append(" TO_NUMBER(TO_CHAR(");
                    builder.Visit(args[0]);
                    builder.Append(",'YYYY')))*12");
                    builder.Append(" + ");
                    builder.Append(" TO_NUMBER(TO_CHAR(");
                    builder.Visit(args[1]);
                    builder.Append(",'MM'))");
                    builder.Append(" - ");
                    builder.Append(" TO_NUMBER(TO_CHAR(");
                    builder.Visit(args[0]);
                    builder.Append(",'MM'))");
                    break;
                case DateParts.Day:
                    builder.Append(" TRUNC(");
                    builder.Visit(args[1]);
                    builder.Append(" - ");
                    builder.Visit(args[0]);
                    builder.Append(") ");
                    break;
                case DateParts.Hour:
                    builder.Append(" TRUNC((");
                    builder.Visit(args[1]);
                    builder.Append(" - ");
                    builder.Visit(args[0]);
                    builder.Append(")*24) ");
                    break;
                case DateParts.Minute:

                    builder.Append(" TRUNC((");
                    builder.Visit(args[1]);
                    builder.Append(" - ");
                    builder.Visit(args[0]);
                    builder.Append(")*24*60) ");
                    break;
                case DateParts.Second:

                    builder.Append(" TRUNC((");
                    builder.Visit(args[1]);
                    builder.Append(" - ");
                    builder.Visit(args[0]);
                    builder.Append(")*24*60*60) ");

                    break;
                case DateParts.Week:
                    builder.Append(" FLOOR(TRUNC(");
                    builder.Visit(args[1]);
                    builder.Append(" - ");
                    builder.Visit(args[0]);
                    builder.Append(")/7) ");
                    break;

            }
        }
    }
}
