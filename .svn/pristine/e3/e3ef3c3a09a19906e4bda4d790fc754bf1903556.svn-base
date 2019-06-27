using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class IFFunctionView : IFunctionView
    {
        public void Render(ISqlBuilder visitor, params Expression[] args)
        {
            var whens = (args[0] as ICollection<Expression>).ToArray();
            var thens = (args[1] as ICollection<Expression>).ToArray();
            var @else = args[2] as Expression;

            var lenght = whens.Length;
            for (int i = 0; i < lenght; i++)
            {
                if (i != 0)
                    visitor.Append(",");

                visitor.Append("IIF(");
                visitor.Visit(whens[i]);
                visitor.Append(",");
                visitor.Visit(thens[i]);

                if (i == lenght - 1)
                {
                    if (@else != null)
                    {
                        visitor.Append(",");
                        visitor.Visit(@else);
                    }
                    for (var j = i; j >= 0; j--)
                        visitor.Append(")");

                }


            }
        }
    }
}
