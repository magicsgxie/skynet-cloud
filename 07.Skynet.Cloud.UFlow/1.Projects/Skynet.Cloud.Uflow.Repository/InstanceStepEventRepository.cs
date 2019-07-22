/************************************************************************************
* Copyright (c) 2019-07-11 12:08:29 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：a9b8216b-28b4-4e48-a8ec-07fd57c8682f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:29 
* 描述：步骤前后期事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:29 
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
   /// 步骤前后期事件结果仓储类
   /// </summary>
   public class InstanceStepEventRepository:ObjectRepository
   {
      public InstanceStepEventRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepEvent  instanceStepEvent)
      {
         return Add<InstanceStepEvent>(instanceStepEvent);
      }
      /// <summary>
      /// 批量添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepEvent>  instanceStepEvents)
      {
         Batch<long, InstanceStepEvent>(instanceStepEvents, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepEvent  instanceStepEvent)
      {
         return Update<InstanceStepEvent>(instanceStepEvent);
      }
      /// <summary>
      /// 删除步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStepEvent>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public InstanceStepEvent GetById(string id)
      {
         return GetByID<InstanceStepEvent>(id);
      }
      /// <summary>
      /// 获取所有的步骤前后期事件结果{步骤前后期事件结果}对象
      /// </summary>
      public IQueryable<InstanceStepEvent> Query()
      {
         return CreateQuery<InstanceStepEvent>();
      }
   }
}
