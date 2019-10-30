/************************************************************************************
* Copyright (c) 2019-07-11 12:08:05 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.EventTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：d374d91b-5953-429d-b0f7-651f3e982c93
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:05 
* 描述：事件模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:05 
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
   /// 事件模板仓储类
   /// </summary>
   public class EventTemplateRepository:ObjectRepository
   {
      public EventTemplateRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public long Add(EventTemplate  eventTemplate)
      {
         return Add<EventTemplate>(eventTemplate);
      }
      /// <summary>
      /// 批量添加事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public void Add(IList<EventTemplate>  eventTemplates)
      {
         Batch<long, EventTemplate>(eventTemplates, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public int Update(EventTemplate  eventTemplate)
      {
         return Update<EventTemplate>(eventTemplate);
      }
      /// <summary>
      /// 删除事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<EventTemplate>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public EventTemplate GetById(string id)
      {
         return GetByID<EventTemplate>(id);
      }
      /// <summary>
      /// 获取所有的事件模板{事件模板}对象
      /// </summary>
      public IQueryable<EventTemplate> Query()
      {
         return CreateQuery<EventTemplate>();
      }
   }
}
