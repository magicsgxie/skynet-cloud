using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates;

namespace UWay.Skynet.Cloud.DataSource
{
    public class TreeDataSourceResult
    {
        public TreeDataSourceResult()
        {
            AggregateResults = new Dictionary<string, IEnumerable<AggregateResult>>();
        }

        public IEnumerable Data { get; set; }
        public IDictionary<string, IEnumerable<AggregateResult>> AggregateResults { get; set; }
        public object Errors { get; set; }
    }
}
