﻿// ======================================================================
// 
//           filename : IExporter.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
// 
// ======================================================================

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core.Models;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    ///     导出
    /// </summary>
    public interface IExporter
    {
        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="dataItems">数据</param>
        /// <returns>文件</returns>
        Task<ExportFileInfo> Export<T>(string fileName, ICollection<T> dataItems) where T : class;

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="dataItems">数据</param>
        /// <returns>文件二进制数组</returns>
        Task<byte[]> ExportAsByteArray<T>(ICollection<T> dataItems) where T : class;

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="dataItems">数据</param>
        /// <returns>文件</returns>
        Task<ExportFileInfo> Export<T>(string fileName, DataTable dataItems) where T : class;

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="dataItems">数据</param>
        /// <returns>文件二进制数组</returns>
        Task<byte[]> ExportAsByteArray<T>(DataTable dataItems) where T : class;

        /// <summary>
        ///     导出Excel表头
        /// </summary>
        /// <param name="items">表头数组</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="globalStyle">全局样式</param>
        /// <param name="styles">样式</param>
        /// <returns>文件二进制数组</returns>
        Task<byte[]> ExportHeaderAsByteArray(string[] items, string sheetName, ExcelHeadStyle globalStyle = null,
            List<ExcelHeadStyle> styles = null);

        /// <summary>
        ///     导出Excel表头
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>文件二进制数组</returns>
        Task<byte[]> ExportHeaderAsByteArray<T>(T type) where T : class;
    }
}