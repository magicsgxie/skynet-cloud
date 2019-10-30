using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Cfgs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    public class InDicatorDictItemRepository : ObjectRepository
    {

        public InDicatorDictItemRepository(IDbContext uow):base(uow)
        {

        }

        public IEnumerable<DictItem> GetDictItems()
        {
            return Query<DictItem>("SELECT * FROM SY_CFG_INDICATOR_DICTITEM");
        }

        public DictItem GetDictItem(string dictType, string dictCode, string relationInfo)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("dictType", dictType);
            parameters.Add("dictCode", dictCode);
            parameters.Add("relationInfo", relationInfo);
            return SingleOrDefault<DictItem>("SELECT * FROM SY_CFG_INDICATOR_DICTITEM WHERE Dict_Type = : dictType AND Dict_Code = :dictCode AND Relate_Info = :relationInfo", parameters);
        }
    }
}
