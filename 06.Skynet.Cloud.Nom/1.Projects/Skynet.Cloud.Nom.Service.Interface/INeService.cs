using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.Nom.Service.Interface
{
    /// <summary>
    /// 网元类
    /// </summary>
  
    public interface INeService
    {
        /// <summary>
        /// 城市获取厂家
        /// </summary>
        /// <param name="neType">网络制式</param>
        /// <param name="citys"></param>
        /// <returns></returns>
        
        List<string> GetNeVendorCodesByCitys(NetType neType, int[] citys);

        /// <summary>
        /// 获取网元厂家编码
        /// </summary>
        /// <param name="neType"></param>
        /// <returns></returns>
        
        string[] GetNeVendorCodes(NetType neType);

        /// <summary>
        /// 获取基站
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        NeBts[] GetNeBtsByCondition(NetType neType, IList<IFilterDescriptor> conditions);

        /// <summary>
        /// 获取TOP N基站
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="topN"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        NeBts[] GetTopNeBtsByCondition(NetType neType, int topN, IList<IFilterDescriptor> conditions);


        /// <summary>
        /// 根据场景获取小区
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="conditions"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>

        List<NeCell> GetCellByGroup(NetType netType, IList<IFilterDescriptor> filters);


        /// <summary>
        /// 分页获取基站
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="conditions"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        DataSourceResult BtsPage(NetType neType, DataSourceRequest request);

        /// <summary>
        /// 分页获取基站
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="conditions"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        DataSourceResult<NeBts> GetNeBtsPageByCondition(NetType neType, DataSourceRequest request);
        /// <summary>
        /// 统计全网基站数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        int GeStaticBtsAllCount(NetType netType);

        /// <summary>
        /// 统计全网扇区数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        int GeStaticCellAllCount(NetType netType);

        /// <summary>
        /// 按城市统计网元数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        List<Pair> GetStaticNeCellCityCount(NetType netType);

        /// <summary>
        /// 按城市统计基站数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        List<Pair> GetStaticNeBtsCityCount(NetType netType);

        /// <summary>
        /// 统计行政区基站数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        List<Pair> GetStaticNeBtsCountyCount(NetType netType);

        /// <summary>
        /// 统计行政区扇区数据
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        List<Pair> GetStaticNeCellCountyCount(NetType netType);


        /// <summary>
        /// 获取小区信息
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        NeCell[] GetNeCellByCondition(NetType neType, IList<IFilterDescriptor> conditions);

        /// <summary>
        /// 获取排名TOP的小区
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="topN"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        NeCell[] GetTopNeCellByCondition(NetType neType,int topN, IList<IFilterDescriptor> conditions);

        /// <summary>
        /// 分页获取小区
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="conditions"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        DataSourceResult GetNeCellPageByCondition(NetType neType, DataSourceRequest request);
        
        /// <summary>
        /// 分页获取BSC或者MME
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        NeBscOrMme[] GetNeBscOrMmeByCondition(NetType neType, IList<IFilterDescriptor> conditions);

        /// <summary>
        /// 根据查询级别获取厂家
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="neRelateionInfo"></param>
        /// <param name="extensionNeRelation"></param>
        /// <param name="neWhere"></param>
        /// <returns></returns>
        string[] GetNeVendorCodesByQueryLevel(NetType netType, string table, string neWhere);

        /// <summary>
        /// 获取C网或者L网信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <returns></returns>
        List<NeCell> GetBasicNeCell(NetType neType);

        /// <summary>
        /// 获取C网或L网基站信息
        /// </summary>
        /// <param name="neType"></param>
        /// <returns></returns>
        List<NeBts> GetBasicNeBts(NetType neType);

        /// <summary>
        /// 获取网元信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <returns></returns>
        NeCell GetBasicNeCellById(NetType neType, string neCellId);

        /// <summary>
        /// 获取网元信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="neCellIds">制式</param>
        /// <returns></returns>
        List<NeCell> GetBasicNeCellByIds(NetType neType, List<string> neCellIds);

        ///// <summary>
        ///// 获取制定网元的邻区
        ///// </summary>
        ///// <param name="neType">制式</param>
        ///// <param name="neCellId">网元id</param>
        ///// <returns></returns>
        //IDictionary<NetType, IEnumerable<NeCell>> GetNeiCells(NetType neType, string neCellId);

    }
}
