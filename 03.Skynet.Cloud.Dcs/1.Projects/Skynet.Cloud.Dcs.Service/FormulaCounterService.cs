using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Service.Interface;
using UWay.Skynet.Cloud.Cache;

using UWay.Skynet.Cloud.Dcs.Entity;
using System.Data;
using UWay.Skynet.Cloud.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace UWay.Skynet.Cloud.Dcs.Service
{
    
    public class FormulaCounterService : IFormulaCounterService
    {
        private const string BASE_FORMULA_COUNTER_KEY = "__baseformula_counter_";

        private const string BASE_FORMULA_COUNTER_KEY_COMBINE = "__baseformula_counter_";

        private IDistributedCache _distributedCache;
        public FormulaCounterService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        private IEnumerable<FormulaCounter> GetCacheResult()
        {
            return _distributedCache.GetObject<IEnumerable<FormulaCounter>>(BASE_FORMULA_COUNTER_KEY);
        }


        private void AddCache( IEnumerable<FormulaCounter> result)
        {
            _distributedCache.SetObjectAsync(BASE_FORMULA_COUNTER_KEY, result, new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(24 * 60 * 60 * 1000) });
        }



        public IEnumerable<FormulaCounter> GetFormulaCounter()
        {
            var result = GetCacheResult();
            if(result != null && result.Count() > 0)
            {
                return result;
            }
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var counters = new FormulaCounterRepository(context).GetFormulaCounters();
                var nelevelRels = new NeLevelRelRepository(context).GetNeLevelRelInfos().ToArray();
                var list = new List<FormulaCounter>();
                if (counters != null && counters.Rows.Count > 0)
                {
                    foreach (DataRow row in counters.Rows)
                    {
                        FormulaCounter formula = new FormulaCounter();

                        formula.NeType = Convert.ToInt32(row["NE_TYPE"]);
                        formula.NeLevel = Convert.ToInt32(row["NE_LEVEL"]);
                        formula.BusinessType = Convert.ToInt32(row["BUSINESS_TYPE"]);
                        formula.VendorVersion = row["VENDOR"].ToString() + row["VERSION"].ToString();
                        formula.DataSource = Convert.ToInt32(row["DATASOURCE"]);

                        formula.FieldName = row["FIELD_NAME"].ToString();
                        formula.CltFieldName = row["CLT_FIELD_NAME"].ToString();
                        formula.CounterName = row["COUNTER_NAME"].ToString();
                        formula.CltTableName = row["CLT_TABLE_NAME"].ToString();
                        formula.TimeAggregation = row["TIME_AGGREGATION"].ToString();
                        formula.NeAggregation = row["NE_AGGREGATION"].ToString();

                        string TableName = "";
                        //
                        foreach (var relat in nelevelRels)
                        {
                            if (!row.Table.Columns.Contains(relat.CounterTableName) || relat.NeType != formula.NeType)
                                continue;
                            TableName = row[relat.CounterTableName].ToString();
                            if (formula.TableName == null)
                                formula.TableName = new Dictionary<string, string>();
                            if (!formula.TableName.ContainsKey(string.Format("{0}_{1}", formula.NeType, relat.QueryNeLevel % 100)))
                                formula.TableName.Add(string.Format("{0}_{1}", formula.NeType, relat.QueryNeLevel % 100), TableName);
                        }
                        list.Add(formula);
                    }
                }
                 
                AddCache(result);
                return result;
            }
        }

        public IEnumerable<FormulaCounter> GetFormulaCounterBy(int type, int dataSourcetType, string vendor, string phraseResult)
        {
            string key = string.Format("{0}_{1}_{2}_{3}", dataSourcetType, type, vendor, phraseResult);
            var list = GetFormulaCounter();
            var result = list.GroupBy(p => string.Format("{0}_{1}_{2}_{3}", p.DataSource, p.NeType, p.VendorVersion, p.CltFieldName))
                   .ToDictionary<IGrouping<string, FormulaCounter>, string, IEnumerable<FormulaCounter>>(a => a.Key, b => b.ToList());
            if (result.ContainsKey(key))
                return result[key];
            return null;
        }
    }
}
