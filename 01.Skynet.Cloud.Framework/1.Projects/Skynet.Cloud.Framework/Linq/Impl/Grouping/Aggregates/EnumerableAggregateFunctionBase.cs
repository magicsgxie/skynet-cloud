using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates
{
    public abstract class EnumerableAggregateFunctionBase : AggregateFunction
    {
        /// <summary>
        /// Gets the type of the extension methods that holds the extension methods for
        /// aggregation. For example <see cref="Enumerable"/> or <see cref="Queryable"/>.
        /// </summary>
        /// <value>
        /// The type of that holds the extension methods. The default value is <see cref="Enumerable"/>.
        /// </value>
        protected virtual internal Type ExtensionMethodsType
        {
            get
            {
                return typeof(Enumerable);
            }
        }

        protected override string GenerateFunctionName()
        {
            var sourceName = SourceField;
            if (sourceName.HasValue())
            {
                sourceName = sourceName.Replace(".", "-");
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", AggregateMethodName, sourceName, GetHashCode());
        }
    }
}
