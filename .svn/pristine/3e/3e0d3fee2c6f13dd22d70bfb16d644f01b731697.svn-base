using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Dialect.Function.Default;

namespace UWay.Skynet.Cloud.Data.Dialect
{
    public static class FunctionView
    {
        public static readonly IFunctionView Like = new LikeFunctionView();
        public static readonly IFunctionView StartsWith = new StartsWithFunctionView();
        public static readonly IFunctionView EndsWith = new EndsWithFunctionView();
        public static readonly IFunctionView Contains = new ContainsFunctionView();
        public static readonly IFunctionView Trim = new TrimFunctionView();
        public static readonly IFunctionView TrimStart = new TrimStartFunctionView();
        public static readonly IFunctionView TrimEnd = new TrimEndFunctionView();
        public static readonly IFunctionView LRTrim = new LRTrimFunctionView();
        public static readonly IFunctionView IsNullOrEmpty = new IsNullOrEmptyFunctionView();
        public static readonly IFunctionView IsNullOrWhiteSpace = new IsNullOrWhiteSpaceFunctionView();

        public static readonly IFunctionView Case = new CaseFunctionView();
        public static readonly IFunctionView Compare = new CompareFunctionView();
        public static readonly IFunctionView Equal = new EqualsFunctionView();
        public static readonly IFunctionView NotEqual = new EqualsFunctionView { isNot = true };

        public static IFunctionView Proxy(Action<ISqlBuilder, Expression[]> handler)
        {
            return new ProxyFunctionView(handler);
        }
        public static IFunctionView VarArgs(string begin, string sep, string end)
        {
            return new VarArgsFunctionView(begin, sep, end);
        }
        public static IFunctionView NoArgs(string name)
        {
            return new NoArgFunctionView(name);
        }
        public static IFunctionView Template(string template)
        {
            return new TemplateFunctionView(template);
        }
        public static IFunctionView StandardSafe(string name, int allowedArgsCount)
        {
            return new StandardSafeFunctionView(name, allowedArgsCount);
        }
        public static IFunctionView Standard(string name)
        {
            return new StandardFunctionView(name);
        }
        public static IFunctionView NotSupport(string functionName)
        {
            return new NotSupportFunctionView(functionName);
        }


    }

}
