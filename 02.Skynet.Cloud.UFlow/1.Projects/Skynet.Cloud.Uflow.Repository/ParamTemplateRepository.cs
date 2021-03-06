/************************************************************************************
* Copyright (c) 2019-07-11 12:08:17 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.ParamTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：f5b451f9-d8cf-486b-bb46-cd1974546696
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:17 
* 描述：参数模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:17 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Repository
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using  UWay.Skynet.Cloud.Uflow.Entity;
   using System.Linq;
   using System.Collections.Generic;

   /// <summary>
   /// 参数模板仓储类
   /// </summary>
   public class ParamTemplateRepository:ObjectRepository
   {
      public ParamTemplateRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加参数模板{参数模板}对象(即:一条记录
      /// </summary>
      public long Add(ParamTemplate  paramTemplate)
      {
         return Add<ParamTemplate>(paramTemplate);
      }
      /// <summary>
      /// 批量添加参数模板{参数模板}对象(即:一条记录
      /// </summary>
      public void Add(IList<ParamTemplate>  paramTemplates)
      {
         Batch<long, ParamTemplate>(paramTemplates, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新参数模板{参数模板}对象(即:一条记录
      /// </summary>
      public int Update(ParamTemplate  paramTemplate)
      {
         return Update<ParamTemplate>(paramTemplate);
      }
      /// <summary>
      /// 删除参数模板{参数模板}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<ParamTemplate>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的参数模板{参数模板}对象(即:一条记录
      /// </summary>
      public ParamTemplate GetById(string id)
      {
         return GetByID<ParamTemplate>(id);
      }
      /// <summary>
      /// 获取所有的参数模板{参数模板}对象
      /// </summary>
      public IQueryable<ParamTemplate> Query()
      {
         return CreateQuery<ParamTemplate>();
      }
   }
}
