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
    public class OperateService : IOperateService
    {
        public long Add(OperationLog user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new OperateLogRepository(uow).Add(user);
            }
            return 1;
        }

        public int DeleteByIds(DateTime[] arrayIds)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new OperateLogRepository(uow).Delete(arrayIds);
            }
            return 1;
        }

        public OperationLog GetById(DateTime dt)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
               return  new OperateLogRepository(uow).GetById(dt);
            }
            
        }

        public DataSourceResult GetByRequest(DataSourceRequest dataSourceRequest)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new OperateLogRepository(uow).Query().ToDataSourceResult(dataSourceRequest);
            }
            
        }

        public int Update(OperationLog user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new OperateLogRepository(uow).Add(user);
            }
            return 1;
        }
    }
}
