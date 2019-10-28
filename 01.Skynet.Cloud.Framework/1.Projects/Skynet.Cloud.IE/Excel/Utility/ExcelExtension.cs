using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UWay.Skynet.Cloud.IE.Excel;

namespace UWay.Skynet.Cloud.IE.Core
{
    public static class ExcelExtension
    {
        private const string EXCEL_EXPORTER_ATTRIBUTE = "EXCEL_EXPORTER_ATTRIBUTE";
        
        public static void AddCustomAttributes(this DataTable dt, string name, float? headerFontSize = 11, float? fontSize = 11, string tableStyle = null, bool autoFitAllColumn = true,string author = null)
        {
            ExcelExporterAttribute excelExporterAttribute = new ExcelExporterAttribute
            {
                Name = name,
                HeaderFontSize = headerFontSize,
                FontSize = fontSize,
                AutoFitAllColumn = autoFitAllColumn,
                TableStyle = tableStyle
            };
            dt.ExtendedProperties.Add(EXCEL_EXPORTER_ATTRIBUTE, excelExporterAttribute);
        }

        public static ExcelExporterAttribute GetCustomAttributes(this DataTable dt)
        {
            if(dt.ExtendedProperties.Contains(EXCEL_EXPORTER_ATTRIBUTE))
            {
                return dt.ExtendedProperties[EXCEL_EXPORTER_ATTRIBUTE] as ExcelExporterAttribute;
            }
            return new ExcelExporterAttribute();
        }
    }
}
