using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class CountyRepository : ObjectRepository
    {
        public CountyRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public int Add(CountyInfo info)
        {
            return base.Add<CountyInfo>(info);
        }


        public IQueryable<CountyInfo> GetCountys()
        {
            return base.CreateQuery<CountyInfo>();
        }


        public long Update(CountyInfo info)
        {
            return base.Update<CountyInfo>(info);
        }

        public long Delete(long[] ids )
        {
            return base.Delete<CountyInfo>(p => ids.Contains(p.CountyID));
        }
    }
}
