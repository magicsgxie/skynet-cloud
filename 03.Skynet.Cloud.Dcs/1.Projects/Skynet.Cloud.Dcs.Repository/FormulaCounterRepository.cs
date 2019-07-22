using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Cache;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    public class FormulaCounterRepository:ObjectRepository
    {

       

        //public FormulaCounterRepository(string containerName):base(containerName)
        //{

        //}

        public FormulaCounterRepository(IDbContext uow):base(uow)
        {
            
        }

        public DataTable GetFormulaCounters()
        {
            return ExecuteDataTable("select * from cfg_indicator_counter");

        }

        //public DataTable GetFormulaCounterDataTable()
        //{
        //    return DataTable(@"select * from cfg_indicator_counter ");
        //}
    }
}
