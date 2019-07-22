/************************************************************************************
* Copyright (c) 2019-07-11 12:07:37 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：05fcef7d-8e8e-4fca-90ce-08b8a663578b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:37 
* 描述：流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:37 
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
   /// 流程表仓储类
   /// </summary>
   public class TemplateFlowRepository:ObjectRepository
   {
      public TemplateFlowRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程表{流程表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateFlow  templateFlow)
      {
         return Add<TemplateFlow>(templateFlow);
      }
      /// <summary>
      /// 批量添加流程表{流程表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateFlow>  templateFlows)
      {
         Batch<long, TemplateFlow>(templateFlows, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程表{流程表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateFlow  templateFlow)
      {
         return Update<TemplateFlow>(templateFlow);
      }
      /// <summary>
      /// 删除流程表{流程表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateFlow>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程表{流程表}对象(即:一条记录
      /// </summary>
      public TemplateFlow GetById(string id)
      {
         return GetByID<TemplateFlow>(id);
      }
      /// <summary>
      /// 获取所有的流程表{流程表}对象
      /// </summary>
      public IQueryable<TemplateFlow> Query()
      {
         return CreateQuery<TemplateFlow>();
      }
   }
}
