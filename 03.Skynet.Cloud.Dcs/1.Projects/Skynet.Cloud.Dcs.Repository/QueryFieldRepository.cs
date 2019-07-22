/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  OrderByFieldRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：2607889f-ca18-4f38-9469-e91bf6461a4a
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/16 14:41:59
 * 描述：排序显示字段仓储
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/16 14:41:59
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：排序显示字段仓储
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
    ///排序显示字段仓储
    /// </summary>
    public class QueryFieldRepository : ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public QueryFieldRepository(IDbContext context) : base(context)
        {
        }

        /// <summary>
        /// 创建过滤条件分组查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<QueryField> GetQueryFields()
        {
            return CreateQuery<QueryField>();
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <param name="item">结构如下:new { AttID = 100001, TemplateID = 30000 }</param>
        /// <returns></returns>
        public QueryField GetQueryFieldByID(object item)
        {
            return GetByID<QueryField>(item);
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int AddQueryField(QueryField item)
        {
            return Add<QueryField>(item);
        }


        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public void AddBatchQueryField(IEnumerable<QueryField> items)
        {
            Batch<QueryField, QueryField>(items, (u, v) => u.Insert(v));
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int UpdateQueryField(QueryField item)
        {
            return Update<QueryField>(item);
        }

        /// <summary>
        /// 根据主键ID查询过滤分组条件
        /// </summary>
        /// <returns></returns>
        public int DeleteQueryFieldByTemplateID(long[] ids)
        {
            return Delete<QueryField>(p => ids.Contains(p.TemplateID));
        }

    }
}
