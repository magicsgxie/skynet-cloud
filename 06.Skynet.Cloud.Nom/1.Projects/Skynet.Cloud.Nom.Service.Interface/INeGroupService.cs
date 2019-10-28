using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Linq;
using Skynet.Cloud.Noap;

namespace UWay.Skynet.Cloud.Nom.Service.Interface
{
    
    public interface INeGroupService
    {
        
        int AddNeGroup(NetType netType, NeGroup item);

        
        int UpdateNeGroup(NetType netType, NeGroup item);
        /// <summary>
        /// 更新网元分组
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="item"></param>
        int UpdateNeGroupName(NetType netType, int neGroupId, string neGroupName);

        
        string DeleteNeGroup(NetType netType, int[] idArrays);

        
        int DelNeGroupItems(NetType netType, int[] idArrays);

        
        int AddNeGroupItem(NetType netType, NeGroupItem item);

        
        void UpdateNeGroupItem(NetType netType, NeGroupItem item);

        
        List<NeGroup> GetNeGroups(NetType netType, List<IFilterDescriptor> conditions);


        //List<TreeLayer> GetCitysByCondition(NetType netType, List<QueryParameter> conditions);

        List<NeGroupItem> GetNeGroupItems(NetType netType, List<IFilterDescriptor> conditions);



        /// <summary>
        /// 根据网元分组ID获取网元
        /// </summary>
        /// <param name="netType">网络类型</param>
        /// <param name="neGroupId">网元分组ID</param>
        /// <param name="neLevel">网元级别</param>
        /// <returns></returns>
        
        DataTable GetNeGroupItemsByGroupID(NetType netType, int groupId, int neLevel);

        /// <summary>
        /// 匹配查询
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        
        List<NeGroup> GetNeGroupByFilter(NetType netType, List<IFilterDescriptor> conditions, string filter);
    }
}
