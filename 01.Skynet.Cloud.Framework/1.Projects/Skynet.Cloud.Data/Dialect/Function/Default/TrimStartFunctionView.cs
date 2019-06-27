using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class TrimStartFunctionView : IFunctionView
    {
        public virtual void Render(ISqlBuilder visitor, params Expression[] args)
        {
            var targetArg = args[0];
            visitor.Append("LTRIM(");
            visitor.Visit(targetArg);
            visitor.Append(")");

        }
    }
}
