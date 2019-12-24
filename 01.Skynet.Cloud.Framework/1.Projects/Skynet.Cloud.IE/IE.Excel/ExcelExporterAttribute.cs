// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ExcelExporterAttribute.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//          
//          
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using UWay.Skynet.Cloud.IE.Core;

namespace UWay.Skynet.Cloud.IE.Excel
{
    /// <summary>
    ///     Excel导出设计
    /// </summary>
    public class ExcelExporterAttribute : ExporterAttribute
    {
        /// <summary>
        ///     表格样式风格
        /// </summary>
        public string TableStyle { get; set; } = "Medium10";

        /// <summary>
        ///     自适应所有列
        /// </summary>
        public bool AutoFitAllColumn { get; set; }

        /// <summary>
        ///     作者
        /// </summary>
        public string Author { get; set; }
    }
}