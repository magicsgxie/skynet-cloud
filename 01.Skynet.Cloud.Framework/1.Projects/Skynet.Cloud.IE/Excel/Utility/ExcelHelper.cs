using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.IE.Core;

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
        public static TemplateFileInfo CreateExcelPackage(string fileName, Action<Workbook> creator)
        {
            var file = new TemplateFileInfo(fileName,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            using (var excelPackage = new Workbook())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        private static void Save(Workbook excelPackage, TemplateFileInfo file)
        {
            excelPackage.Save(file.FileName);
        }
    }
}
