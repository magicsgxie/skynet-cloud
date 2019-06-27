using System;


namespace UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates
{
    using System.Collections.ObjectModel;
    using System.Linq;

    public class AggregateResultCollection : Collection<AggregateResult>
    {
        /// <summary>
        /// Gets the first <see cref="AggregateResult"/> which
        /// <see cref="AggregateResult.FunctionName"/> is equal to <paramref name="functionName"/>.
        /// </summary>
        /// <value>
        /// The <see cref="AggregateResult"/> for the specified function if any, otherwise null.
        /// </value>
        public AggregateResult this[string functionName]
        {
            get
            {
                return this.FirstOrDefault(r => r.FunctionName == functionName);
            }
        }
    }
}
