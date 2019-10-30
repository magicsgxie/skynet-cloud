using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    /// <summary>
    /// 用户数据仓库
    /// </summary>
    public class UserRepository : ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow"></param>
        public UserRepository(IDbContext dbcontext)
            : base(dbcontext)
        {

        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ext"></param>
        public long AddUser(UserInfo item)
        {
            return Add<UserInfo,long>(item, p => p.UserID);
        }

        public void DeleteUserRoleInfo(int userID)
        {
            Delete<UserInfo>(p => p.UserID == userID);
            //Delete<RoleUserInfo>(new i => i.UserID == userID);
        }

        public void AddUserRoleInfo(RoleUserInfo item)
        {
            Add(item);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pwd"></param>
        public void UpdataPassword(int userID, string pwd)
        {
            Update<UserInfo>(new { Password = pwd }, p => p.UserID == userID);
        }

        /// <summary>
        /// 更新用户状态(0:解禁，1:禁用)
        /// </summary>
        /// <param name="userIdArray"></param>
        /// <param name="userstatus">true:解禁  false：禁用</param>
        public void UpdateUserStatus(int[] userIdArray, bool userstatus)
        {
            Update<UserInfo>(new { IsEnable = (userstatus ? 0 : 1) }, p => userIdArray.Any(u => u == p.UserID));
        }

        /// <summary>
        /// 更新用户状态(0:锁定，1:解锁)
        /// </summary>
        /// <param name="userIdArray"></param>
        /// <param name="userstatus">true:解锁 false:锁定</param>
        public void UpdateUserLocked(int[] userIdArray, bool userstatus)
        {
            Update<UserInfo>(new { IsLocked = (userstatus ? 0 : 1) }, p => userIdArray.Any(u => u == p.UserID));
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="idArray"></param>
        public void DeleteUser(long[] idArray)
        {
            Update<UserInfo>(new { Invalid = DataDeleteStatusEnum.Invalid }, p => idArray.Any(u => u == p.UserID));
        }

        public void DeleteUserRole(int[] idArray)
        {
            Delete<RoleUserInfo>(p => idArray.Any(u => u == p.UserID));
        }

        public List<RoleInfo> GetRoleInfo(int uid)
        {
            return CreateQuery<RoleInfo>().ToList();
        }

        public List<RoleUserInfo> GetUserRoles(int uid)
        {
            return CreateQuery<RoleUserInfo>().Where(p => p.UserID == uid).ToList();
        }

        /// <summary>
        /// 根据用户账号获取用户信息
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByUrAccount(string userAccount)
        {
            return CreateQuery<UserInfo>().FirstOrDefault(p => p.UserNo.Equals(userAccount, StringComparison.InvariantCultureIgnoreCase) && p.Invalid == 0);
        }

        public UserInfo GetUserInfoById(int uid)
        {
            return GetByID<UserInfo>(uid);
        }
        public IQueryable<UserInfo> GetUserInfo()
        {
            return CreateQuery<UserInfo>();
        }

       


        public DataSourceResult GetUsersByCondition(DataSourceRequest dataSourceRequest)
        {
            return CreateQuery<UserInfo>().ToDataSourceResult(dataSourceRequest);
        }

        public IQueryable<UserInfo> GetUsers()
        {
            return CreateQuery<UserInfo>();
        }

      
    }
}
