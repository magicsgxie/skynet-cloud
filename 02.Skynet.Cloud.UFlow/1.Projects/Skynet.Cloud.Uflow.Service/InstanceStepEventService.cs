/************************************************************************************
* Copyright (c) 2019-07-11 12:03:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：d7709f50-9aff-4702-915e-d3fcb6cb5523
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:12 
* 描述：步骤前后期事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:12 
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
   /// 步骤前后期事件结果服务实现类
   /// </summary>
   public class InstanceStepEventService: IInstanceStepEventService
   {
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepEvent  instanceStepEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepEventRepository(dbContext).Add(instanceStepEvent);
         }
      }
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepEvent>  instanceStepEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepEventRepository(dbContext).Add(instanceStepEvents);
         }
      }
      /// <summary>
      /// 更新步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepEvent  instanceStepEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepEventRepository(dbContext).Update(instanceStepEvent);
         }
      }
      /// <summary>
      /// 删除步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public InstanceStepEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
