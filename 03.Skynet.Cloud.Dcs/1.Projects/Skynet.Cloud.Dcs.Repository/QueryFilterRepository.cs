/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  QueryFilterRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：52c94a1b-68a6-4348-a38f-0336535233e2
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/16 14:41:34
 * 描述：过滤字段仓储
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/16 14:41:34
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：过滤字段仓储
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
    /// 过滤字段仓储
    /// </summary>
    public class QueryFilterRepository : ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public QueryFilterRepository(IDbContext context) : base(context)
        {
        }

        /// <summary>
        /// 创建过滤条件分组查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<QueryFilter> GetQueryFilters()
        {
            return CreateQuery<QueryFilter>();
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <param name="item">结构如下:new { AttID = 100001, GroupID = 30000 }</param>
        /// <returns></returns>
        public QueryFilter GetQueryFilterByID(object item)
        {
            return GetByID<QueryFilter>(item);
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int AddQueryFilter(QueryFilter item)
        {
            return Add<QueryFilter>(item);
        }


        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public void AddBatchQueryFilter(IEnumerable<QueryFilter> items)
        {
            Batch<QueryFilter, QueryFilter>(items, (u, v) => u.Insert(v));
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int UpdateQueryFilter(QueryFilter item)
        {
            return Update<QueryFilter>(item);
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int DeleteQueryFilterByGroupID(IEnumerable<string> ids)
        {
            return Delete<QueryFilter>(p => ids.Contains(p.GroupID));
        }

    }
}
