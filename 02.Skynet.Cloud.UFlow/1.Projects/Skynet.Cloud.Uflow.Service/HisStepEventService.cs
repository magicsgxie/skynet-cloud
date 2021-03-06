/************************************************************************************
* Copyright (c) 2019-07-11 12:02:57 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：2814483b-49db-4100-bf25-7cf0073cb493
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:57 
* 描述：步骤前后期事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:57 
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
   public class HisStepEventService: IHisStepEventService
   {
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisStepEvent  hisStepEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepEventRepository(dbContext).Add(hisStepEvent);
         }
      }
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepEvent>  hisStepEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepEventRepository(dbContext).Add(hisStepEvents);
         }
      }
      /// <summary>
      /// 更新步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisStepEvent  hisStepEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepEventRepository(dbContext).Update(hisStepEvent);
         }
      }
      /// <summary>
      /// 删除步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public HisStepEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
