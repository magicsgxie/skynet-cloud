/************************************************************************************
* Copyright (c) 2019-07-11 12:08:34 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：edd5a236-b226-451c-8804-69f3c64c33cc
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:34 
* 描述：记录下引擎对每个转发控制转发的结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:34 
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
   /// 记录下引擎对每个转发控制转发的结果仓储类
   /// </summary>
   public class InstanceStepTransferRepository:ObjectRepository
   {
      public InstanceStepTransferRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepTransfer  instanceStepTransfer)
      {
         return Add<InstanceStepTransfer>(instanceStepTransfer);
      }
      /// <summary>
      /// 批量添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepTransfer>  instanceStepTransfers)
      {
         Batch<long, InstanceStepTransfer>(instanceStepTransfers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepTransfer  instanceStepTransfer)
      {
         return Update<InstanceStepTransfer>(instanceStepTransfer);
      }
      /// <summary>
      /// 删除记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStepTransfer>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public InstanceStepTransfer GetById(string id)
      {
         return GetByID<InstanceStepTransfer>(id);
      }
      /// <summary>
      /// 获取所有的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象
      /// </summary>
      public IQueryable<InstanceStepTransfer> Query()
      {
         return CreateQuery<InstanceStepTransfer>();
      }
   }
}
