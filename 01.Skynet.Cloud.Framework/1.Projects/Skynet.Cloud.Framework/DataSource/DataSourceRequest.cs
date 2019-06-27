using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.DataSource
{
    /// <summary>
    /// 数据请求包
    /// </summary>
    public class DataSourceRequest
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSourceRequest()
        {
            Page = 1;
            Aggregates = new List<AggregateDescriptor>();
        }

        /// <summary>
        /// 页码
        /// </summary>
        public long Page
        {
            get;
            set;
        }

        /// <summary>
        /// 每页数据
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 分页
        /// </summary>
        public IList<SortDescriptor> Sorts
        {
            get;
            set;
        }

        /// <summary>
        /// 过滤
        /// </summary>
        public IList<IFilterDescriptor> Filters
        {
            get;
            set;
        }


        /// <summary>
        /// 分组
        /// </summary>
        public IList<GroupDescriptor> Groups
        {
            get;
            set;
        }


        /// <summary>
        /// 汇总
        /// </summary>
        public IList<AggregateDescriptor> Aggregates
        {
            get;
            set;
        }
    }
}
