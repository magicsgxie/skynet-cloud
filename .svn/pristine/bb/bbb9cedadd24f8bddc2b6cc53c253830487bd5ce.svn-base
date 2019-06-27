using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.DataSource;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Upms.Service.Interface;
using UWay.Skynet.Cloud.Upms.Repository;

namespace UWay.Skynet.Cloud.Upms.Services
{
    /// <summary>
    /// 城市信息
    /// </summary>
    public class LoginLogService : ILoginLogService
    {
        public long Add(LoginLog user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new LoginLogRepository(uow).Add(user);
            }
            return 1;
        }

        public LoginLog GetById(long loginID)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new LoginLogRepository(uow).GetById(loginID);
            }
            
        }

        public DataSourceResult GetByRequest(DataSourceRequest request)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new LoginLogRepository(uow).Query().ToDataSourceResult(request);
            }

        }
    }
}
