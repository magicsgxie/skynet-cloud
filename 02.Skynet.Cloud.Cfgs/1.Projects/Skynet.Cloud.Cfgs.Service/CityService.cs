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
    /// <summary>
    /// 城市信息
    /// </summary>
    public class CityService:ICityService
    {
        public void AddCityInfo(CityInfo item)
        {
            throw new NotImplementedException();
        }

        public void UpdateCityInfo(CityInfo item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int[] idArray)
        {
            throw new NotImplementedException();
        }

        public List<CityInfo> GetCitys()
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new CityRepository(uow).GetCitys();
            }
        }

        public List<CityInfo> GetSynsCitys()
        {
            throw new NotImplementedException();
        }
    }
}
