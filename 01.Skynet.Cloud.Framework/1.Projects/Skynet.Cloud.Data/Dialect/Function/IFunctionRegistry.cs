using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Data.Dialect
{
    public interface IFunctionRegistry
    {
        IDictionary<string, IFunctionView> SqlFunctions { get; }

        void RegisterFunction(string fnName, IFunctionView fn);

        bool TryGetFunction(string fnName, out IFunctionView fn);

    }
}
