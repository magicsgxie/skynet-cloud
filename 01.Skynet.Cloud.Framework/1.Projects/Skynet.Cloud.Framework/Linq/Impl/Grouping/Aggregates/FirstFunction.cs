using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates
{
    public class FirstFunction : EnumerableAggregateFunction
    {
        /// <summary>
        /// Gets the the First method name.
        /// </summary>
        /// <value><c>First</c>.</value>
        public override string AggregateMethodName
        {
            get
            {
                return "FirstOrDefault";
            }
        }
    }
}
