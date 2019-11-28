using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates;

namespace UWay.Skynet.Cloud.Request
{

    /// <summary>
    /// 树结构数据源
    /// </summary>
    public class TreeDataSourceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeDataSourceResult()
        {
            AggregateResults = new Dictionary<string, IEnumerable<AggregateResult>>();
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, IEnumerable<AggregateResult>> AggregateResults { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Errors { get; set; }
    }
}
