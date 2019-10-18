using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    public static class DataRowExtention
    {
        /// <summary>
        /// 根据列名获取当前行对应的值
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetColumnValue(this DataRow currentRow, string colName)
        {
            if (currentRow.Table.Columns.Contains(colName))
            {
                var value = currentRow[colName];
                
                if (value != null)
                {
                    return value.ToString();
                }
                    
            }
            return string.Empty;
        }

        ///// <summary>
        ///// 根据列名获取当前行对应的值
        ///// </summary>
        ///// <param name="currentRow"></param>
        ///// <param name="colName"></param>
        ///// <returns></returns>
        //public static string GetColumnValue(this DataRow currentRow, string colName)
        //{
        //    if (currentRow.Table.Columns.Contains(colName))
        //    {
        //        var value = currentRow[colName];

        //        if (value != null)
        //        {
        //            return value.ToString();
        //        }

        //    }
        //    return string.Empty;
        //}

        /// <summary>
        /// 根据列名获取当前行对应的值
        /// </summary>
        /// <param name="dataTemplate"></param>
        /// <param name="currentRow"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetColumnValue(this List<ImportDataTemplateField> datafields, DataRow currentRow, string colName)
        {
            string value = currentRow.GetColumnValue(colName);
            if (string.IsNullOrEmpty(value))
            {
                var field = datafields.Where(f => f.Fieldname == colName).FirstOrDefault();
                if (field != null)
                {
                    return currentRow.GetColumnValue(field.Fieldtext);
                }
            }
            return value;
        }

        //ExcelDataField
        public const string DATABASEOPERATE = "DataBaseOperate"; //指示是更新、插入、更新错误、插入错误 
        public const string DATABASEERRORINFO = "DataBaseErrorInfo"; //指示错误信息
        public const string DATABASESQL = "DataBaseSql"; //sql语句
        public const string DATABASEROWGUIDID = "RowGuidID"; //RowGuidID
        public const string DATABASEIMPID = "ImpId"; //ImpId
        public const string DATABASEIMPCITY = "ImpCity"; //ImpCity
        public const string DATABASEIMPTIME = "ImpTime"; //ImpTime
        public const string MultiCheck = "MultiCheck";
        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="errorMessage"></param>
        public static void SetErrorInfo(this DataRow currentRow, string errorMessage)
        {
            currentRow.SetValue(DATABASEOPERATE,"错误");
            var currentError = currentRow.GetColumnValue(DATABASEERRORINFO);
            if (string.IsNullOrEmpty(currentError))
            {
                currentRow.SetValue(DATABASEERRORINFO,errorMessage);
            }
            else
            {
                currentRow.SetValue(DATABASEERRORINFO, currentError + "；" + errorMessage);
            }
        }

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="errorMessage"></param>
        public static void SetValue(this DataRow currentRow, string fieldName,object value)
        {
            currentRow[fieldName] = value;
        }

    }
}
