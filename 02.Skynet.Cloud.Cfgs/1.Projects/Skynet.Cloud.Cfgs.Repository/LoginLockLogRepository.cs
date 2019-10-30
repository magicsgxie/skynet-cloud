using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class LoginLockLogRepository : ObjectRepository
    {
        public LoginLockLogRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public void Add(LoginLockLog info)
        {
            base.Add<LoginLockLog>(info);
        }

        public IQueryable<LoginLockLog> Query()
        {
            return base.CreateQuery<LoginLockLog>();
        }

        public void Update(LoginLockLog info)
        {
            Update<LoginLockLog>(info);
        }

        public void Add(IList<LoginLockLog> info)
        {
            base.Batch<long, LoginLockLog>(info, (u, v) => u.Insert(v));
        }

    }
}
