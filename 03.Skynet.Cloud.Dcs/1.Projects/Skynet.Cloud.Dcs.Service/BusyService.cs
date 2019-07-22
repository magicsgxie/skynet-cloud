using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Service.Interface;


namespace UWay.Skynet.Cloud.Dcs.Service
{
    
    public class BusyService : IBusyService
    {

        public List<BusyInfo> GetBusyInfos()
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new BusyRepository(context).GetBusyInfos().ToList();
            }
        }
    }
}
