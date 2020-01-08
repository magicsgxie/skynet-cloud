// ======================================================================
// 
//           filename : ExcelHelper.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//          
//           
//           
// 
// ======================================================================

using System;
using System.IO;
using UWay.Skynet.Cloud.IE.Core.Models;
using OfficeOpenXml;

namespace UWay.Skynet.Cloud.IE.Excel.Utility
{
    public static class ExcelHelper
    {
        /// <summary>
        ///     创建Excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static ExportFileInfo CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
        {
            var file = new ExportFileInfo(fileName,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            using (var excelPackage = new ExcelPackage())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        private static void Save(ExcelPackage excelPackage, ExportFileInfo file)
        {
            excelPackage.SaveAs(new FileInfo(file.FileName));
        }
    }
}