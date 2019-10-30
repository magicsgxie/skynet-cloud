using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Ccs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Ccs.Repository
{
    public class DictItemRepository : ObjectRepository
    {
        public DictItemRepository(IDbContext uow)
            : base(uow)
        {

        }

        public IQueryable<DictItem> GetDictItems()
        {
            return CreateQuery<DictItem>();
        }
    }
}
