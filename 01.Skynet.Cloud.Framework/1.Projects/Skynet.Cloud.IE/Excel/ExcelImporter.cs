using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core;
using UWay.Skynet.Cloud.IE.Excel.Utility;

namespace UWay.Skynet.Cloud.IE.Excel
{
    /// <summary>
    ///     Excel导入
    /// </summary>
    public class ExcelImporter : IImporter
    {
        /// <summary>
        /// 生成Excel导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">文件名必须填写! - fileName</exception>
        public Task<TemplateFileInfo> GenerateTemplate<T>(string fileName) where T : class, new()
        {
            using (var importer = new ImportHelper<T>())
            {
                return importer.GenerateTemplate(fileName);
            }
        }

        public Task<TemplateFileInfo> GenerateTemplate(DataTable dataTable, string fileName)
        {
            using (var importer = new ImportHelper<DataTable>())
            {
                return importer.GenerateTemplate(dataTable, fileName);
            }
        }

        /// <summary>
        ///     生成Excel导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>二进制字节</returns>
        public Task<byte[]> GenerateTemplateBytes<T>() where T : class, new()
        {
            using (var importer = new ImportHelper<T>())
            {
                return importer.GenerateTemplateByte();
            }
        }

        /// <summary>
        ///     导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Task<ImportResult<T>> Import<T>(string filePath,IDictionary<string, IDictionary<string,object>> keyValuePairs = null) where T : class, new()
        {
            using (var importer = new ImportHelper<T>(filePath))
            {
                return importer.Import(filePath, keyValuePairs);
            }
        }

        /// <summary>
        ///     导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Task<ImportDataTableResult> Import(string filePath, DataTable dataTemplate)
        {
            using (var importer = new ImportHelper<DataTable>(filePath))
            {
                return importer.Import(filePath, dataTemplate);
            }
        }

        public Task<ImportResult<T>> Import<T>(Stream stream, IDictionary<string, IDictionary<string, object>> keyValuePairs = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<ImportDataTableResult> Import(Stream stream, DataTable dataTemplate)
        {
            throw new NotImplementedException();
        }
    }
}
