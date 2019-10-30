/************************************************************************************
* Copyright (c) 2019-07-11 12:07:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：b516b855-23e7-4283-b473-b8c4a0078b5d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:44 
* 描述：步骤事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:44 
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
   /// 步骤事件表仓储类
   /// </summary>
   public class TemplateStepEventRepository:ObjectRepository
   {
      public TemplateStepEventRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepEvent  templateStepEvent)
      {
         return Add<TemplateStepEvent>(templateStepEvent);
      }
      /// <summary>
      /// 批量添加步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepEvent>  templateStepEvents)
      {
         Batch<long, TemplateStepEvent>(templateStepEvents, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepEvent  templateStepEvent)
      {
         return Update<TemplateStepEvent>(templateStepEvent);
      }
      /// <summary>
      /// 删除步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepEvent>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public TemplateStepEvent GetById(string id)
      {
         return GetByID<TemplateStepEvent>(id);
      }
      /// <summary>
      /// 获取所有的步骤事件表{步骤事件表}对象
      /// </summary>
      public IQueryable<TemplateStepEvent> Query()
      {
         return CreateQuery<TemplateStepEvent>();
      }
   }
}
