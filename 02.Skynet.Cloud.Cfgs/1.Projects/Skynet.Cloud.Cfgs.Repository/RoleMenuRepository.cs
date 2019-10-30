using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenuRepository : ObjectRepository
    {
        //public RoleMenuRepository(string containerName)
        //    : base(containerName)
        //{

        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元模式</param>
        public RoleMenuRepository(IDbContext uow)
            : base(uow)
        {

        }

        public int Add(RoleResourceInfo info)
        {
            return base.Add<RoleResourceInfo>(info);
        }

        public void Add(IList<RoleResourceInfo> info)
        {
            base.Batch<long, RoleResourceInfo>(info, (u, v) => u.Insert(v));
        }


        public IQueryable<RoleResourceInfo> Query()
        {
            return base.CreateQuery<RoleResourceInfo>();
        }


        public long DeleteByRoleId(long roleId)
        {
            return base.Delete<RoleResourceInfo>(p => p.RoleID.Equals(roleId));
        }
    }
}
