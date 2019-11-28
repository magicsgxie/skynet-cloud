using System.Collections;
using System.Collections.Generic;
using System.Data;
using UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates;

namespace UWay.Skynet.Cloud.Request
{

    /// <summary>
    /// 数据结果
    /// </summary>
    public class DataSourceResult
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable Data { get; set; }

        /// <summary>
        /// 总共
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 聚合结果
        /// </summary>
        public IEnumerable<AggregateResult> AggregateResults { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public object Errors { get; set; }
    }


    /// <summary>
    /// Data Table数据结果
    /// </summary>
    public class DataSourceTableResult
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public DataTable Data { get; set; }
        /// <summary>
        /// 总共
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 聚合结果
        /// </summary>
        public IEnumerable<AggregateResult> AggregateResults { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public object Errors { get; set; }
    }

    /// <summary>
    /// 数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataSourceResult<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable<T> Data { get; set; }
        /// <summary>
        /// 总共
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 聚合结果
        /// </summary>
        public IEnumerable<AggregateResult> AggregateResults { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public object Errors { get; set; }
    }


}
