using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Ccs.Entity;

namespace UWay.Skynet.Cloud.Ccs.Service.Interface
{
    public interface ICountyService
    {
        IList<CountyInfo> GetCountys();
    }
}
