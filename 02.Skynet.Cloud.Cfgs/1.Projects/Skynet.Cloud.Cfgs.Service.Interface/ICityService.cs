using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Cfgs.Entity;

namespace UWay.Skynet.Cloud.Cfgs.Service.Interface
{
    public interface ICityService
    {
        List<CityInfo> GetCitys();
    }
}
