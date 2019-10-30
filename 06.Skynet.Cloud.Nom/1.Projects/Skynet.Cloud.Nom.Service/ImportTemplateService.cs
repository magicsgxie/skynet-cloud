using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Skynet.Cloud.Noap;
using Steeltoe.Common.Discovery;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Discovery.Abstract;
using UWay.Skynet.Cloud.IE;
using UWay.Skynet.Cloud.IE.Core;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Nom.Repository;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Nom.Service
{
    public class ImportTemplateService : BaseDiscoveryService, IImportTemplateService
    {

        private IImporter _importer;

        public ImportTemplateService(IImporter importer,IDiscoveryClient client, ILogger logger):base(client, logger)
        {
            _importer = importer;
        }

        public IEnumerable<ImportDataField> GetImportDataFields(int templateId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImportTemplate> GetImportDataTemplates()
        {
            throw new NotImplementedException();
        }

        private DataTable GeImportDataTemplate(int templateId, string user)
        {
            DataTable dt = new DataTable();
            using(var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var tr = new ImportDataRepository(context);
                var tdfr = new ImportDataFieldRepository(context);
                var template = tr.Get(templateId);
                var dataFields = tdfr.CreateQuery().Where(p => p.Templateid == templateId).ToList();
                var enumField = dataFields.Select(p => template.ImpTablename + "_" + p.Fieldname).ToList();
                var er = new EnumsRepository(context);
                var enumTypes = er.GetEnumTypes();
                var enumValues = er.GetEnumValues();
                var enums = (from p in enumTypes
                            join v in enumValues
                            on p.Id equals v.TypeId
                            where enumField.Contains(p.NameEn)
                            select new
                            {
                                Name = p.NameEn,
                                Key = v.Name,
                                value = v.Value
                            }).ToList();
                var dicEnums = enums.GroupBy(p => p.Name).ToDictionary(o => o.Key);
                dt.AddSheetName(template.TemplateName);
                
                //var citys = base.Invoke<CityInfo>()
                var citys = GetCitysAsync(user);
                var countys = GetCountysAsync(user);
                foreach(var item in dataFields)
                {
                    DataColumn dc = new DataColumn()
                    {
                        ColumnName = item.Fieldname,
                        Caption = item.Fieldtext,
                        MaxLength = item.Fieldlength??0,
                        DataType = item.GetFieldType()
                    };
                    dc.AddRequire((item.Isneed.Equals("Y") || item.Isneed.Equals("是") || item.Isneed.Equals("true")));
                    if(dicEnums.ContainsKey(template.ImpTablename+"_"+item.Fieldname))
                    {
                        var dics = dicEnums[template.ImpTablename + "_" + item.Fieldname].ToDictionary(p => p.Name, v => v.value);
                        dc.AddDictionary(dics);
                    } else if(item.Fieldname.ToUpper().Contains("CITY"))
                    {
                        dc.AddDictionary(citys.Result.ToDictionary(p => p.CityName, v => v.CityID));
                    } else if(item.Fieldname.ToUpper().Contains("COUNTY") || item.Fieldname.ToUpper().Contains("ADM_AREA"))
                    {
                        dc.AddDictionary(countys.Result.ToDictionary(p => p.CountyName, v => v.CountyID));
                    }
                }
                dt.TableName = template.ImpTablename;
            }
            return dt;
        }

        private const string CITY_URL = "http://skynet-cloud-dics/api/citys";
        private const string COUNTY_URL = "http://skynet-cloud-dics/api/countys";

        private Task<IList<CityInfo>> GetCitysAsync(string user)
        {
            
            return Invoke<IList<CityInfo>>(new HttpRequestMessage(HttpMethod.Get, $"{CITY_URL}?name={user}"));
        }

        private Task<IList<CountyInfo>> GetCountysAsync(string user)
        {
            return Invoke<IList<CountyInfo>>(new HttpRequestMessage(HttpMethod.Get, $"{COUNTY_URL}?name={user}"));
        }

        public ImportMsgResult Import(string filename, int templateId, string user)
        {
            var dt = GeImportDataTemplate(templateId, user);
            var result = this._importer.Import(filename, dt);
            result.Wait();
            var dataResult = result.Result;
            var data = dataResult.Data;
            var oldData = GetOldDataTable(templateId, data);
            
            return null;
        }

        protected virtual DataTable GetOldDataTable(int templateId, DataTable excelData)
        {
            SelectQuery selectQuery = new SelectQuery();
            selectQuery.FromClause.BaseTable = FromTerm.Table(excelData.TableName);
            var primaryKeyName = new List<string>();
            foreach(DataColumn item in excelData.Columns)
            {
                selectQuery.Columns.Add(new SelectColumn( item.ColumnName));
                if(item.PrimaryKey() == true)
                {
                    primaryKeyName.Add(item.ColumnName);
                }
            }

            var i = 0;
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            selectQuery.WherePhrase = new WhereClause(FilterCompositionLogicalOperator.Or);
            foreach (DataRow item in  excelData.Rows)
            {
                var clause = new WhereClause(FilterCompositionLogicalOperator.And);
                var j = 0;
                
                foreach (var key in primaryKeyName)
                {
                    clause.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field(key), SqlExpression.Parameter(string.Format("{0}{1}{2}", key, i, j)), FilterOperator.IsEqualTo));
                    parameters.Add(string.Format("{0}{1}{2}", key,i , j), item[key]);
                    j++;

                }
                selectQuery.WherePhrase.SubClauses.Add(clause);
                i++;
            }

            using (var context = UnitOfWork.Get(NetType.LTE.ToContainerName(DataBaseType.Normal)))
            {
                var tr = new ImportDataRepository(context);
                return tr.CreateQuery(selectQuery, parameters);
            }
            //return null;
        }

        public ImportMsgResult Import(Stream fileStream, int templateId, string user)
        {
            throw new NotImplementedException();
        }
    }
}
