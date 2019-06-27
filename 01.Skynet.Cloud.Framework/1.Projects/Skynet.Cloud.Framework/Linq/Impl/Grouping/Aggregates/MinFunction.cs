using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates
{
    public class MinFunction : EnumerableSelectorAggregateFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinFunction"/> class.
        /// </summary>
        public MinFunction()
        {
        }

        /// <summary>
        /// Gets the the Min method name.
        /// </summary>
        /// <value><c>Min</c>.</value>
        public override string AggregateMethodName
        {
            get
            {
                return "Min";
            }
        }
    }
}
