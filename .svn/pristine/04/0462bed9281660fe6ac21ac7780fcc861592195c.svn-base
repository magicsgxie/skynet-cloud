using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class RightFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Right", "", "2"));
            }
            //Right(t0.descript,CASE WHEN CHARINDEX('c',t0.descript) = 0 THEN 0 else 
            //(LEN(t0.descript)- (CHARINDEX('c',t0.descript)+LEN('c'))+1) END)
            builder.Append("RIGHT(");
            builder.Visit(args[0]);
            builder.Append(",CASE WHEN CHARINDEX(");
            builder.Visit(args[1]);
            builder.Append(",");
            builder.Visit(args[0]);
            builder.Append(") = 0 THEN 0 ELSE (LEN(");
            builder.Visit(args[0]);
            builder.Append(") - (CHARINDEX(");
            builder.Visit(args[1]);
            builder.Append(",");
            builder.Visit(args[0]);
            builder.Append(") + LEN(");
            builder.Visit(args[1]);
            builder.Append("))+1) END)");
        }
    }
}
