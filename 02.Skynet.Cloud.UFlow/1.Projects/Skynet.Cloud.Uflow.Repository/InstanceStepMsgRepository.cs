/************************************************************************************
* Copyright (c) 2019-07-11 12:08:31 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：fb03d433-d969-4aec-aafb-3808c0ada1a8
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:31 
* 描述：步骤消息处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:31 
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
   /// 步骤消息处理仓储类
   /// </summary>
   public class InstanceStepMsgRepository:ObjectRepository
   {
      public InstanceStepMsgRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepMsg  instanceStepMsg)
      {
         return Add<InstanceStepMsg>(instanceStepMsg);
      }
      /// <summary>
      /// 批量添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepMsg>  instanceStepMsgs)
      {
         Batch<long, InstanceStepMsg>(instanceStepMsgs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepMsg  instanceStepMsg)
      {
         return Update<InstanceStepMsg>(instanceStepMsg);
      }
      /// <summary>
      /// 删除步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStepMsg>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public InstanceStepMsg GetById(string id)
      {
         return GetByID<InstanceStepMsg>(id);
      }
      /// <summary>
      /// 获取所有的步骤消息处理{步骤消息处理}对象
      /// </summary>
      public IQueryable<InstanceStepMsg> Query()
      {
         return CreateQuery<InstanceStepMsg>();
      }
   }
}
