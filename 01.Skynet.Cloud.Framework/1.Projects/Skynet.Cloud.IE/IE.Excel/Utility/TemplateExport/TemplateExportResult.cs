// ======================================================================
// 
//           filename : TemplateExportResult.cs
//           description :
// 
//           created by magic.s.g.xie at  -- 
//           
//          
//           
//           
// 
// ======================================================================

using System;
using System.Collections.Generic;

namespace UWay.Skynet.Cloud.IE.Excel.Utility.TemplateExport
{
    /// <summary>
    ///     模板导出结果
    /// </summary>
    public class TemplateExportResult
    {
        /// <summary>
        /// </summary>
        public TemplateExportResult()
        {

        }

        /// <summary>
        ///     模板错误
        /// </summary>
        public virtual IList<TemplateFieldErrorInfo> TemplateErrors { get; set; }

        /// <summary>
        ///     异常信息
        /// </summary>
        public virtual Exception Exception { get; set; }

        /// <summary>
        ///     是否存在导入错误
        /// </summary>
        public virtual bool HasError => Exception != null || (TemplateErrors?.Count > 0);
    }
}