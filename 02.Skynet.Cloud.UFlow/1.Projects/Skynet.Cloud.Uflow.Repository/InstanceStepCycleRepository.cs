/************************************************************************************
* Copyright (c) 2019-07-11 12:08:29 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：ec43de6f-8504-4bea-ba08-0f94713f0474
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:29 
* 描述：存放每一次循环处理的结果，把最后的结果更新到事件表中 
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
   /// 存放每一次循环处理的结果，把最后的结果更新到事件表中仓储类
   /// </summary>
   public class InstanceStepCycleRepository:ObjectRepository
   {
      public InstanceStepCycleRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepCycle  instanceStepCycle)
      {
         return Add<InstanceStepCycle>(instanceStepCycle);
      }
      /// <summary>
      /// 批量添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepCycle>  instanceStepCycles)
      {
         Batch<long, InstanceStepCycle>(instanceStepCycles, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepCycle  instanceStepCycle)
      {
         return Update<InstanceStepCycle>(instanceStepCycle);
      }
      /// <summary>
      /// 删除存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStepCycle>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public InstanceStepCycle GetById(string id)
      {
         return GetByID<InstanceStepCycle>(id);
      }
      /// <summary>
      /// 获取所有的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象
      /// </summary>
      public IQueryable<InstanceStepCycle> Query()
      {
         return CreateQuery<InstanceStepCycle>();
      }
   }
}
