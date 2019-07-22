/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  QueryGroupRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：7eb3baa7-4cb4-4262-9d42-e64aa8a1a383
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/16 14:42:25
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/16 14:42:25
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：过滤条件分组仓储
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    /// <summary>
    /// 过滤条件分组仓储
    /// </summary>
    public class QueryGroupRepository:ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public QueryGroupRepository(IDbContext context) : base(context)
        {
        }

        /// <summary>
        /// 创建过滤条件分组查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<QueryGroup> GetQueryGroups()
        {
            return CreateQuery<QueryGroup>();
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public QueryGroup GetQueryGroupByID(long ID)
        {
            return GetByID<QueryGroup>(ID);
        }

        /// <summary>
        /// 增加滤分组条件
        /// </summary>
        /// <returns></returns>
        public string AddQueryGroup(QueryGroup item)
        {
            return Add<QueryGroup,string>(item, p => p.ID);
        }

        /// <summary>
        /// 批量更新过滤分组条件
        /// </summary>
        /// <returns></returns>
        public void BacthAddQueryGroup(IEnumerable<QueryGroup> items)
        {
            Batch<QueryGroup, QueryGroup>(items, (u,v)=> u.Insert(v));
        }


        /// <summary>
        /// 更新过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int UpdateQueryGroup(QueryGroup item)
        {
            return Update<QueryGroup>(item);
        }

        /// <summary>
        /// 删除过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int DeleteQueryGroup(string[] ids)
        {
            return Delete<QueryGroup>(p => ids.Contains(p.ID) || ids.Contains(p.ParentID));
        }


        /// <summary>
        /// 删除过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int DeleteQueryGroupByTemplateID(long id)
        {
            return Delete<QueryGroup>(p => p.TemplateID == id);
        }

        /// <summary>
        /// 删除过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int DeleteQueryGroupByTemplateID(long[] ids)
        {
            return Delete<QueryGroup>(p => ids.Contains(p.TemplateID));
        }
    }
}
