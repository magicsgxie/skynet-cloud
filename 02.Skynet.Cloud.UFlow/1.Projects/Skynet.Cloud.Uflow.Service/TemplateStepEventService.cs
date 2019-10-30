/************************************************************************************
* Copyright (c) 2019-07-11 12:02:31 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：b881f95f-97c2-49c7-9cbe-c5144518d43c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:31 
* 描述：步骤事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:31 
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
   /// 步骤事件表服务实现类
   /// </summary>
   public class TemplateStepEventService: ITemplateStepEventService
   {
      /// <summary>
      /// 添加步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepEvent  templateStepEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepEventRepository(dbContext).Add(templateStepEvent);
         }
      }
      /// <summary>
      /// 添加步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepEvent>  templateStepEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepEventRepository(dbContext).Add(templateStepEvents);
         }
      }
      /// <summary>
      /// 更新步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepEvent  templateStepEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepEventRepository(dbContext).Update(templateStepEvent);
         }
      }
      /// <summary>
      /// 删除步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public TemplateStepEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤事件表{步骤事件表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
