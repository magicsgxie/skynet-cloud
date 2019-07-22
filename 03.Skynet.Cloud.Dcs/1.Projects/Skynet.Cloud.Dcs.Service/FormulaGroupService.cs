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
    
    public class FormulaGroupService : IFormulaGroupService
    {
        public long AddFormulaGroup(FormulaGroup item)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new FormulaGroupRepository(context).AddFormulaGroup(item);
            }
        }

        public int DeleteFormulaGroup(long[] ids)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new FormulaGroupRepository(context).DeleteFormulaGroup(ids);
            }
        }

        public List<FormulaGroup> GetFormulaGroup()
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new FormulaGroupRepository(context).GetFormulaGroup().ToList();
            }
        }

        public FormulaGroup GetFormulaGroupById(long id)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new FormulaGroupRepository(context).GetFormulaGroupById(id);
            }
        }

        public int UpdateFormulaGroup(FormulaGroup item)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new FormulaGroupRepository(context).UpdateFormulaGroup(item);
            }
        }
    }
}
