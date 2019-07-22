/************************************************************************************
* Copyright (c) 2019-07-11 12:02:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：ece3986d-2fb8-4caa-82e9-a7224100c318
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:23 
* 描述：流程事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:23 
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
   /// 流程事件表服务实现类
   /// </summary>
   public class TemplateFlowEventService: ITemplateFlowEventService
   {
      /// <summary>
      /// 添加流程事件表{流程事件表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateFlowEvent  templateFlowEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventRepository(dbContext).Add(templateFlowEvent);
         }
      }
      /// <summary>
      /// 添加流程事件表{流程事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateFlowEvent>  templateFlowEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateFlowEventRepository(dbContext).Add(templateFlowEvents);
         }
      }
      /// <summary>
      /// 更新流程事件表{流程事件表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateFlowEvent  templateFlowEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventRepository(dbContext).Update(templateFlowEvent);
         }
      }
      /// <summary>
      /// 删除流程事件表{流程事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程事件表{流程事件表}对象(即:一条记录
      /// </summary>
      public TemplateFlowEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程事件表{流程事件表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
