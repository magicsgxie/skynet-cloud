using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class LoginLogRepository : ObjectRepository
    {
        public LoginLogRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public void Add(LoginLog info)
        {
            base.Add<LoginLog>(info);
        }

        public IQueryable<LoginLog> Query()
        {
            return base.CreateQuery<LoginLog>();
        }

        public LoginLog GetById(long loginID)
        {
            return base.GetByID<LoginLog>(loginID);
        }

        public void Add(IList<LoginLog> info)
        {
            base.Batch<long, LoginLog>(info, (u, v) => u.Insert(v));
        }

    }
}
