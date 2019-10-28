using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core;

namespace UWay.Skynet.Cloud.IE
{
    /// <summary>
    ///     导入
    /// </summary>
    public interface IImporter
    {
        /// <summary>
        ///     生成Excel导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<TemplateFileInfo> GenerateTemplate<T>(string fileName) where T : class, new();

        /// <summary>
        ///     生成Excel导入模板
        /// </summary>
        /// <returns></returns>
        Task<TemplateFileInfo> GenerateTemplate(DataTable dataTable,string fileName);

        /// <summary>
        ///     生成Excel导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>二进制字节</returns>
        Task<byte[]> GenerateTemplateBytes<T>() where T : class, new();

        /// <summary>
        ///     导入模型验证数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<ImportResult<T>> Import<T>(string filePath,IDictionary<string, IDictionary<string, object>> keyValuePairs = null) where T : class, new();

        /// <summary>
        ///     导入模型验证数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<ImportResult<T>> Import<T>(Stream stream, IDictionary<string, IDictionary<string, object>> keyValuePairs = null) where T : class, new();


        /// <summary>
        ///  导入模型验证数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="dataTemplate">数据模板</param>
        /// <returns></returns>
        Task<ImportDataTableResult> Import(string filePath, DataTable dataTemplate);

        /// <summary>
        ///  导入模型验证数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="dataTemplate">数据模板</param>
        /// <returns></returns>
        Task<ImportDataTableResult> Import(Stream stream, DataTable dataTemplate);
    }
}
