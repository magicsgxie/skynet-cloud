﻿using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class PadRightFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                throw new NotSupportedException(string.Format(Res.ArgumentCountError, "PadRight()", "", " 2 or 3 "));
            }
            builder.Append("IIF(LEN(");
            builder.Visit(args[0]);
            builder.Append(") >= ");
            builder.Visit(args[1]);
            builder.Append(",");
            builder.Visit(args[0]);
            builder.Append(",(");
            builder.Visit(args[0]);
            builder.Append("+");
            if (args.Length == 2)
            {
                builder.Append("SPACE(");
                builder.Visit(args[1]);
                builder.Append(" - LEN(");
                builder.Visit(args[0]);
                builder.Append("))))");
            }
            else
            {
                builder.Append("STRING(");
                builder.Visit(args[1]);
                builder.Append(" - LEN(");
                builder.Visit(args[0]);
                builder.Append("),");
                builder.Visit(args[2]);
                builder.Append(")))");
            }

        }
    }
}
