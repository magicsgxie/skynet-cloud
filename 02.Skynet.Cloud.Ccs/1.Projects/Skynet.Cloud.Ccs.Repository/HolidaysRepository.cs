using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Ccs.Entity;

namespace UWay.Skynet.Cloud.Ccs.Repository
{
    public class HolidaysRepository : ObjectRepository
    {
        public HolidaysRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public void Add(Holiday info)
        {
            base.Add<Holiday>(info);
        }

        public IQueryable<Holiday> Query()
        {
            return base.CreateQuery<Holiday>();
        }

        public void Update(Holiday info)
        {
            Update<Holiday>(info);
        }

        public void Update(IList<Holiday> info)
        {

            base.Batch<int, Holiday>(info, (u, v) => u.Update(v));
        }

        public void Add(IList<Holiday> info)
        {
            base.Batch<long, Holiday>(info, (u, v) => u.Insert(v));
        }

        public long Delete(long[] ids)
        {
            return base.Delete<Holiday>(p => ids.Contains(p.HolidayId));
        }
    }
}
