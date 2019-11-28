using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// 逻辑与或
    /// </summary>
    public enum FilterCompositionLogicalOperator
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        
        Null = 0,
        /// <summary>
        /// 与
        /// </summary>
        [Description("与")]
        And = 1,

        /// <summary>
        /// 或
        /// </summary>
        [Description("或")]
        Or = 2
    }
}
