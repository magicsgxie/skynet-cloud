using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Nom.Repository
{
    /// <summary>
    /// 网元分组，场景
    /// </summary>
    public class NeGroupRepository:ObjectRepository
    {

        
        public NeGroupRepository(IDbContext uow):base(uow)
        {

        }

        /// <summary>
        /// 获取网元分组最大ID
        /// </summary>
        /// <returns></returns>
        private int GetNeGroupId()
        {
            var q = CreateQuery<NeGroup>();
            var a = from b in q
                    select b.GroupID;
            return a.Max();
        }

        public int AddNeGroup(NeGroup item)
        {
            item.GroupID = GetNeGroupId() + 1;
            return Add<NeGroup>(item);
        }

        public int UpdateNeGroup(NeGroup item)
        {
            return Update<NeGroup>(item);
        }

        /// <summary>
        /// 更新网元分组名称
        /// </summary>
        /// <param name="neGroupId">网元分组ID</param>
        /// <param name="neGroupName">网元分组名称</param>
        /// <returns></returns>
        public int UpdateNeGroupName(int neGroupId, string neGroupName)
        {
            return Update<NeGroup>(new { GroupName = neGroupName }, p => p.GroupID == neGroupId);
        }

        public int DeleteNeGroup(int[] idArrays)
        {
            return Delete<NeGroup>(p => idArrays.Contains(p.GroupID));
            //ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE group_id in ({1}) ", tableName, idArrays.Concat()));
        }

        public int DelNeGroupItems(int[] idArrays)
        {
            return Delete<NeGroupItem>(p => idArrays.Contains(p.GroupID));
            //ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE group_id in ({1}) ", tableName, idArrays.Concat()));
        }

        public int AddNeGroupItem(NeGroupItem item)
        {
           return Add<NeGroupItem>(item);
            //
        }

        public void UpdateNeGroupItem(NeGroupItem item, string tableName)
        {
            DelNeGroupItems(new int[] { item.GroupID });
            AddNeGroupItem(item);
            //
        }

        public IQueryable<NeGroup> GetNeGroups()
        {
            return CreateQuery<NeGroup>();
        }

        public IQueryable<NeGroupItem> GetNeGroupItems()
        {
            return CreateQuery<NeGroupItem>();
        }

        /// <summary>
        /// 根据网元分组ID获取网元下的所属网元
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="groupId">网元分组ID</param>
        /// <param name="neLevel">网元级别</param>
        /// <returns></returns>
        public DataTable GetNeGroupItemsByGroupID(NetType netType, int groupID, int neLevel)
        {
            //var q = CreateQuery<NeGroupItem>();
            //var query = from c in q
            //            where c.GroupID == groupID
            //            select c;
            //return query.ToList().Concat(query.ToList().SelectMany(t => GetNeGroupItemsByGroupID(t.GroupID)));
            string neGroupTable = "mod_negroup_group";//三期库
            string neGroupItemTable = "mod_negroup_ne";//三期库
            string neLevelTable = "NE_BTS_C";
            if (netType == NetType.LTE)//四期库
            {
                neGroupTable = "mod_negroup_group_l";
                neGroupItemTable = "mod_negroup_ne_l";
            }
            switch (neLevel)
            {
                case 3://基站
                    neLevelTable = "NE_BTS_C";
                    break;
                case 4://扇区
                    neLevelTable = "NE_CELL_C";
                    break;
                case 5://载频
                    neLevelTable = "NE_CARRIER_C";
                    break;
                default:
                    break;
            }
            string sql = @"select distinct 'OMC:' || C.OMC_ID || 'BSC:' || C.BSC_ID || 'BTS:' ||
                C.BTS_ID || C.NE_SYS_ID AS MyKey,
                'OMC:' || C.OMC_ID || 'BSC:' || C.BSC_ID || C.RELATED_BSC AS GROUPBY,
                gp.GROUP_ID,
                gp.GROUP_NAME,
                C.BTS_ID as BTS,
                gp.Level_Id as LE,
                ne.ne_sys_id,
                ne.CHINA_NAME,
                '' as ANT_AZIMUTH,
                '' as pn,
                C.VENDOR,
                LONGITUDE,
                LATITUDE,
                C.CITY_NAME
  from (select *
          from {1}
         start with group_id = {0}
        connect by parentid = prior group_id) gp
 inner join {2} ne
    on gp.group_id = ne.group_id
 INNER join {3} C
    on ne.ne_sys_id = C.Ne_Sys_Id
 order by ne.CHINA_NAME";
            return ExecuteDataTable(string.Format(sql, groupID, neGroupTable, neGroupItemTable, neLevelTable));
        }

        ///// <summary>
        ///// 匹配查询
        ///// </summary>
        ///// <param name="conditions"></param>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //public IQueryable<NeGroup> GetNeGroupByFilter(List<IFilterDescriptor> conditions, string filter)
        //{
        //    return CreateQuery<NeGroup>().Where(conditions).Where(p => p.GroupName.ToUpper().Contains(filter.ToUpper()));
        //}

        //public IEnumerable<NeGroup> GetChildGroupsByCondition(int parentID, int cityID, int levelid)
        //{
        //    var q = CreateQuery<NeGroup>().Where(p => p.ParentID == parentID && p.CityID == cityID  && p.LevelID == levelid).ToList();
        //    return q.SelectMany(t => GetChildGroupsById(t.GroupID));
        //}


        //public IQueryable<NeGroup> GetChildGroupsById(NetType netType,int groupId)
        //{
        //    var tbl = netType.NeGroup();
        //    //var q = CreateQuery<NeGroup>().Where(p => p.ParentID == groupId);
        //    //return q.Concat(q.SelectMany(t => GetChildGroupsById(t.GroupID)));
        //}
    }
}
