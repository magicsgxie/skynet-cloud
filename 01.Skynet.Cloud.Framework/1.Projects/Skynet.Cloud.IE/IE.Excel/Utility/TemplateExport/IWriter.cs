// ======================================================================
// 
//           filename : IWriter.cs
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
    public interface IWriter
    {
        /// <summary>
        /// 地址
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// 单元格原始字符串
        /// </summary>
        string CellString { get; set; }

        /// <summary>
        /// 写入器类型
        /// </summary>
        WriterTypes WriterType { get; set; }

        /// <summary>
        /// 表格数据对象Key
        /// </summary>
        string TableKey { get; set; }
    }
}