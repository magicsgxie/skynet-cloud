/************************************************************************************
* Copyright (c) 2019-07-11 12:03:02 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：4ea711a0-74e3-4917-a149-2c32bf58afa4
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:02 
* 描述：记录下引擎对每个转发控制转发的结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:02 
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
   public class HisStepTransferService: IHisStepTransferService
   {
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public long Add(HisStepTransfer  hisStepTransfer)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepTransferRepository(dbContext).Add(hisStepTransfer);
         }
      }
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepTransfer>  hisStepTransfers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepTransferRepository(dbContext).Add(hisStepTransfers);
         }
      }
      /// <summary>
      /// 更新记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Update(HisStepTransfer  hisStepTransfer)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepTransferRepository(dbContext).Update(hisStepTransfer);
         }
      }
      /// <summary>
      /// 删除记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepTransferRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public HisStepTransfer GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepTransferRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepTransferRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
