using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 用户性别
    /// </summary>
    public enum UserSex
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 先生
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女士
        /// </summary>
        Female = 2,
    }

    /// <summary>
    /// A kind of SQL join
    /// </summary>
    public enum JoinType
    {
        CrossJoin,
        InnerJoin,
        CrossApply,
        OuterApply,
        LeftOuter,
        SingletonLeftOuter
    }

    /// <summary>
    /// Specifies how a result set should be ordered.
    /// </summary>
    public enum OrderByDirection
    {
        Null= 0,

        Ascending = 1,

        Descending = 2,
    }

    public enum NetType
    {
        CDMA  = 1,
        GSM = 2,
        WCDMA = 3,
        LTE = 4,
        FIVEG=5,
    }

    public enum NeLevel
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// OMC级别
        /// </summary>
        OMC  = 1,
        /// <summary>
        /// Bsc级别
        /// </summary>
        Bsc = 2,
        /// <summary>
        /// 基站级别
        /// </summary>
        Bts = 3,
        /// <summary>
        /// 小区级别
        /// </summary>
        Cell = 4,
        /// <summary>
        /// 载频级别
        /// </summary>
        Carr = 5,
        /// <summary>
        /// 行政区级别
        /// </summary>
        County = 7,
        /// <summary>
        /// 地市级别
        /// </summary>
        City = 8,
        /// <summary>
        /// 场景级别
        /// </summary>
        Scence = 9,
        /// <summary>
        /// 特殊级别
        /// </summary>
        SPECIAL = 10,

        /// <summary>
        /// 场景分支
        /// </summary>
        SUBITEM = 14,
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// 普通数据
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 性能
        /// </summary>
        Perf = 1,

        /// <summary>
        /// 基础数据
        /// </summary>
        Basic = 2,

        /// <summary>
        /// 参数
        /// </summary>
        Para = 3,

        /// <summary>
        /// 栅格
        /// </summary>
        Grid = 4,

        /// <summary>
        /// 话单
        /// </summary>
        CDR = 5,

        /// <summary>
        /// MR话单
        /// </summary>
        MR = 6,

        /// <summary>
        /// 信令
        /// </summary>
        Trace = 7,


        /// <summary>
        /// 工作流
        /// </summary>
        Jom = 8,

        /// <summary>
        /// 感知
        /// </summary>
        Perception = 9,


        /// <summary>
        /// 决策数据
        /// </summary>
        Policy = 10,

        /// <summary>
        /// 规划站
        /// </summary>
        Css = 11,
    }


    /// <summary>
    /// 操作日志类型
    /// </summary>
    public enum ActionType
    {
        [Description("自动记录日志")]
        Auto = 1,
        [Description("手动记录日志")]
        Manual = 0,
        [Description("查询大数据量记录日志")]
        BigData = 2
    }
}
