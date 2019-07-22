using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Services.Interface
{
    public interface IPerfService
    {

        /// <summary>
        /// 获取最新时间性能数据
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="neCellIds"></param>
        /// <param name="indicatorIds"></param>
        /// <returns></returns>
        DataTable QueryIndicator(NetType netType, DataBaseType dataBaseType, string neCellIds, string indicatorIds, string dateTime);
    }
}
