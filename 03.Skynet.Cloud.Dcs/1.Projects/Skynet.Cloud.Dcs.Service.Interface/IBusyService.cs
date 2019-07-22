using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 基本公式接口
    /// </summary>
    
    public interface IBusyService
    {
        
        List<BusyInfo> GetBusyInfos();
    }
}
