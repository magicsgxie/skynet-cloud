// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ExporterAttribute.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           QQ：279218456（编程交流）
// 
// ======================================================================

using System;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// 导出特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExporterAttribute : Attribute
    {
        /// <summary>
        ///     名称(比如当前Sheet 名称)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     头部字体大小
        /// </summary>
        public float? HeaderFontSize { set; get; }

        /// <summary>
        ///     正文字体大小
        /// </summary>
        public float? FontSize { set; get; }
    }
}