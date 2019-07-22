/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  QueryTemplateRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：4964b241-a084-4d25-a2a8-c27478d15f42
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/17 9:45:50
 * 描述：模板数据库仓储
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/17 9:45:50
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：模板数据库仓储
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
    /// 模板数据库仓储
    /// </summary>
    public class QueryTemplateRepository : ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public QueryTemplateRepository(IDbContext context) : base(context)
        {
        }

        /// <summary>
        /// 创建模板查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<QueryTemplate> GetQueryTemplates()
        {
            return CreateQuery<QueryTemplate>();
        }

        /// <summary>
        /// 根据主键ID查询模板
        /// </summary>
        /// <returns></returns>
        public QueryTemplate GetQueryTemplateByID(long ID)
        {
            return GetByID<QueryTemplate>(ID);
        }

        /// <summary>
        /// 根据主键ID查询模板
        /// </summary>
        /// <returns></returns>
        public long AddQueryTemplate(QueryTemplate item)
        {
            return Add<QueryTemplate, long>(item, p => p.TemplateID);
        }

        /// <summary>
        /// 批量添加模板
        /// </summary>
        /// <returns></returns>
        public void BacthAddQueryTemplate(QueryTemplate[] items)
        {
            Batch<QueryTemplate, QueryTemplate>(items, (u, v) => u.Insert(v));
        }


        /// <summary>
        /// 跟新模板
        /// </summary>
        /// <returns></returns>
        public int UpdateQueryTemplate(QueryTemplate item)
        {
            return Update<QueryTemplate>(item);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <returns></returns>
        public int DeleteQueryTemplate(long[] ids)
        {
            return Delete<QueryTemplate>(p => ids.Contains(p.TemplateID));
        }

    }
}
