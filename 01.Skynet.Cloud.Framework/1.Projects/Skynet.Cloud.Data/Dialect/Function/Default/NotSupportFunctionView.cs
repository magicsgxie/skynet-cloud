using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class NotSupportFunctionView : IFunctionView
    {
        private string FunctionName;
        public NotSupportFunctionView(string name)
        {
            FunctionName = name;
        }
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            throw new NotSupportedException(string.Format(Res.NotSupported, FunctionName, "function"));
        }
    }
}
