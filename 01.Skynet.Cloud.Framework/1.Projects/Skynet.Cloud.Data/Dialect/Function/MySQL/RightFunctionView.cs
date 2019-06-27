using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class RightFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "RIGHT", "", "2"));
            }
            //RIGHT(t0.descript,case WHEN CHARINDEX('c',t0.descript) = 0 then 0 else 
            //(LEN(t0.descript)- (CHARINDEX('c',t0.descript)+LEN('c'))+1) END)
            builder.Append("RIGHT(");
            builder.Visit(args[0]);
            builder.Append(",CASE WHEN INSTR(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(") = 0 THEN 0 ELSE (LENGTH(");
            builder.Visit(args[0]);
            builder.Append(") - (INSTR(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(") + LENGTH(");
            builder.Visit(args[1]);
            builder.Append("))+1) END)");
        }
    }
}
