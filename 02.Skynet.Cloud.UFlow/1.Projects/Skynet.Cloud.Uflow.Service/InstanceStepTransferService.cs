/************************************************************************************
* Copyright (c) 2019-07-11 12:03:16 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：b55b01ea-296f-48be-bb92-ff38b3de1b2e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:16 
* 描述：记录下引擎对每个转发控制转发的结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:16 
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
   /// 记录下引擎对每个转发控制转发的结果服务实现类
   /// </summary>
   public class InstanceStepTransferService: IInstanceStepTransferService
   {
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepTransfer  instanceStepTransfer)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepTransferRepository(dbContext).Add(instanceStepTransfer);
         }
      }
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepTransfer>  instanceStepTransfers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepTransferRepository(dbContext).Add(instanceStepTransfers);
         }
      }
      /// <summary>
      /// 更新记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepTransfer  instanceStepTransfer)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepTransferRepository(dbContext).Update(instanceStepTransfer);
         }
      }
      /// <summary>
      /// 删除记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepTransferRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public InstanceStepTransfer GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepTransferRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepTransferRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
