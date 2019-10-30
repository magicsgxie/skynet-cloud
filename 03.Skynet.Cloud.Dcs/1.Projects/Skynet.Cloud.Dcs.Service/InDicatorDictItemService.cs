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
using UWay.Skynet.Cloud.Ccs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Service
{
    
    public class InDicatorDictItemService : IInDicatorDictItemService
    {

        private IDistributedCache _distributedCache;

        private const string IN_DICATOR_CONFIG_KEY = "__in_dicator_cfg_";

        public InDicatorDictItemService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        } 



        private void AddCache(IDictionary<string, DictItem> result)
        {
            _distributedCache.SetObjectAsync(IN_DICATOR_CONFIG_KEY, result);
        }

        private IDictionary<string, DictItem> GetCaches()
        {
            return _distributedCache.GetObject<IDictionary<string, DictItem>>(IN_DICATOR_CONFIG_KEY);
        }

        public IDictionary<string , DictItem> GetInDicatorCfgs()
        {
            IDictionary<string, DictItem> result = GetCaches();
            if(result == null || result.Count == 0)
            {
                using (var context = UnitOfWork.Get(Unity.ContainerName))
                {
                    result = new InDicatorDictItemRepository(context).GetDictItems().ToDictionary(p => string.Format("{0}_{1}_{2}", p.DictType, p.DictCode, p.RelateInfo));
                    AddCache(result);

                }
            }
            return result;
        }

        public DictItem GetById(int neType, string neLevel, string dictType= "Ne_Level")
        {
            string strNeLevel = neLevel.ToString();
            if (strNeLevel.Length > 2)//级别
            {
                strNeLevel = strNeLevel.Substring(1);
            }
            else
            {
                strNeLevel = strNeLevel.PadLeft(2, '0');
            }

            IDictionary<string, DictItem> result = GetCaches();
            if(result == null || result.Count == 0)
            {
                using (var context = UnitOfWork.Get(Unity.ContainerName))
                {
                    return new InDicatorDictItemRepository(context).GetDictItem(dictType, string.Format("{0}{1}", neType, neLevel), "CHANGE_CARR");
                }
            } else
            {
                if(result.ContainsKey(string.Format("{0}_{1}{2}_{3}", dictType, neType, neLevel, "CHANGE_CARR")))
                {
                    return result[string.Format("{0}_{1}{2}_{3}", dictType, neType, neLevel, "CHANGE_CARR")];
                }
            }
            return null;
        }
    }
}
