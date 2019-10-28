using System;
using System.Collections.Generic;
using System.Text;

namespace Skynet.Cloud.Noap
{
    public enum NeLevel
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// OMC级别
        /// </summary>
        OMC = 1,
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
}
