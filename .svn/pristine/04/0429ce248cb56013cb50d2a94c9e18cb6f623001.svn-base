using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class NoArgFunctionView : IFunctionView
    {

        public NoArgFunctionView(string name)
        {
            Name = name.ToUpper();
        }

        private string Name;

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args != null && args.Length > 0)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, Name, "no", ""));

            builder.Append(Name);
            builder.Append("()");
        }
    }
}
