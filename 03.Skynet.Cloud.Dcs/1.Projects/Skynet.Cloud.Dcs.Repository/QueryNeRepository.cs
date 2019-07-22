/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  QueryNeRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：69da4f82-5456-4d3a-a82e-2a65b1369966
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/28 10:29:37
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/28 10:29:37
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：
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
    /// 网元条件仓储
    /// </summary>
    public class QueryNeRepository:ObjectRepository
    {

        /// <summary>
        /// 网元条件仓储构造函数
        /// </summary>
        /// <param name="context"></param>
        public QueryNeRepository(IDbContext context):base(context)
        {

        }

        public int AddQueryNe(QueryNe item)
        {
            return Add<QueryNe>(item);
        }

        public int UpdateQueryNe(QueryNe item)
        {
            return Update<QueryNe>(item);
        }

        public int DeleteQueryNe(long templateID)
        {
            return Delete<QueryNe>(p =>  p.TemplateID == templateID);
        }

        public int DeleteQueryNe( IEnumerable<long> templateIDs)
        {
            return Delete<QueryNe>(p => templateIDs.Contains(p.TemplateID));
        }

        public int DeleteQueryNe(long[] templateIDs)
        {
            return Delete<QueryNe>(p => templateIDs.Contains(p.TemplateID ));
        }

        /// <summary>
        /// 获取网元信息
        /// </summary>
        /// <param name="obj">动态结构：{MenuID = 1, TemplateID = 1}</param>
        /// <returns></returns>
        public QueryNe GetQueryNe(object obj)
        {
            return GetByID<QueryNe>(obj);
        }
    }
}
