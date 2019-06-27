using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.DataSource;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Upms.Service.Interface;
using UWay.Skynet.Cloud.Upms.Repository;

namespace UWay.Skynet.Cloud.Upms.Services
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
