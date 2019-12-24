// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : IExporterByTemplate.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-26 14:59
//           
//           
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core.Models;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// </summary>
    public interface IExporterByTemplate
    {
        /// <summary>
        ///     根据模板导出列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate">Html模板内容</param>
        /// <returns></returns>
        Task<string> ExportListByTemplate<T>(ICollection<T> dataItems, string htmlTemplate = null) where T : class;

        /// <summary>
        ///     根据模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="htmlTemplate">Html模板内容</param>
        /// <returns></returns>
        Task<string> ExportByTemplate<T>(T data, string htmlTemplate = null) where T : class;

        /// <summary>
        ///    根据模板导出列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        Task<TemplateFileInfo> ExportListByTemplate<T>(string fileName, ICollection<T> dataItems,
            string htmlTemplate = null) where T : class;

        /// <summary>
        ///     根据模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        Task<TemplateFileInfo> ExportByTemplate<T>(string fileName, T data,
            string htmlTemplate) where T : class;
    }
}