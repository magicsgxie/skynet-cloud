using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Service.Interface;
using UWay.Skynet.Cloud.Cache;
using UWay.Skynet.Cloud.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace UWay.Skynet.Cloud.Dcs.Service
{
    /// <summary>
    /// 
    /// </summary>
    
    public class BaseFormulaService:IBaseFormulaService
    {
        private const string BASE_FORMULA_KEY = "__baseformula_";

        private const string BASE_FORMULA_KEY_COMBINE = "__baseformula_";

        private IDistributedCache _distributedCache;
        public BaseFormulaService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public long AddBaseFormula(BaseFormula item)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BaseFormulaRepository(context).AddBaseFormula(item);
            }
        }

        public int UpdateBaseFormula(BaseFormula item)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BaseFormulaRepository(context).UpdateBaseFormula(item);
            }
        }

        public int DeleteBaseFormulaById(long[] ids)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BaseFormulaRepository(context).DeleteBaseFormulaById(ids);
            }
        }

        public List<BaseFormula> GetFormulas()
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BaseFormulaRepository(context).GetFormulas().ToList();
            }
        }

        public List<BaseFormula> GetFormulasByFormulasCnName(string cnName)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BaseFormulaRepository(context).GetFormulasByFormulasCnName(cnName).ToList();
            }
        }

        public BaseFormula GetFormulaByID(long ID)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BaseFormulaRepository(context).GetFormulaByID(ID);
            }
        }

        public IEnumerable<BaseFormula> GetFormulas(IEnumerable<long> attids)
        {
            return GetPerfFormulaDatas().Where(p => attids.Contains(p.Key)).Select(o => o.Value);
        }

        private IDictionary<long, BaseFormula> GetCacheResult()
        {
            return _distributedCache.GetObject<IDictionary<long, BaseFormula>>(BASE_FORMULA_KEY);
        }

        private IDictionary<string, IEnumerable<BaseFormula>> GetCombineCacheResult()
        {
            return _distributedCache.GetObject< IDictionary<string, IEnumerable<BaseFormula>>> (BASE_FORMULA_KEY_COMBINE);
        }

        private void AddCache(IDictionary<long, BaseFormula> result)
        {
            _distributedCache.SetObjectAsync(BASE_FORMULA_KEY, result, new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(24 * 60 * 60 * 1000) });
        }

        private void AddCacheString(IDictionary<string, IEnumerable<BaseFormula>> result)
        {
            _distributedCache.SetObjectAsync(BASE_FORMULA_KEY_COMBINE, result, new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(24 * 60 * 60 * 1000) });
        }

        public BaseFormula GetById(long attId)
        {
            var result = GetPerfFormulaDatas();
            if (result != null && result.Count > 0)
            {
                if (result.ContainsKey(attId))
                {
                    return result[attId];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                using (var context = UnitOfWork.Get(Unity.ContainerName))
                {
                    return new FormulaBaseDataRepository(context).GetById(attId);
                }
            }
        }


        public IDictionary<long, BaseFormula> GetPerfFormulaDatas()
        {
            var result = GetCacheResult();
            if (result != null && result.Count > 0)
            {
                return result;
            }
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                result = new FormulaBaseDataRepository(context).GetPerfFormulaDatas();
                AddCache(result);
                AddCacheString(GetPerfFormulaDatas().Select(p => p.Value).GroupBy(t => string.Format("{0}_{1}_{2}_{3}", t.DataSource, t.NeType, t.VendorVersion, t.AttCnName.ToUpper())).ToDictionary<IGrouping<string, BaseFormula>, string, IEnumerable<BaseFormula>>(a => a.Key, b => b.ToList()));
            }
            return result;
        }

        public IEnumerable<BaseFormula> GetSubByCondition(int neType, int dataSourceType, string vendorVersion, string attCNName)
        {
            string key = string.Format("{0}_{1}_{2}_{3}", dataSourceType, neType, vendorVersion, attCNName);
            var result = GetBaseFormulaDictionary();
            if(result.ContainsKey(key))
            {
                return result[key];
            }
            return null;

        }

        ///private static IDictionary<string, IEnumerable<BaseFormula>> _baseFormulaDictionary = null;

        /// <summary>
        /// 公式字典 key = string.Format("{0}_{1}_{2}_{3}", t.DATASOURCE, t.NE_TYPE, t.VendorVersion, t.ATT_CNNAME.ToUpper())
        /// </summary>
        private IDictionary<string, IEnumerable<BaseFormula>> GetBaseFormulaDictionary()
        {
            var _baseFormulaDictionary = GetCombineCacheResult();


            if (_baseFormulaDictionary == null || _baseFormulaDictionary.Count <= 0)
            {
                //进行字典分割，加快检索效率 
                GetPerfFormulaDatas();
                _baseFormulaDictionary = GetCombineCacheResult();
            }
            return _baseFormulaDictionary;
            
        }
    }
}
