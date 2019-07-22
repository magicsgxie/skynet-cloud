using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    public class ExcelInfo
    {
        public string SheetName { get; set; }

        public DataTable SheetDataSource { get; set; }

        //public List<CellStyleInfo> CellStyle { get; set; }
    }
}
