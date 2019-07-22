using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 基本公式接口
    /// </summary>
    
    public interface IInDicatorDictItemService
    {
        IDictionary<string, DictItem> GetInDicatorCfgs();

        DictItem GetById(int neType, string neLevel, string dictType = "Ne_Level");
    }
}
