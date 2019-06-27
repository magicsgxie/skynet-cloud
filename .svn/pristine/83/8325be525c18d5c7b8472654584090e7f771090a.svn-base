using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class OrgRepository : ObjectRepository
    {
        public OrgRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public int Add(OrgnizationInfo info)
        {
            return base.Add<OrgnizationInfo>(info);
        }


        public long Update(OrgnizationInfo info)
        {
            return base.Update<OrgnizationInfo>(info);
        }

        public OrgnizationInfo GetById(int loginID)
        {
            return base.GetByID<OrgnizationInfo>(loginID);
        }


        public long Delete(int[] ids)
        {
            return base.Delete<OrgnizationInfo>(p => ids.Contains(p.OrgID));
        }


        public IQueryable<OrgnizationInfo> Query()
        {
            return CreateQuery<OrgnizationInfo>();
        }
    }
}
