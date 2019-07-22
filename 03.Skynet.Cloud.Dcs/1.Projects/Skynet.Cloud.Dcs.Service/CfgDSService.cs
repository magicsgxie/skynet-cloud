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
    
    public class CfgDSService : ICfgDSService
    {
        public List<CfgDataSource> GetDataSource()
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new CfgDSRepository(context).GetDataSource().ToList();
            }
        }

        public CfgDataSource GetByUqlType(UqlType uqlType)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new CfgDSRepository(context).GetDataSource().SingleOrDefault(p => p.ParseType == uqlType);
            }
        }
    }
}
