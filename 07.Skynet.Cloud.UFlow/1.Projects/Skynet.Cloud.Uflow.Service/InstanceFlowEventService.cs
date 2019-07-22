/************************************************************************************
* Copyright (c) 2019-07-11 12:03:09 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：8f0d2435-0036-4142-830f-363256521917
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:09 
* 描述：实例事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:09 
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
   /// 实例事件表服务实现类
   /// </summary>
   public class InstanceFlowEventService: IInstanceFlowEventService
   {
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceFlowEvent  instanceFlowEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowEventRepository(dbContext).Add(instanceFlowEvent);
         }
      }
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceFlowEvent>  instanceFlowEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceFlowEventRepository(dbContext).Add(instanceFlowEvents);
         }
      }
      /// <summary>
      /// 更新实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceFlowEvent  instanceFlowEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowEventRepository(dbContext).Update(instanceFlowEvent);
         }
      }
      /// <summary>
      /// 删除实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public InstanceFlowEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
