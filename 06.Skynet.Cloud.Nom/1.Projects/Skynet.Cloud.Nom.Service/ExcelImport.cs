using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;
using System.IO;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using Microsoft.Extensions.Configuration;
using UWay.Skynet.Cloud.Excel.IE;

namespace UWay.Skynet.Cloud.Nom.Service
{
    internal class ExcelImport : IImport
    {




        private IDictionary<string, IRuleCheck> _ruleChecks;
        private IConfiguration _multiCheck;

        public ExcelImport(IEnumerable<IRuleCheck> ruleChecks, IConfiguration configuration)
        {
            _ruleChecks = ruleChecks.ToDictionary(p => p.GetType().Name);
            _multiCheck = configuration.GetSection("multi");
            //serviceProvider.get
        }

        private ImportMsgResult msgResult = new ImportMsgResult();

        public ImportMsgResult Import(string excelfileName, ImportDataTemplate template, string user)
        {
            return Import(GetExcelData(excelfileName), template, user);
        }

        protected virtual IList<ExcelInfo> GetExcelData(string excelfileName)
        {
            return ExcelUtils.ToExcelInfo(excelfileName);
        }

        protected ImportMsgResult Import(IList<ExcelInfo> excels, ImportDataTemplate template, string user)
        {
            IList<ImportMsgResult> results = new List<ImportMsgResult>();
            System.Threading.Tasks.Parallel.ForEach(excels, excel => {
                results.Add(Import(excel, template, user));
            });
            return null;
        }

        public ImportMsgResult Import(ExcelInfo excel, ImportDataTemplate template, string user)
        {
            msgResult.Result = false;
            //check template
            if (!CheckTemplate(excel, template.DataFields.Select(p => p.Fieldtext)))
            {

                msgResult.Msg = "导入数据为空";
            }



            return msgResult;
        }

        protected DataTable GetBaseDataChange(DataTable dt, IEnumerable<string> keyFields,string tableName, Dictionary<string, ImportDataTemplateField> dataFields)
        {
            List<string> columnNames = new List<string>();
            List<string> keyFieldValues = new List<string>();
            StringBuilder sb = new StringBuilder();
            DataTable result = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                if(dataFields.ContainsKey(column.ColumnName))
                {
                    columnNames.Add(column.ColumnName);
                    var datafield = dataFields[column.ColumnName];
                    result.Columns.Add(new DataColumn(datafield.Fieldname));
                }
                
            }
            result.Columns.Add(new DataColumn(DataRowExtention.DATABASEOPERATE));
            result.Columns.Add(new DataColumn(DataRowExtention.DATABASEERRORINFO));
            var rowCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                var keyValue = "";
                foreach(var item in keyFields)
                {
                    if(dr[item] != null)
                        keyValue += dr[item].ToString();
                }
                if(keyFieldValues.Contains(keyValue))
                {
                    sb.AppendLine("第" + (rowCount + 2) + "行是重复数据");
                    continue;
                } else
                {
                    keyFieldValues.Add(keyValue);
                }

                DataRow dataRow = result.NewRow();
                foreach(var column in columnNames)
                {

                    CheckFieldValid(tableName, dataFields[column], dr.GetColumnValue(column), dr);
                }

                rowCount++;
            }
            return result;
          

        }

        private bool CheckFieldValid(string tableName,ImportDataTemplateField field, string value, DataRow dr)
        {
            IRuleCheck ruleCheck = GetRule(field);
            return ruleCheck.CheckData(field, value,dr);
        }

        private IRuleCheck GetRule(ImportDataTemplateField field)
        {
            var serviceName = "RegexMatchRuleCheck";

            if(field.Controltype ==4)
            {
                if(field.Fieldname == "CITY_NAME")
                {

                } else if(field.Fieldname == "COUNTY_NAME")
                {

                }
                serviceName = _multiCheck.GetValue<string>("Combobox", "EnumValueRuleCheck");
            } else
            {
                if (field.Datatype.Equals("日期") || field.Datatype.Equals("时间"))
                {
                    serviceName = _multiCheck.GetValue<string>("DateTime", "DateTimeRuleCheck");
                } else if(field.Datatype.Equals("数字"))
                {
                    serviceName = _multiCheck.GetValue<string>("Number", "NumberRuleCheck");
                }
                else if (field.Datatype.Equals("浮点"))
                {
                    serviceName = _multiCheck.GetValue<string>("Float", "FloatRuleCheck");
                }
            }

            return _ruleChecks[serviceName];
        }


        protected virtual bool CheckTemplate(ExcelInfo excel, IEnumerable<string> dataFieldNames)
        {
            if(string.IsNullOrEmpty(excel.SheetName))
            {
                return false;
            }

            if(excel.SheetDataSource == null || excel.SheetDataSource.Rows.Count <=0)
            {
                return false;
            }
            List<string> columnNames = new List<string>();
            foreach(DataColumn column in excel.SheetDataSource.Columns)
            {
                columnNames.Add(column.ColumnName);
            }

            var q = from p in columnNames
                    join r in dataFieldNames
                    on p equals r
                    select p;
            return q.Any();
        }

        



        protected virtual IList<ImportDataTemplateField> GetExistsDataFromExcel(ExcelInfo excel, IList<ImportDataTemplateField> dataFields)
        {
            
            IList<ImportDataTemplateField> excelDataFields = new List<ImportDataTemplateField>();
            if (excel.SheetDataSource != null && excel.SheetDataSource.Columns.Count > 0)
            {
                foreach (DataColumn item in excel.SheetDataSource.Columns)
                {
                    item.ColumnName = item.ColumnName.Replace("(*)", string.Empty).Trim();//过滤自动维护的字段标识
                    var filed = dataFields.FirstOrDefault(p => p.Fieldtext == item.ColumnName);
                    if (filed != null)
                    {
                        excelDataFields.Add(filed);
                    }
                }
            }
            return excelDataFields;
        }

        protected virtual IList<ImportDataTemplateField> GetKeyFields(string keyFieldCombination, IList<ImportDataTemplateField> dataFields)
        {
            if(!keyFieldCombination.IsNullOrEmpty())
            {
                return dataFields.Where(p => keyFieldCombination.Split(',').Contains(p.Fieldname)).ToList();
            }

            return new List<ImportDataTemplateField>(); ;
        }

        protected virtual string ExistsKeyFields(IList<string> keyNames, DataTable dt)
        {
            StringBuilder msg = new StringBuilder();
            
            foreach(DataColumn dc in dt.Columns)
            {
                if (keyNames.Contains(dc.ColumnName))
                {
                    
                }
            }
            return msg.ToString();
        }

        protected virtual List<BasedataChangedRecord> GenerateBaseDataChange(DataTable dataTable) {
            return null;
        }

        protected virtual int ExecuteInsert(DataTable dataTable)
        {
            return 0;
        }

        protected virtual List<EnumValue> GetEnumValue(IList<string> fieldNames)
        {
            return null;
        }

        public ImportMsgResult Import(Stream fileStream, ImportDataTemplate template, string user)
        {
            throw new NotImplementedException();
        }
    }
}
