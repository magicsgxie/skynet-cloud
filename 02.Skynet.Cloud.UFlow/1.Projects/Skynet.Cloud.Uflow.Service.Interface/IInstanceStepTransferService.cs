/************************************************************************************
* Copyright (c) 2019-07-11 12:03:16 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：ba59bd66-b0fa-4cdc-9963-2626dc9d934a
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



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 记录下引擎对每个转发控制转发的结果服务接口类
   /// </summary>
   public interface IInstanceStepTransferService
   {
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepTransfer  instanceStepTransfer);
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepTransfer>  instanceStepTransfers);
      /// <summary>
      /// 更新记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepTransfer  instanceStepTransfer);
      /// <summary>
      /// 删除记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      InstanceStepTransfer GetById(string id);
      /// <summary>
      /// 获取所有的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
