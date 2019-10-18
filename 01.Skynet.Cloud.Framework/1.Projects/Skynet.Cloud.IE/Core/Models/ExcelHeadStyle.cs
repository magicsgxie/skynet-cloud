using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.IE.Core.Models
{
    /// <summary>
    ///     excel头部样式
    /// </summary>
    public class ExcelHeadStyle
    {
        /// <summary>
        ///     字体大小
        /// </summary>
        public double FontSize { set; get; } = 11;

        /// <summary>
        ///     是否加粗
        /// </summary>
        public bool IsBold { set; get; }

        /// <summary>
        ///     格式化
        /// </summary>
        public int Format { get; set; }

        /// <summary>
        ///     是否自适应
        /// </summary>
        public bool IsAutoFit { set; get; }

        /// <summary>
        ///     是否忽略
        /// </summary>
        public bool IsIgnore { get; set; }
    }
}
