using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{

    /// <summary>
    /// A kind of SQL join
    /// </summary>
    public enum JoinType
    {
        /// <summary>
        /// 交叉连接
        /// </summary>
        CrossJoin,
        /// <summary>
        /// 内连接
        /// </summary>
        InnerJoin,
        /// <summary>
        /// Cross Apply
        /// </summary>
        CrossApply,

        /// <summary>
        /// Outer Apply
        /// </summary>
        OuterApply,
        /// <summary>
        /// 左外连接
        /// </summary>
        LeftOuter,

        /// <summary>
        /// 单一左外连接
        /// </summary>
        SingletonLeftOuter
    }

    /// <summary>
    /// Specifies how a result set should be ordered.
    /// </summary>
    public enum OrderByDirection
    {
        /// <summary>
        /// 空值
        /// </summary>
        Null= 0,

        /// <summary>
        /// 升序
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// 降序
        /// </summary>
        Descending = 2,
    }


    /// <summary>
    /// 操作日志类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 自动记录日志
        /// </summary>
        [Description("自动记录日志")]
        Auto = 1,
        /// <summary>
        /// 手动记录日志
        /// </summary>
        [Description("手动记录日志")]
        Manual = 0,

        /// <summary>
        /// 大数据查询日志记录
        /// </summary>
        [Description("查询大数据量记录日志")]
        BigData = 2
    }
}
