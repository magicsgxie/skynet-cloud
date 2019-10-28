using System;
using System.Collections.Generic;
using System.Text;

namespace Skynet.Cloud.Noap
{
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
}
