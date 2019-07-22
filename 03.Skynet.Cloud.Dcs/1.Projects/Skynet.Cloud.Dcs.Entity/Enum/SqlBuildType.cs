using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// Sql拼装类型
    /// </summary>
    public enum UqlType
    {
        /// <summary>
        /// 指标数据，如，性能数据，话单数据，MR数据等
        /// </summary>
        IndicatorData = 1,
        /// <summary>
        /// 基础数据
        /// </summary>
        BaseData = 2,
        /// <summary>
        /// 参数数据
        /// </summary>
        ParaData = 3
    }
}
