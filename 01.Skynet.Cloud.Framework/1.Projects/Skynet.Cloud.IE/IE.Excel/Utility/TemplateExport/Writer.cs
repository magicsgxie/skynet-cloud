// ======================================================================
// 
//           filename : Writer.cs
//           description :
// 
//           created by magic.s.g.xie at  -- 
//           
//          
//           
//           
// 
// ======================================================================

namespace UWay.Skynet.Cloud.IE.Excel.Utility.TemplateExport
{
    /// <summary>
    /// 写入器
    /// </summary>
    public class Writer : IWriter
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 单元格原始字符串
        /// </summary>
        public string CellString { get; set; }

        /// <summary>
        /// 写入的字符串
        /// </summary>
        public string WriteString { get; set; }

        /// <summary>
        /// 写入器类型
        /// </summary>
        public WriterTypes WriterType { get; set; }

        /// <summary>
        /// 表格数据对象Key
        /// </summary>
        public string TableKey { get; set; }
    }
}