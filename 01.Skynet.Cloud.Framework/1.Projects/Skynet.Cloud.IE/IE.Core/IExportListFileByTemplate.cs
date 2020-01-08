// ======================================================================
// 
//           filename : IExportListFileByTemplate.cs
//           description :
// 
//           created by magic.s.g.xie at  -- 
// 
// ======================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core.Models;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// 根据模板导出列表文件
    /// </summary>
    public interface IExportListFileByTemplate
    {
        /// <summary>
        ///     根据模板导出列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        Task<ExportFileInfo> ExportListByTemplate<T>(string fileName, ICollection<T> dataItems,
            string htmlTemplate = null) where T : class;
    }
}