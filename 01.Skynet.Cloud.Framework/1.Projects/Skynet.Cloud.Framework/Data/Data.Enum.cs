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
