using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Skynet.Cloud.IE.Test.Models
{
    public class DataTableTest
    {
        public void BuilderDataTable()
        {
            DataTable dt = new DataTable();
            
                
            DataColumn dc = new DataColumn();
            dc.ColumnName = "XX";
            dc.AllowDBNull = false;
            dc.Caption = "xxx";
            dc.MaxLength = 10;
            dc.Expression = "xxxxx";
            dt.PrimaryKey = new DataColumn[] { dc };
            dt.TableName = "";
            dt.DisplayExpression = "";
            dt.Columns.Add(dc);
        }
    }
}
