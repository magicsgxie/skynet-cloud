/************************************************************************************
* Copyright (c) 2019-07-11 12:02:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.EventTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：7d7935a4-3895-4fc1-ad7f-c032de354879
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:49 
* 描述：事件模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:49 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Service
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;

   using System.Linq;
   using UWay.Skynet.Cloud.Uflow.Service.Interface;
   using UWay.Skynet.Cloud.Uflow.Repository;
   using UWay.Skynet.Cloud.Linq;
   using UWay.Skynet.Cloud;

   /// <summary>
   /// 事件模板服务实现类
   /// </summary>
   public class EventTemplateService: IEventTemplateService
   {
      /// <summary>
      /// 添加事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public long Add(EventTemplate  eventTemplate)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new EventTemplateRepository(dbContext).Add(eventTemplate);
         }
      }
      /// <summary>
      /// 添加事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public void Add(IList<EventTemplate>  eventTemplates)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new EventTemplateRepository(dbContext).Add(eventTemplates);
         }
      }
      /// <summary>
      /// 更新事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public int Update(EventTemplate  eventTemplate)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new EventTemplateRepository(dbContext).Update(eventTemplate);
         }
      }
      /// <summary>
      /// 删除事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new EventTemplateRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public EventTemplate GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new EventTemplateRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的事件模板{事件模板}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new EventTemplateRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
