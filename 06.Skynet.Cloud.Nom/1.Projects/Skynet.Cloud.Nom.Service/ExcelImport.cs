using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Ufa.Enterprise.Entity;

namespace UWay.Skynet.Cloud.Nom.Service
{
    internal class ExcelImport : IImport
    {
        //ExcelDataField
        protected const string DATABASEOPERATE = "DataBaseOperate"; //指示是更新、插入、更新错误、插入错误 
        protected const string DATABASEERRORINFO = "DataBaseErrorInfo"; //指示错误信息
        protected const string DATABASESQL = "DataBaseSql"; //sql语句
        protected const string DATABASEROWGUIDID = "RowGuidID"; //RowGuidID
        protected const string DATABASEIMPID = "ImpId"; //ImpId
        protected const string DATABASEIMPCITY = "ImpCity"; //ImpCity
        protected const string DATABASEIMPTIME = "ImpTime"; //ImpTime

        public void Import(NetType netType, ExcelInfo excel, ImportDataTemplate template, ImportDataTemplateField fields, IList<CityInfo> cityIds, string user)
        {

            //ImportDataTemplateField = new List<ImportDataTemplateField>();
            //DataFields = list;
            //foreach (var item in Template.KeyfieldCombination.Split(',').ToList())
            //{
            //    List<QueryParameter> lisQuery1 = new List<QueryParameter>();
            //    var fld = DataFields.FirstOrDefault(p => p.Fieldname == item && string.IsNullOrWhiteSpace(p.Format));
            //    if (fld == null)
            //    {
            //        var query2 = new QueryParameter() { Name = "Fieldname", Operator = FilterOperator.IsEqualTo, Value = item };
            //        lisQuery1.Add(query);
            //        lisQuery1.Add(query2);
            //        var filed = service.GetDatafieldsByCondition(lisQuery1).FirstOrDefault();
            //        DataFields.Add(filed);
            //    }
            //}
        }

        //protected IList<ImportDataTemplateField> ExcelDataField { set; private get; }

        protected virtual bool CheckTemplate(ExcelInfo excel)
        {
            if(string.IsNullOrEmpty(excel.SheetName))
            {
                return false;
            }
            return true;
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

        protected virtual bool CheckKeyField(string keyFieldCombination, IList<ImportDataTemplateField> dataFields)
        {
            var keyFields = new List<string>();
            foreach (var item in keyFieldCombination.Split(',').ToList())
            {
                var fld = dataFields.FirstOrDefault(p => p.Fieldname == item && string.IsNullOrWhiteSpace(p.Format));
                if (fld != null)
                {
                    // 自动维护的主键CITY_ID,PROVINCE,SCENE_ID可以不需要导入
                    // 但CITY_NAME必须导入(重写CheckKeyField方法)，如扇区
                    if ((fld.Fieldname == "CITY_ID" ||
                        fld.Fieldname == "PROVINCE" ||
                        fld.Fieldname == "SCENE_ID" ||
                        fld.Fieldname == "SERIAL_ID") &&
                        fld.Iseditable == 0)
                        continue;
                    keyFields.Add(fld.Fieldtext);
                }
            }
            if (keyFields.Count > 0)
            {
                List<string> noMatches = new List<string>();
                foreach (var item in keyFields)
                {
                    var noMatche = dataFields.FirstOrDefault(p => p.Fieldtext == item);
                    if (noMatche == null)
                    {
                        noMatches.Add(item);
                    }
                }
                //if (noMatches.Count > 0)
                //{
                //    ImportProgressInfoList.Add(new ImportProgressInfo { key = "失败", value = string.Format("缺少主键列：{0}", string.Join(",", noMatches)) });
                //    ImpTemplate.ImpResult = "失败";
                //    ImpTemplate.Resultdescription = string.Format("缺少主键列：{0}", string.Join(",", noMatches));
                //    throw new Exception(string.Format("缺少主键列：{0}", string.Join(",", noMatches)));
                //}
            }
            return false;
        }
        
    }
}
