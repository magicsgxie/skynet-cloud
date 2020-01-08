// ======================================================================
// 
//           filename : IExportListStringByTemplate.cs
//           description :
// 
//           created by magic.s.g.xie at  -- 
// 
// ======================================================================


using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// 根据模板导出列表字符串
    /// </summary>
    public interface IExportListStringByTemplate
    {
        /// <summary>
        ///     根据模板导出列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate">Html模板内容</param>
        /// <returns></returns>
        Task<string> ExportListByTemplate<T>(ICollection<T> dataItems, string htmlTemplate = null) where T : class;
    }
}