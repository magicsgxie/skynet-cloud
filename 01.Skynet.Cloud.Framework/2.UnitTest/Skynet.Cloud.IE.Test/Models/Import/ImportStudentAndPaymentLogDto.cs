using UWay.Skynet.Cloud.IE.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.IE.Tests.Models.Import
{
   public  class ImportStudentAndPaymentLogDto
    {

        [ExcelImporter(SheetName = "1班导入数据")]
        public ImportStudentDto Class1Students { get; set; }

        [ExcelImporter(SheetName = "缴费数据")]
        public ImportPaymentLogDto Class2Students { get; set; }
    }
}
