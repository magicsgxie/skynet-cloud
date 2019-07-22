using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Upms.Service.Interface;
using UWay.Skynet.Cloud.Upms.Repository;

namespace UWay.Skynet.Cloud.Upms.Services
{
    /// <summary>
    /// 城市信息
    /// </summary>
    public class UserService : IUserService
    {
        public long AddUser(UserInfo user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new UserRepository(uow).AddUser(user);
            }
        }

        public void DeleteUser(long[] arrayIds)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new UserRepository(uow).DeleteUser(arrayIds);
            }
        }

        public UserInfo GetUserByUserNo(string userNo)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new UserRepository(uow).GetUserInfoByUrAccount(userNo);
            }
        }

        public DataSourceResult GetUsersByRequest(DataSourceRequest dataSourceRequest)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new UserRepository(uow).GetUserInfo().ToDataSourceResult(dataSourceRequest);
            }
        }

        public int UpdateUser(UserInfo user)
        {
            throw new NotImplementedException();
        }
    }
}
