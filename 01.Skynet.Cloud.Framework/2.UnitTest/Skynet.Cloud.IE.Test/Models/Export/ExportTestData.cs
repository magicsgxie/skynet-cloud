// ======================================================================
// 
//           filename : ExportTestData.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-11-05 20:02
//           
//           
//           
//           
// 
// ======================================================================

using System.ComponentModel.DataAnnotations;
using UWay.Skynet.Cloud.IE.Core;

namespace UWay.Skynet.Cloud.IE.Tests.Models.Export
{
    /// <summary>
    ///     在Excel导出中，Name将为Sheet名称
    ///     在HTML、Pdf、Word导出中，Name将为标题
    /// </summary>
    [Exporter(Name = "通用导出测试")]
    public class ExportTestData
    {
        /// <summary>
        /// </summary>
        [Display(Name = "列1")]
        public string Name1 { get; set; }

        [ExporterHeader(DisplayName = "列2")] public string Name2 { get; set; }

        public string Name3 { get; set; }
        public string Name4 { get; set; }
    }
}