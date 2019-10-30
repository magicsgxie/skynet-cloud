using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Ccs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Ccs.Repository
{
    public class CityRepository : ObjectRepository
    {
        //public CityRepository(string containerName)
        //    : base(containerName)
        //{

        //}

        public CityRepository(IDbContext uow)
            : base(uow)
        {

        }

        public List<CityInfo> GetCitys()
        {
            List<CityInfo> citys = CreateQuery<CityInfo>().OrderBy(p => p.ShowSerial).ToList();
            foreach (var item in citys)
            {
                if (!item.LatitudeCenter.HasValue && item.LatitudeLB.HasValue && item.LatitudeRB.HasValue)
                {
                    item.LatitudeCenter = (item.LatitudeLB.Value + item.LatitudeRB.Value) / 2;
                }

                if (!item.LongitudeCenter.HasValue && item.LongitudeLB.HasValue && item.LongitudeRB.HasValue)
                {
                    item.LongitudeCenter = (item.LongitudeLB.Value + item.LongitudeRB.Value) / 2;
                }
            }
            return citys;
        }
    }
}
