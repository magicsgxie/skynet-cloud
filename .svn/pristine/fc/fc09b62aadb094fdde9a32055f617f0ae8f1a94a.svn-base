using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class ProxyFunctionView : IFunctionView
    {
        Action<ISqlBuilder, Expression[]> proxy;

        public ProxyFunctionView(Action<ISqlBuilder, Expression[]> proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");
            this.proxy = proxy;
        }

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            proxy(builder, args);
        }
    }
}
