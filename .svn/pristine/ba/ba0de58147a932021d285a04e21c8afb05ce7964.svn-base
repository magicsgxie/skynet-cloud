using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class RoleInfoRepository : ObjectRepository
    {
        public RoleInfoRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ext"></param>
        public void AddRole(RoleInfo item)
        {
            Add<RoleInfo>(item);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ext"></param>
        public void UpdateRole(RoleInfo item)
        {
            Update<RoleInfo>(item);
        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="idArray"></param>
        public void DeleteRole(int[] idArray)
        {
            Update<RoleInfo>(new { Invalid = DataDeleteStatusEnum.Invalid }, p => idArray.Any(u => u == p.RoleID));
        }



        public IQueryable<RoleInfo> GetRoles()
        {
            return CreateQuery<RoleInfo>();
        }


    }
}
