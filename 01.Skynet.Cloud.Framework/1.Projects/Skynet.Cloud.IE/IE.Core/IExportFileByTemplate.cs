// ======================================================================
// 
//           filename : IExportFileByTemplate.cs
//           description :
// 
//           created by magic.s.g.xie at  -- 
// 
// ======================================================================

using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core.Models;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// 根据模板导出文件
    /// </summary>
    public interface IExportFileByTemplate
    {
        /// <summary>
        ///     根据模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="template">HTML模板或模板路径</param>
        /// <returns></returns>
        Task<ExportFileInfo> ExportByTemplate<T>(string fileName, T data,
            string template) where T : class;
    }
}