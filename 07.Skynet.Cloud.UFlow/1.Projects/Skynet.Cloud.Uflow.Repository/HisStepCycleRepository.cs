/************************************************************************************
* Copyright (c) 2019-07-11 12:08:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：fb4669a9-db0b-4e0a-99da-b8ef6fb03ba3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:12 
* 描述：存放每一次循环处理的结果，把最后的结果更新到事件表中 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:12 
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
   public class HisStepCycleRepository:ObjectRepository
   {
      public HisStepCycleRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public long Add(HisStepCycle  hisStepCycle)
      {
         return Add<HisStepCycle>(hisStepCycle);
      }
      /// <summary>
      /// 批量添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepCycle>  hisStepCycles)
      {
         Batch<long, HisStepCycle>(hisStepCycles, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public int Update(HisStepCycle  hisStepCycle)
      {
         return Update<HisStepCycle>(hisStepCycle);
      }
      /// <summary>
      /// 删除存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepCycle>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public HisStepCycle GetById(string id)
      {
         return GetByID<HisStepCycle>(id);
      }
      /// <summary>
      /// 获取所有的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象
      /// </summary>
      public IQueryable<HisStepCycle> Query()
      {
         return CreateQuery<HisStepCycle>();
      }
   }
}
