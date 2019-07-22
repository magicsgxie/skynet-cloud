/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.36366
 * 机器名称：UWAY
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Services
 * 文件名：  FormulaBaseDataService
 * 版本号：  V1.0.0.0
 * 唯一标识：76133a3b-e311-4b78-bfc4-d6e1afb38aaa
 * 当前的用户域：UWay
 * 创建人：  谢韶光
 * 电子邮箱：magic.s.g.xie@uway.cn
 * 创建时间：2019/6/26 15:35:15
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2019/6/26 15:35:15
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：
 * 
 * 
 * 
 * 
 ************************************************************************************/
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Service.Interface;


namespace UWay.Skynet.Cloud.Dcs.Service
{
    /// <summary>
    /// 基础数据服务类
    /// </summary>
    
    public class FormulaBaseDataService : IFormulaBaseDataService
    {
        private const string BASE_FORMULA_KEY = "__baseformula_";

        private const string BASE_DATA_KEY = "__basedata_";

        private IDistributedCache _distributedCache;
        public FormulaBaseDataService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public IList<FormulaBaseData> GetFormulaBaseDatas()
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {

                return new FormulaBaseDataRepository(context).GetFormulaBaseDatas().ToList();
            }
        }

        //public IEnumerable<BaseFormula> GetFormulas(IEnumerable<long> attids)
        //{
        //    return GetPerfFormulaDatas().Where(p => attids.Contains(p.Key)).Select(o => o.Value);
        //}

        //private IDictionary<long, BaseFormula> GetCacheResult()
        //{
        //    return _distributedCache.GetObject<IDictionary<long, BaseFormula>>(BASE_FORMULA_KEY);
        //}

        private IDictionary<long, FormulaBaseData> GetBaseDataCacheResult()
        {
            return _distributedCache.GetObject<IDictionary<long, FormulaBaseData>>(BASE_FORMULA_KEY);
        }

        //private void AddCache(IDictionary<long, BaseFormula> result)
        //{
        //    _distributedCache.SetObjectAsync(BASE_FORMULA_KEY, result, new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(24 * 60 * 60 * 1000) });
        //}

        private void AddCache(IDictionary<long, FormulaBaseData> result)
        {
            _distributedCache.SetObjectAsync(BASE_DATA_KEY, result, new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(24 * 60 * 60 * 1000) });
        }

       
    }
}
