// ======================================================================
// 
//           filename : WordExporter.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-26 14:59
//           
//          
//           
//           
// 
// ======================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using UWay.Skynet.Cloud.IE.Core;
using UWay.Skynet.Cloud.IE.Core.Models;
using UWay.Skynet.Cloud.IE.Html;

namespace UWay.Skynet.Cloud.IE.Word
{
    /// <summary>
    ///     Word导出
    /// </summary>
    public class WordExporter : IExportListFileByTemplate, IExportFileByTemplate
    {
        /// <summary>
        ///     根据模板导出列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="dataItems"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        public async Task<ExportFileInfo> ExportListByTemplate<T>(string fileName, ICollection<T> dataItems,
            string htmlTemplate = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("文件名必须填写!", nameof(fileName));
            var exporter = new HtmlExporter();
            var htmlString = await exporter.ExportListByTemplate(dataItems, htmlTemplate);

            using (var generatedDocument = new MemoryStream())
            {
                using (var package =
                    WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    var mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    var converter = new HtmlConverter(mainPart);
                    converter.ParseHtml(htmlString);

                    mainPart.Document.Save();
                }

                File.WriteAllBytes(fileName, generatedDocument.ToArray());
                var fileInfo = new ExportFileInfo(fileName,
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                return fileInfo;
            }
        }

        /// <summary>
        ///     根据模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="htmlTemplate"></param>
        /// <returns></returns>
        public async Task<ExportFileInfo> ExportByTemplate<T>(string fileName, T data, string htmlTemplate)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("文件名必须填写!", nameof(fileName));
            var exporter = new HtmlExporter();
            var htmlString = await exporter.ExportByTemplate(data, htmlTemplate);

            using (var generatedDocument = new MemoryStream())
            {
                using (var package =
                    WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    var mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    var converter = new HtmlConverter(mainPart);
                    converter.ParseHtml(htmlString);

                    mainPart.Document.Save();
                }

                File.WriteAllBytes(fileName, generatedDocument.ToArray());
                var fileInfo = new ExportFileInfo(fileName,
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                return fileInfo;
            }
        }
    }
}