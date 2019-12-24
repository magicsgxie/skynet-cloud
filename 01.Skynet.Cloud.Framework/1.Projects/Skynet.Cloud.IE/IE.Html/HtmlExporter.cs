// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : HtmlExporter.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-26 14:59
//          
//          
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using UWay.Skynet.Cloud.IE.Core;
using UWay.Skynet.Cloud.IE.Core.Extension;
using UWay.Skynet.Cloud.IE.Core.Models;
using RazorEngine;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Encoding = System.Text.Encoding;

namespace UWay.Skynet.Cloud.IE.Html
{
    /// <summary>
    ///     HTML导出
    /// </summary>
    public class HtmlExporter : IExporterByTemplate
    {
        /// <summary>
        ///     根据模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate">Html模板内容</param>
        /// <returns></returns>
        public Task<string> ExportListByTemplate<T>(ICollection<T> dataItems, string htmlTemplate = null) where T : class
        {
            var result = RunCompileTpl(new ExportDocumentInfoOfListData<T>(dataItems), htmlTemplate);
            return Task.FromResult(result);
        }

        /// <summary>
        ///     根据模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="htmlTemplate">Html模板内容</param>
        /// <returns></returns>
        public Task<string> ExportByTemplate<T>(T data, string htmlTemplate) where T : class
        {
            var result = RunCompileTpl(new ExportDocumentInfo<T>(data), htmlTemplate);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 获取HTML模板
        /// </summary>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        protected string GetHtmlTemplate(string htmlTemplate = null) => string.IsNullOrWhiteSpace(htmlTemplate)
                ? typeof(HtmlExporter).Assembly.ReadManifestString("default.cshtml")
                : htmlTemplate;

        /// <summary>
        /// 编译和运行模板
        /// </summary>
        /// <param name="model"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        protected string RunCompileTpl(object model, string htmlTemplate = null)
        {
            var htmlTpl = GetHtmlTemplate(htmlTemplate);
            return Engine.Razor.RunCompile(htmlTpl, htmlTpl.GetHashCode().ToString(), null, model);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        public async Task<TemplateFileInfo> ExportListByTemplate<T>(string fileName, ICollection<T> dataItems,
            string htmlTemplate = null) where T : class
        {
            var file = new TemplateFileInfo(fileName, "text/html");

            var result = await ExportListByTemplate(dataItems, htmlTemplate);
            File.WriteAllText(fileName, result, Encoding.UTF8);
            return file;
        }

        public async Task<TemplateFileInfo> ExportByTemplate<T>(string fileName, T data,
            string htmlTemplate) where T : class
        {
            var file = new TemplateFileInfo(fileName, "text/html");
            var result = await ExportByTemplate(data, htmlTemplate);

            File.WriteAllText(fileName, result, Encoding.UTF8);
            return file;
        }
    }
}