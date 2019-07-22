using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    public class BusyRepository:ObjectRepository
    {
        //public BusyRepository(string containerName):base(containerName)
        //{

        //}

        public BusyRepository(IDbContext uow):base(uow)
        {

        }

         public IQueryable<BusyInfo> GetBusyInfos()
        {
            return CreateQuery<BusyInfo>();
        }

    }
}
