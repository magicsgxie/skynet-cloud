using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Nom.Repository;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;
using System.Data;
using Skynet.Cloud.Noap;

namespace UWay.Skynet.Cloud.Nom.Services
{
    /// <summary>
    /// 网元分组
    /// </summary>
    
    public class NeGroupService : INeGroupService
    {
        /// <summary>
        /// 添加网元分组
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="item"></param>
        public int AddNeGroup(NetType netType, NeGroup item)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName( DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                    return r.AddNeGroup(item);
                }

            }
        }
        /// <summary>
        /// 更新网元分组
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="item"></param>
        public int UpdateNeGroup(NetType netType, NeGroup item)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                   return r.UpdateNeGroup(item);
                }

            }
        }

        /// <summary>
        /// 更新网元分组
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="item"></param>
        public int UpdateNeGroupName(NetType netType, int neGroupId, string neGroupName)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                    return r.UpdateNeGroupName(neGroupId, neGroupName);
                }

            }
        }

        /// <summary>
        /// 删除网元分组
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="idArrays"></param>
        public string DeleteNeGroup(NetType netType, int[] idArrays)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                   int a=  r.DeleteNeGroup(idArrays);
                   int b = r.DelNeGroupItems(idArrays);
                   return a + "," + b;
                }

            }
        }

        /// <summary>
        /// 删除网元分组和网元对应关系
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="idArrays">网元列表</param>
        public int DelNeGroupItems(NetType netType, int[] idArrays)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                   return r.DelNeGroupItems(idArrays);
                }
            }
        }

        /// <summary>
        /// 添加网元分组和网元之间的关系
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="item">网元</param>
        public int AddNeGroupItem(NetType netType, NeGroupItem item)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                   return r.AddNeGroupItem(item);
                }

            }
        }
        /// <summary>
        /// 更新网元分组和网元之间的关系
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="item">网元</param>
        public void UpdateNeGroupItem(NetType netType, NeGroupItem item)
        {
            using (var uow = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeGroupRepository(uow))
                {
                    r.DelNeGroupItems(new int[] { item.GroupID });
                    r.AddNeGroupItem(item);
                }

            }
        }

        /// <summary>
        /// 获取网元分组
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="conditions">动态条件</param>
        /// <returns></returns>
        public List<NeGroup> GetNeGroups(NetType netType, List<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeGroupRepository(context).GetNeGroups().Where(conditions).ToList();

            }
        }

        /// <summary>
        /// 获取网元下的所属网元
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="conditions">动态条件</param>
        public List<NeGroupItem> GetNeGroupItems(NetType netType, List<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeGroupRepository(context).GetNeGroupItems().ToList();
            }
        }

        /// <summary>
        /// 根据网元分组ID获取网元下的所属网元
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="groupId">网元分组ID</param>
        /// <param name="neLevel">网元级别</param>
        /// <returns></returns>
        public DataTable GetNeGroupItemsByGroupID(NetType netType, int groupId, int neLevel)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeGroupRepository(context).GetNeGroupItemsByGroupID(netType, groupId, neLevel);
            }
        }

        /// <summary>
        /// 匹配查询
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<NeGroup> GetNeGroupByFilter(NetType netType, List<IFilterDescriptor> conditions, string filter)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeGroupRepository(context).GetNeGroups().Where(conditions)
                    .Where(p => p.GroupName.ToUpper().Contains(filter.ToUpper())).ToList();
            }
        }
        
    }
}
