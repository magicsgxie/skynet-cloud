using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class LoginLockConfigRepsotiry : ObjectRepository
    {
        public LoginLockConfigRepsotiry(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public void Add(LoginLockConfig info)
        {
            base.Add<LoginLockConfig>(info);
        }

        public IQueryable<LoginLockConfig> Query()
        {
            return base.CreateQuery<LoginLockConfig>();
        }

        public void Update(LoginLockConfig info)
        {
            Update<LoginLockConfig>(info);
        }

        public void Add(IList<LoginLockConfig> info)
        {
            base.Batch<long, LoginLockConfig>(info, (u, v) => u.Insert(v));
        }

    }
}
