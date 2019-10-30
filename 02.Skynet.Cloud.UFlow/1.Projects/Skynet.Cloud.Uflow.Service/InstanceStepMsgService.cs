/************************************************************************************
* Copyright (c) 2019-07-11 12:03:13 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：939fd2eb-717e-4e5f-b987-85bcb9e1bf79
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:13 
* 描述：步骤消息处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:13 
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
   /// 步骤消息处理服务实现类
   /// </summary>
   public class InstanceStepMsgService: IInstanceStepMsgService
   {
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepMsg  instanceStepMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepMsgRepository(dbContext).Add(instanceStepMsg);
         }
      }
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepMsg>  instanceStepMsgs)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepMsgRepository(dbContext).Add(instanceStepMsgs);
         }
      }
      /// <summary>
      /// 更新步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepMsg  instanceStepMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepMsgRepository(dbContext).Update(instanceStepMsg);
         }
      }
      /// <summary>
      /// 删除步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepMsgRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public InstanceStepMsg GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepMsgRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepMsgRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
