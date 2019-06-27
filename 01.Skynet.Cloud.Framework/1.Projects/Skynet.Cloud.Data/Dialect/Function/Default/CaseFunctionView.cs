using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class CaseFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                throw new ArgumentException(string.Format(Res.ArgumentCountError, "CASE", "", " 2 or 3 "));
            }

            var whens = (args[0] as ICollection<Expression>).ToArray();
            var thens = (args[1] as ICollection<Expression>).ToArray();
            var @else = args[2] as Expression;

            builder.Append("CASE");

            var lenght = whens.Length;
            for (int i = 0; i < lenght; i++)
            {
                builder.Append(" WHEN ");
                builder.Visit(whens[i]);
                builder.Append(" THEN ");
                builder.Visit(thens[i]);
            }

            if (@else != null)
            {
                builder.Append(" ELSE ");
                builder.Visit(@else);
            }

            builder.Append(" END");
        }
    }
}
