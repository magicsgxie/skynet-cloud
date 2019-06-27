using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class RoleUserRepository:ObjectRepository
    {
        //public RoleUserRepository(string containerName)
        //    : base(containerName)
        //{

        //}
        public RoleUserRepository(IDbContext uow):base(uow)
        {

        }

        public int Add(RoleUserInfo info)
        {
            return base.Add<RoleUserInfo>(info);
        }

        public void Add(IList<RoleUserInfo> info)
        {
            base.Batch<long, RoleUserInfo>(info, (u, v) => u.Insert(v));
        }


        public IQueryable<RoleUserInfo> Query()
        {
            return base.CreateQuery<RoleUserInfo>();
        }


        public long DeleteByRoleId(long roleId)
        {
            return base.Delete<RoleUserInfo>(p => p.RoleID.Equals(roleId));
        }
    }
}
