using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class StandardSafeFunctionView : StandardFunctionView
    {
        private int allowedArgsCount = 1;
        public StandardSafeFunctionView(string name, int allowedArgsCount)
            : base(name)
        {
            this.allowedArgsCount = allowedArgsCount;
        }

        public override void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length != allowedArgsCount)
                throw new ArgumentException(string.Format(Res.ArgumentCountError, name, "", allowedArgsCount));
            base.Render(builder, args);
        }
    }
}
