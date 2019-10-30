/************************************************************************************
* Copyright (c) 2019-07-11 12:02:21 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：73faa214-ae04-434e-9202-e1e327e85a11
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:21 
* 描述：事件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:21 
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
   /// 事件服务实现类
   /// </summary>
   public class TemplateEventService: ITemplateEventService
   {
      /// <summary>
      /// 添加事件{事件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateEvent  templateEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventRepository(dbContext).Add(templateEvent);
         }
      }
      /// <summary>
      /// 添加事件{事件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateEvent>  templateEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateEventRepository(dbContext).Add(templateEvents);
         }
      }
      /// <summary>
      /// 更新事件{事件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateEvent  templateEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventRepository(dbContext).Update(templateEvent);
         }
      }
      /// <summary>
      /// 删除事件{事件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的事件{事件}对象(即:一条记录
      /// </summary>
      public TemplateEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的事件{事件}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
