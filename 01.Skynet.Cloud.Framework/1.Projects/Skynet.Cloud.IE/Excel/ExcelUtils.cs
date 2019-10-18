using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace UWay.Skynet.Cloud.Excel.IE
{
    public class ExcelUtils
    {


        /// <summary>
        /// 文件流转化为Excel实体
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static IList<ExcelInfo> ToExcelInfo(string file,int columnNumber = 0)
        {
            Workbook workbook = new Workbook(file);

            return GetExcelInfos(workbook, columnNumber);
        }

        //public static IList<ExcelInfo> ToExcelInfo(Stream fileStream)
        //{
        //    //Workbook workbook = new Workbook(fileStream);

        //    //return GetExcelInfos(workbook);
        //    IList<ExcelInfo> data = new List<ExcelInfo>();
        //    Workbook workbook = new Workbook(fileStream);
        //    System.Threading.Tasks.Parallel.ForEach(workbook.Worksheets, worksheet =>
        //    {
        //        ExcelInfo sheetInfo = new ExcelInfo();
        //        var cells = worksheet.Cells;
        //        DataTable dt = new DataTable();

        //        for (int i = 0; i < cells.MaxDataColumn + 1; i++)
        //        {
        //            DataColumn dc = new DataColumn();
        //            dc.ColumnName = cells[0, i].StringValue.Replace("(*)", string.Empty).Trim();
        //            dt.Columns.Add(dc);
        //        }

        //        for (int i = 1; i < cells.MaxDataRow + 1; i++)
        //        {
        //            DataRow dr = dt.NewRow();
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                var dc = dt.Columns[j];
        //                string value;
        //                if (cells[i, j].Type == CellValueType.IsNumeric && cells[i, j].Value.ToString().Contains("E"))
        //                {
        //                    value = Decimal.Parse(cells[i, j].Value.ToString(), System.Globalization.NumberStyles.Float).ToString();
        //                }
        //                else
        //                {
        //                    value = (cells[i, j].Value ?? string.Empty).ToString();
        //                }
        //                if (value.Length > 0)
        //                    value = value.Replace(Environment.NewLine, string.Empty);
        //                dr[dc.ColumnName] = value;
        //            }
        //            dt.Rows.Add(dr);
        //        }
        //        sheetInfo.SheetName = worksheet.Name;
        //        sheetInfo.SheetDataSource = dt;
        //        data.Add(sheetInfo);
        //    });
        //    return data;
        //}

        /// <summary>
        /// 文件流转化为Excel实体
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static IList<ExcelInfo> ToExcelInfo(Stream fileStream, int columnNumber)
        {
            
            Workbook workbook = new Workbook(fileStream);
           
            return GetExcelInfos(workbook, columnNumber);
        }

        private static  IList<ExcelInfo> GetExcelInfos(Workbook workbook,int columnNumber = 0)
        {
            List<ExcelInfo> data = new List<ExcelInfo>();
            System.Threading.Tasks.Parallel.ForEach(workbook.Worksheets, worksheet =>
            {
                ExcelInfo sheetInfo = new ExcelInfo();
                var cells = worksheet.Cells;
                if(columnNumber == 0)
                {
                    columnNumber = cells.MaxDataColumn;
                }
                var columnIndex = columnNumber < cells.MaxDataColumn ? columnNumber : cells.MaxDataColumn;
                for (int i = 0; i < columnIndex + 1; i++)
                {
                    DataColumn dc = new DataColumn();
                    cells[0, i].PutValue(cells[0, i].StringValue.Replace("(*)", string.Empty).Trim());
                }
                var dt = worksheet.Cells.ExportDataTable(0, 0, cells.MaxDataRow + 1, columnIndex + 1, true);
                sheetInfo.SheetName = worksheet.Name;
                sheetInfo.SheetDataSource = dt;
                data.Add(sheetInfo);
            });

            return data;
        }

        /// <summary>
        /// 到处Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Export(DataTable dt, string path)
        {
            if (dt != null)
            {
                try
                {
                    Workbook workbook = new Workbook();
                    Worksheet cellSheet = workbook.Worksheets[0];
                    //为head添加样式      
                    Style headStyle = workbook.CreateStyle();
                    //设置居中  
                    headStyle.HorizontalAlignment = TextAlignmentType.Center;
                    //设置背景颜色  
                    headStyle.ForegroundColor = System.Drawing.Color.FromArgb(215, 236, 241);
                    headStyle.Pattern = BackgroundType.Solid;
                    headStyle.Font.Size = 12;
                    headStyle.Font.Name = "宋体";
                    headStyle.Font.IsBold = true;

                    //为单元格添加样式      
                    Style cellStyle = workbook.CreateStyle();
                    //设置居中
                    cellStyle.HorizontalAlignment = TextAlignmentType.Center;
                    cellStyle.Pattern = BackgroundType.Solid;
                    cellStyle.Font.Size = 12;
                    cellStyle.Font.Name = "宋体";

                    //设置列宽 从0开始 列宽单位是字符
                    cellSheet.Cells.SetColumnWidth(1, 43);
                    cellSheet.Cells.SetColumnWidth(5, 12);
                    cellSheet.Cells.SetColumnWidth(7, 10);
                    cellSheet.Cells.SetColumnWidth(8, 14);
                    cellSheet.Cells.SetColumnWidth(9, 14);

                    int rowIndex = 0;
                    int colIndex = 0;
                    int colCount = dt.Columns.Count;
                    int rowCount = dt.Rows.Count;
                    //Head 列名处理
                    for (int i = 0; i < colCount; i++)
                    {
                        cellSheet.Cells[rowIndex, colIndex].PutValue(dt.Columns[i].ColumnName);
                        cellSheet.Cells[rowIndex, colIndex].SetStyle(headStyle);
                        colIndex++;
                    }
                    rowIndex++;
                    //Cell 其它单元格处理
                    for (int i = 0; i < rowCount; i++)
                    {
                        colIndex = 0;
                        for (int j = 0; j < colCount; j++)
                        {
                            cellSheet.Cells[rowIndex, colIndex].PutValue(dt.Rows[i][j].ToString());
                            cellSheet.Cells[rowIndex, colIndex].SetStyle(cellStyle);
                            colIndex++;
                        }
                        rowIndex++;
                    }
                    cellSheet.AutoFitColumns();  //列宽自动匹配，当列宽过长是收缩
                    path = Path.GetFullPath(path);
                    //workbook.Save(path,SaveFormat.CSV);  
                    workbook.Save(path);
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception("导出"+path+"失败" + e.Message);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
