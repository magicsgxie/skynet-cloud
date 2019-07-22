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
    public class OrgService : IOrgService
    {
        public long Add(OrgnizationInfo user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new OrgRepository(uow).Add(user);
            }
            return 1;
        }

        public int DeleteByIds(int[] arrayIds)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new OrgRepository(uow).Delete(arrayIds);
            }
            return 1;
        }

        public OrgnizationInfo GetById(int orgId)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new OrgRepository(uow).GetById(orgId);
            }
            
        }

        public DataSourceResult GetByRequest(DataSourceRequest dataSourceRequest)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new OrgRepository(uow).Query().ToDataSourceResult(dataSourceRequest);
            }
            
        }

        public int Update(OrgnizationInfo user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new OrgRepository(uow).Add(user);
            }
            return 1;
        }
    }
}
