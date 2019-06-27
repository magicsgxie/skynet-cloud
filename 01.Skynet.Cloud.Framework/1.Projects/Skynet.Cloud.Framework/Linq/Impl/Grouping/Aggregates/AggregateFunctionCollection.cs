using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using UWay.Skynet.Cloud.Linq.Impl;

    public class AggregateFunctionCollection : Collection<AggregateFunction>
    {
        /// <summary>
        /// Gets the <see cref="AggregateFunction"/> with the specified function name.
        /// </summary>
        /// <value>
        /// First <see cref="AggregateFunction"/> with the specified function name 
        /// if any, otherwise null.
        /// </value>
        public AggregateFunction this[string functionName]
        {
            get
            {
                return this.FirstOrDefault(f => f.FunctionName == functionName);
            }
        }
    }
}
