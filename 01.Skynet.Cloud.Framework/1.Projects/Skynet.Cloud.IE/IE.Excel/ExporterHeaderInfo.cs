// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ExporterHeaderInfo.cs
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
    public class ExporterHeaderInfo
    {
        /// <summary>
        ///     列索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     列名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        ///     列属性
        /// </summary>
        public ExporterHeaderAttribute ExporterHeader { get; set; }
    }
}