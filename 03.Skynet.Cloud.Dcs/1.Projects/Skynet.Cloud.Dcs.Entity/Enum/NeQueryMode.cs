using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 网元查询模式
    /// </summary>
    public enum NeQueryMode
    {
        /// <summary>
        /// 网元树
        /// </summary>
        NeTree = 0,
        /// <summary>
        /// 网元分组
        /// </summary>
        NeGroup = 1,
        /// <summary>
        /// 行政区
        /// </summary>
        Area = 2,
    }
}
