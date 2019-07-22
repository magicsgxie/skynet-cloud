/************************************************************************************
* Copyright (c) 2019-07-11 12:07:36 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：befc87c2-6f38-48df-8a06-36b5cec7c734
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:36 
* 描述：事件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:36 
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
   /// 事件仓储类
   /// </summary>
   public class TemplateEventRepository:ObjectRepository
   {
      public TemplateEventRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加事件{事件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateEvent  templateEvent)
      {
         return Add<TemplateEvent>(templateEvent);
      }
      /// <summary>
      /// 批量添加事件{事件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateEvent>  templateEvents)
      {
         Batch<long, TemplateEvent>(templateEvents, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新事件{事件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateEvent  templateEvent)
      {
         return Update<TemplateEvent>(templateEvent);
      }
      /// <summary>
      /// 删除事件{事件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateEvent>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的事件{事件}对象(即:一条记录
      /// </summary>
      public TemplateEvent GetById(string id)
      {
         return GetByID<TemplateEvent>(id);
      }
      /// <summary>
      /// 获取所有的事件{事件}对象
      /// </summary>
      public IQueryable<TemplateEvent> Query()
      {
         return CreateQuery<TemplateEvent>();
      }
   }
}
