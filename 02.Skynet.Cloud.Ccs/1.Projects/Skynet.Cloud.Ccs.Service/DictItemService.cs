using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Ccs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Ccs.Service.Interface;
using UWay.Skynet.Cloud.Ccs.Repository;

namespace UWay.Skynet.Cloud.Ccs.Services
{
    public class DictItemService:IDictItemService
    {
        public DictItemService()
        {
        }

        public void AddDictItem(DictItem item)
        {
            throw new NotImplementedException();
        }

        public void UpdateDictItem(DictItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int[] idArray)
        {
            throw new NotImplementedException();
        }

        public List<DictItem> GetDictItems(string dictType)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new DictItemRepository(uow).GetDictItems().Where(t=>t.DictType.Equals(dictType)).ToList();
            }
        }

        public List<DictItem> GetDictItems(string dictType, string dictCode)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new DictItemRepository(uow).GetDictItems().Where(t => t.DictType.Equals(dictType) && t.DictCode.Equals(dictCode)).ToList();
            }
        }
    }
}
