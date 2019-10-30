using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class MenuRepository : ObjectRepository
    {
        public MenuRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public int AddMenu(ResourceInfo item)
        {
            return Add<ResourceInfo, int>(item, p => p.MenuID);
        }

        //public int DeleteMenuByRoleID(int[] idArray)
        //{
        //    return Delete<MenuInfo>(p => idArray.Contains(p.RoleId));
        //}

        //public int DeleteMenuByUserID(int[] idArray)
        //{
        //    return Delete<MenuInfo>(p => idArray.Contains(p.UserId));
        //}

        public IQueryable<ResourceInfo> GetMenus()
        {
            return CreateQuery<ResourceInfo>();
        }
    }
}
