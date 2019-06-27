using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class RightFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "Right()", "", " 2 "));
            }
            //Right(t0.descript,case when CHARINDEX('c',t0.descript) = 0 then 0 else 
            //(LEN(t0.descript)- (CHARINDEX('c',t0.descript)+LEN('c'))+1) end)

            builder.Append("Right(");
            builder.Visit(args[0]);
            builder.Append(",IIF(InStr(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(") = 0,0,(LEN(");
            builder.Visit(args[0]);
            builder.Append(") - (InStr(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(")+LEN(");
            builder.Visit(args[1]);
            builder.Append("))+1)))");



        }
    }
}
