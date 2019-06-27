using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class TrimFunctionView : IFunctionView
    {
        public virtual void Render(ISqlBuilder visitor, params Expression[] args)
        {
            var targetArg = args[0];
            visitor.Append("TRIM(");
            visitor.Visit(targetArg);
            visitor.Append(")");

        }
    }
}
