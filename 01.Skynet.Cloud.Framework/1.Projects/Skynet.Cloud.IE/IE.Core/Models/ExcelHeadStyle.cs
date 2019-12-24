// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ExcelHeadStyle.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//           
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

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
        public float FontSize { set; get; } = 11;

        /// <summary>
        ///     是否加粗
        /// </summary>
        public bool IsBold { set; get; }

        /// <summary>
        ///     格式化
        /// </summary>
        public string Format { get; set; }

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