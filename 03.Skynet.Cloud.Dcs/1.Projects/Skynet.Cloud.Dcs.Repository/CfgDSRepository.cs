using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    public class CfgDSRepository:ObjectRepository
    {
        //public CfgDSRepository(string containerName):base(containerName)
        //{

        //}

        public CfgDSRepository(IDbContext uow):base(uow)
        {

        }

        public IQueryable<CfgDataSource> GetDataSource()
        {
            return CreateQuery<CfgDataSource>();
           // return CreateQuery<DataSource>("select * from CFG_INDICATOR_DATASOURCE").ToList();
        }
    }
}
