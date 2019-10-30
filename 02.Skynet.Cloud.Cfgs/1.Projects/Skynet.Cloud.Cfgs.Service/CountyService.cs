using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Cfgs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Cfgs.Service.Interface;
using UWay.Skynet.Cloud.Cfgs.Repository;

namespace UWay.Skynet.Cloud.Cfgs.Services
{
    public class CountyInfoService : ICountyService
    {
        public IList<CountyInfo> GetCountys()
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new CountyRepository(uow).GetCountys().ToList();
            }
        }
    }
}
