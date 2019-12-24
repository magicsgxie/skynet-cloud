// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : TemplateErrorInfo.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-18 9:43
//           
//           
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

namespace UWay.Skynet.Cloud.IE.Core.Models
{
    /// <summary>
    ///     模板错误信息
    /// </summary>
    public class TemplateErrorInfo
    {
        /// <summary>
        ///     错误等级
        /// </summary>
        public ErrorLevels ErrorLevel { get; set; }

        /// <summary>
        ///     Excel列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///     要求的列名
        /// </summary>
        public string RequireColumnName { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }
    }
}