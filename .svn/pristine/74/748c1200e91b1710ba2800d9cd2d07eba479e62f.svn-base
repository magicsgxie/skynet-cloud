using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class InsertFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {

            if (args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Insert()", "", " 3 "));
            }

            builder.Append("IIF(");
            builder.Visit(args[1]);
            builder.Append(" > LEN(");
            builder.Visit(args[0]);
            builder.Append("),NULL,MID(");
            builder.Visit(args[0]);
            builder.Append(",1,");
            builder.Visit(Expression.Subtract(Expression.Property(args[1], "Value"), Expression.Constant(1, Types.Int32)));
            builder.Append(") + ");
            builder.Visit(args[2]);
            builder.Append(" + MID(");
            builder.Visit(args[0]);
            builder.Append(",").Visit(args[1]);
            builder.Append(",LEN(");
            builder.Visit(args[0]);
            builder.Append(")))");
        }
    }
}
