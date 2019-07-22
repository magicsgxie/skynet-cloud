/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Ufa.Enterprise.Services.Interface.Admin
 * 文件名：  IQueryTemplateSerice
 * 版本号：  V1.0.0.0
 * 唯一标识：f7cadc22-0916-4073-84c5-e9ad2e7aa9ab
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/21 15:06:48
 * 描述：模板管理
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/21 15:06:48
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：模板管理
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
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 模板管理
    /// </summary>
   
    public interface IQueryTemplateService
    {
        /// <summary>
        /// 分页查询模板信息
        /// </summary>
        /// <param name="condition">条件集合</param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        DataSourceResult GetQueryTemplatePaginations(DataSourceRequest dataSourceRequest);

        long AddQueryTemplate(QueryTemplateView item);

        bool IsExistsTemplateName(string templateName, int menuID);

        int UpdateQueryTemplates(QueryTemplateView item);


        QueryTemplateView GetQueryTemplateByID(long templateID);
    }
}
