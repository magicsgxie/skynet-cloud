using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class UserPwdConfigRepository : ObjectRepository
    {
        public UserPwdConfigRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public int Add(PwdConfigInfo info)
        {
            return base.Add<PwdConfigInfo>(info);
        }


        public long Update(PwdConfigInfo info)
        {
            return base.Update<PwdConfigInfo>(info);
        }

        public IQueryable<PwdConfigInfo> Query()
        {
            return base.CreateQuery<PwdConfigInfo>();
        }

    }
}
