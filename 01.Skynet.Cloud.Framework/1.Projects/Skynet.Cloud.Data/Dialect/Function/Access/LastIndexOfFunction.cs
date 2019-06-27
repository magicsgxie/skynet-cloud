using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class LastIndexOfFunction : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args == null)
                throw new NotSupportedException("args");
            if (args.Length != 2 && args.Length != 3)
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "LastIndexOf()", "", " 2 or 3 "));
            //var isChar = args[1].Type == Types.Char;
            builder.Append("IIF(INSTR(");
            builder.Visit(args[0]);
            builder.Append(",");
            builder.Visit(args[1]);
            builder.Append(") = 0,-1,");
            if (args.Length == 3)
            {
                //args[2] = Expression.Constant((int)(args[2] as ConstantExpression).Value + 1);
                builder.Append("(IIf((INSTR(");
                builder.Append("MID(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append("),");
                builder.Visit(args[1]);
                builder.Append(") = 0) OR (");
                builder.Visit(args[2]);
                builder.Append(" > LEN(");
                builder.Visit(args[0]);
                builder.Append(")),-1 ,(LEN(MID(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append(")) - (INSTR(");
                builder.Append("STRREVERSE(MID(");
                builder.Visit(args[0]);
                builder.Append(",1,");
                builder.Visit(args[2]);
                builder.Append(")),");

                builder.Append("STRREVERSE(");
                builder.Visit(args[1]);
                builder.Append(")) + LEN(");
                builder.Visit(args[1]);
                builder.Append(")-1)))))");


            }
            else
            {
                builder.Append("(LEN(");
                builder.Visit(args[0]);
                builder.Append(")- (INSTR(STRREVERSE(");
                builder.Visit(args[0]);
                builder.Append("),STRREVERSE(");
                builder.Visit(args[1]);
                builder.Append(")) + LEN(");
                builder.Visit(args[1]);
                builder.Append(")-1)))");


            }
        }
    }
}
