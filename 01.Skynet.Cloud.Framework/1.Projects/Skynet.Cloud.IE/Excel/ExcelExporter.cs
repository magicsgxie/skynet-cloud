using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Core;
using UWay.Skynet.Cloud.IE.Core.Models;
using UWay.Skynet.Cloud.IE.Excel.Utility;

namespace UWay.Skynet.Cloud.IE.Excel
{
    public class ExcelExporter : IExporter
    {
        /// <summary>
        ///     表头处理函数
        /// </summary>
        public static Func<string, string> ColumnHeaderStringFunc { get; set; } = str => str;

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dataItems">数据列</param>
        /// <returns>文件</returns>
        public Task<TemplateFileInfo> Export<T>(string fileName, ICollection<T> dataItems) where T : class
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("文件名必须填写!", nameof(fileName));
            //允许不存在扩展名
            //var extension = Path.GetExtension(fileName);
            //if (string.IsNullOrWhiteSpace(extension))
            //{
            //    fileName = fileName + ".xlsx";
            //}
            var fileInfo = ExcelHelper.CreateExcelPackage(fileName, workbook =>
            {
                //导出定义
                var exporter = GetExporterAttribute<T>();

                //if (exporter?.Author != null)
                //    workbook. = exporter?.Author;

                var sheet = workbook.Worksheets.Add(exporter?.Name ?? "导出结果");
                sheet.AutoFitRows();
                sheet.AutoFitColumns();
                //sheet
                //workbook.
                //sheet. = true;
                if (GetExporterHeaderInfoList<T>(out var exporterHeaderList)) return;
                AddHeader(exporterHeaderList, sheet, exporter,(cell,isbold,size) =>
                {
                    var style = workbook.CreateStyle();
                    style.Font.IsBold = isbold;
                    if(size.HasValue)
                        style.Font.DoubleSize = size.Value;
                    cell.SetStyle(style);
                });
                AddDataItems(sheet, exporterHeaderList, dataItems, exporter);
                AddStyle(exporter, exporterHeaderList, sheet);
            });
            return Task.FromResult(fileInfo);
        }

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="dataItems">数据</param>
        /// <returns>文件二进制数组</returns>
        public Task<byte[]> ExportAsByteArray<T>(ICollection<T> dataItems) where T : class
        {
            using (var excelPackage = new Workbook())
            {
                //导出定义
                var exporter = GetExporterAttribute<T>();
                //excelPackage.Settings.
                //if (exporter?.Author != null)
                //    excelPackage.ContentTypeProperties..Author = exporter?.Author;

                var sheet = excelPackage.Worksheets.Add(exporter?.Name ?? "导出结果");
                //sheet.OutLineApplyStyle = true;
                if (GetExporterHeaderInfoList<T>(out var exporterHeaderList)) return null;
                AddHeader(exporterHeaderList, sheet, exporter, (cell, isbold, size) =>
                {
                    var style = excelPackage.CreateStyle();
                    style.Font.IsBold = isbold;
                    if (size.HasValue)
                        style.Font.DoubleSize = size.Value;
                    cell.SetStyle(style);
                });
                AddDataItems(sheet, exporterHeaderList, dataItems, exporter);
                AddStyle(exporter, exporterHeaderList, sheet);

                return Task.FromResult(excelPackage.GetAsByteArray());
            }
        }

        /// <summary>
        ///     导出excel表头
        /// </summary>
        /// <param name="items">表头数组</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="globalStyle">全局样式</param>
        /// <param name="styles">样式</param>
        /// <returns></returns>
        public Task<byte[]> ExportHeaderAsByteArray(string[] items, string sheetName, ExcelHeadStyle globalStyle = null,
            List<ExcelHeadStyle> styles = null)
        {
            using (var excelPackage = new Workbook())
            {
                var sheet = excelPackage.Worksheets.Add(sheetName ?? "导出结果");
                //sheet. = true;
                AddHeader(items, sheet);
                AddStyle(sheet, items.Length,(cellStyle, excelCol) =>
                {
                    var style = excelPackage.CreateStyle();

                    
                    if (cellStyle.Format > 0)
                        style.Number =( cellStyle.Format);
                    style.Font.IsBold = cellStyle.IsBold;
                    style.Font.DoubleSize = cellStyle.FontSize;

                    if (cellStyle.IsAutoFit) style.ShrinkToFit = true;
                    StyleFlag styleFlag = new StyleFlag();
                    styleFlag.NumberFormat = true;
                    excelCol.ApplyStyle(style, styleFlag);
                },globalStyle, styles);
                return Task.FromResult(excelPackage.GetAsByteArray());
            }
        }



        /// <summary>
        ///     导出Excel表头
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>文件二进制数组</returns>
        public Task<byte[]> ExportHeaderAsByteArray<T>(T type) where T : class
        {
            using (var excelPackage = new Workbook())
            {
                //导出定义
                var exporter = GetExporterAttribute<T>();

                //if (exporter?.Author != null)
                //    excelPackage.Workbook.Properties.Author = exporter?.Author;

                var sheet = excelPackage.Worksheets.Add(exporter?.Name ?? "导出结果");
                //sheet.OutLineApplyStyle = true;
                if (GetExporterHeaderInfoList<T>(out var exporterHeaderList)) return null;
                AddHeader(exporterHeaderList, sheet, exporter,(cell, isbold, size) =>
                {
                    var style = excelPackage.CreateStyle();
                    style.Font.IsBold = isbold;
                    if (size.HasValue)
                        style.Font.DoubleSize = size.Value;
                    cell.SetStyle(style);
                });
                AddStyle(exporter, exporterHeaderList, sheet);
                return Task.FromResult(excelPackage.GetAsByteArray());
            }

            throw new NotImplementedException();
        }

        /// <summary>
        ///     创建表头
        /// </summary>
        /// <param name="exporterHeaderDtoList"></param>
        /// <param name="sheet"></param>
        protected void AddHeader(List<ExporterHeaderInfo> exporterHeaderDtoList, Worksheet sheet,
            ExcelExporterAttribute exporter, Action<Cell, bool, float?> action)
        {
            
            foreach (var exporterHeaderDto in exporterHeaderDtoList)
                if (exporterHeaderDto != null)
                {
                    if (exporterHeaderDto.ExporterHeader != null)
                    {
                        var exporterHeaderAttribute = exporterHeaderDto.ExporterHeader;
                        if (exporterHeaderAttribute != null && !exporterHeaderAttribute.IsIgnore)
                        {
                            var name = exporterHeaderAttribute.DisplayName.IsNullOrWhiteSpace()
                                ? exporterHeaderDto.PropertyName
                                : exporterHeaderAttribute.DisplayName;

                            sheet.Cells[1, exporterHeaderDto.Index].PutValue( ColumnHeaderStringFunc(name));
                            var size = exporter?.HeaderFontSize ?? exporterHeaderAttribute.FontSize;
                            action(sheet.Cells[1, exporterHeaderDto.Index], exporterHeaderAttribute.IsBold, size);
                            

                        }
                    }
                    else
                    {
                        sheet.Cells[1, exporterHeaderDto.Index].Value =
                            ColumnHeaderStringFunc(exporterHeaderDto.PropertyName);
                    }
                }
        }

        /// <summary>
        ///     创建表头
        /// </summary>
        /// <param name="exporterHeaders">表头数组</param>
        /// <param name="sheet">工作簿</param>
        protected void AddHeader(string[] exporterHeaders, Worksheet sheet)
        {
            var columnIndex = 0;
            foreach (var exporterHeader in exporterHeaders)
                if (exporterHeader != null)
                {
                    columnIndex++;
                    sheet.Cells[1, columnIndex].Value = exporterHeader;
                }
        }

        /// <summary>
        ///     添加导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="items"></param>
        protected void AddDataItems<T>(Worksheet sheet, List<ExporterHeaderInfo> exporterHeaders, ICollection<T> items,
            ExcelExporterAttribute exporter)
        {
            if (items == null || items.Count == 0)
                return;
            //var tbStyle = TableStyles.Medium10;
            //if (exporter != null && !exporter.TableStyle.IsNullOrWhiteSpace())
            //    tbStyle = (TableStyles)Enum.Parse(typeof(TableStyles), exporter.TableStyle);
            //sheet.Cells.LoadFromCollection(items, false, tbStyle);
            ImportTableOptions importTableOptions = new ImportTableOptions()
            {
                 ConvertGridStyle = true,
                 DateFormat = "yyyy-MM-dd hh:mm:ss",
            };
            sheet.Cells.ImportData(items.ToDataTable(), 2, 1, importTableOptions);
        }

        /// <summary>
        ///     添加导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="items"></param>
        protected void AddDataItems(Worksheet sheet,  DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;
            //var tbStyle = TableStyles.Medium10;
            //if (exporter != null && !exporter.TableStyle.IsNullOrWhiteSpace())
            //    tbStyle = (TableStyles)Enum.Parse(typeof(TableStyles), exporter.TableStyle);
            //sheet.Cells.LoadFromCollection(items, false, tbStyle);
            ImportTableOptions importTableOptions = new ImportTableOptions()
            {
                ConvertGridStyle = true,
                DateFormat = "yyyy-MM-dd hh:mm:ss",
            };
            sheet.Cells.ImportData(dt, 2, 1, importTableOptions);
        }


        /// <summary>
        ///     添加样式
        /// </summary>
        /// <param name="exporterHeaders"></param>
        /// <param name="sheet"></param>
        protected void AddStyle(ExcelExporterAttribute exporter, List<ExporterHeaderInfo> exporterHeaders,
            Worksheet sheet)
        {
            foreach (var exporterHeader in exporterHeaders)
                if (exporterHeader.ExporterHeader != null)
                {
                    if (exporterHeader.ExporterHeader.IsIgnore)
                    {
                        //TODO:后续直接修改数据导出逻辑（不写忽略列数据）
                        sheet.Cells.DeleteColumn(exporterHeader.Index);
                        //删除之后，序号依次-1
                        foreach (var item in exporterHeaders.Where(p => p.Index > exporterHeader.Index)) item.Index--;
                        continue;
                    }

                    var col = sheet.Cells.Columns[exporterHeader.Index];
                    col.Style.Number = exporterHeader.ExporterHeader.Format;

                    if (exporter.AutoFitAllColumn || exporterHeader.ExporterHeader.IsAutoFit)
                        sheet.AutoFitColumns();
                }
        }

        /// <summary>
        ///     添加样式
        /// </summary>
        /// <param name="sheet">excel工作簿</param>
        /// <param name="columns">总列数</param>
        /// <param name="globalStyle">全局样式</param>
        /// <param name="styles">样式</param>
        protected void AddStyle(Worksheet sheet, int columns,Action<ExcelHeadStyle, Column> action, ExcelHeadStyle globalStyle = null,
            List<ExcelHeadStyle> styles = null)
        {
            var col = 0;
            if (styles != null)
            {
                foreach (var style in styles)
                {
                    col++;
                    if (col <= columns)
                    {
                        if (style.IsIgnore)
                        {
                            sheet.Cells.DeleteColumn(col);
                            continue;
                        }

                        var excelCol = sheet.Cells.Columns[col];
                        action(style, excelCol);
                    }
                }
            }
            else
            {
                if (globalStyle != null)
                    for (var i = 1; i <= columns; i++)
                    {
                        if (globalStyle.IsIgnore)
                        {
                            sheet.Cells.DeleteColumn(i);
                            continue;
                        }

                        var excelCol = sheet.Cells.Columns[i];
                        action(globalStyle, excelCol);
                        //if (!globalStyle.Format.IsNullOrWhiteSpace())
                        //    excelCol.Style.Numberformat.Format = globalStyle.Format;
                        //excelCol.Style.Font.Bold = globalStyle.IsBold;
                        //excelCol.Style.Font.Size = globalStyle.FontSize;

                        //if (globalStyle.IsAutoFit) excelCol.AutoFit();
                    }
            }
        }

        /// <summary>
        ///     获取头部定义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exporterHeaderList"></param>
        /// <returns></returns>
        private static bool GetExporterHeaderInfoList<T>(out List<ExporterHeaderInfo> exporterHeaderList)
        {
            exporterHeaderList = new List<ExporterHeaderInfo>();
            var objProperties = typeof(T).GetProperties();
            if (objProperties == null || objProperties.Length == 0)
                return true;
            for (var i = 0; i < objProperties.Length; i++)
                exporterHeaderList.Add(new ExporterHeaderInfo
                {
                    Index = i + 1,
                    PropertyName = objProperties[i].Name,
                    ExporterHeader =
                        (objProperties[i].GetCustomAttributes(typeof(ExporterHeaderAttribute), true) as
                            ExporterHeaderAttribute[])?.FirstOrDefault()
                });
            return false;
        }

        /// <summary>
        ///     获取头部定义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exporterHeaderList"></param>
        /// <returns></returns>
        private static bool GetExporterHeaderInfoList(DataTable dt, out List<ExporterHeaderInfo> exporterHeaderList)
        {
            exporterHeaderList = new List<ExporterHeaderInfo>();
            
            for (var i = 0; i < dt.Columns.Count; i++)
                exporterHeaderList.Add(new ExporterHeaderInfo
                {
                    Index = i + 1,
                    PropertyName = dt.Columns[i].ColumnName,
                    ExporterHeader = new ExporterHeaderAttribute(dt.Columns[i].Caption)
                });
            return false;
        }

        /// <summary>
        ///     获取表全局定义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static ExcelExporterAttribute GetExporterAttribute<T>() where T : class
        {
            var exporterTableAttributes =
                typeof(T).GetCustomAttributes(typeof(ExcelExporterAttribute), true) as ExcelExporterAttribute[];
            if (exporterTableAttributes != null && exporterTableAttributes.Length > 0)
                return exporterTableAttributes[0];

            var exporterAttributes =
                typeof(T).GetCustomAttributes(typeof(ExporterAttribute), true) as ExporterAttribute[];

            if (exporterAttributes != null && exporterAttributes.Length > 0)
            {
                var export = exporterAttributes[0];
                return new ExcelExporterAttribute
                {
                    FontSize = export.FontSize,
                    HeaderFontSize = export.HeaderFontSize
                };
            }

            return null;
        }

        /// <summary>
        ///     获取表全局定义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static ExcelExporterAttribute GetExporterAttribute(DataTable dt)
        {
            var export = dt.GetCustomAttributes();
            return new ExcelExporterAttribute
            {
                FontSize = export.FontSize,
                HeaderFontSize = export.HeaderFontSize
            };
            

        }

        ///// <summary>
        /////     获取表全局定义
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //private static ExcelExporterAttribute GetExporter() 
        //{
        //    var exporterTableAttributes =
        //        typeof(T).GetCustomAttributes(typeof(ExcelExporterAttribute), true) as ExcelExporterAttribute[];
        //    if (exporterTableAttributes != null && exporterTableAttributes.Length > 0)
        //        return exporterTableAttributes[0];

        //    var exporterAttributes =
        //        typeof(T).GetCustomAttributes(typeof(ExporterAttribute), true) as ExporterAttribute[];

        //    if (exporterAttributes != null && exporterAttributes.Length > 0)
        //    {
        //        var export = exporterAttributes[0];
        //        return new ExcelExporterAttribute
        //        {
        //            FontSize = export.FontSize,
        //            HeaderFontSize = export.HeaderFontSize
        //        };
        //    }

        //    return null;
        //}

        public Task<TemplateFileInfo> Export(string fileName, DataTable dt)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("文件名必须填写!", nameof(fileName));
            //允许不存在扩展名
            //var extension = Path.GetExtension(fileName);
            //if (string.IsNullOrWhiteSpace(extension))
            //{
            //    fileName = fileName + ".xlsx";
            //}
            var fileInfo = ExcelHelper.CreateExcelPackage(fileName, workbook =>
            {
                //导出定义
                var exporter = GetExporterAttribute(dt);

                //if (exporter?.Author != null)
                //    workbook. = exporter?.Author;

                var sheet = workbook.Worksheets.Add(dt.GetSheet());
                sheet.AutoFitRows();
                sheet.AutoFitColumns();
                //sheet
                //workbook.
                //sheet. = true;
                if (GetExporterHeaderInfoList(dt,out var exporterHeaderList)) return;
                AddHeader(exporterHeaderList, sheet, exporter, (cell, isbold, size) =>
                {
                    var style = workbook.CreateStyle();
                    style.Font.IsBold = isbold;
                    if (size.HasValue)
                        style.Font.DoubleSize = size.Value;
                    cell.SetStyle(style);
                });
                AddDataItems(sheet,dt);
                AddStyle(exporter, exporterHeaderList, sheet);
            });
            return Task.FromResult(fileInfo);
        }
    }
}
