using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    using System.Linq;

    internal static class QueryProviderExtensions
    {
        public static bool IsEntityFrameworkProvider(this IQueryProvider provider)
        {
            var name = provider.GetType().FullName;
            return name == "System.Data.Objects.ELinq.ObjectQueryProvider" ||
                    name == "System.Data.Entity.Core.Objects.ELinq.ObjectQueryProvider" ||
                    name.StartsWith("LinqKit.ExpandableQueryProvider") ||
                    name.StartsWith("System.Data.Entity.Internal.Linq");
        }

        public static bool IsLinqToObjectsProvider(this IQueryProvider provider)
        {
            return provider.GetType().FullName.Contains("EnumerableQuery");
        }
    }
}
