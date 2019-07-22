/************************************************************************************
* Copyright (c) 2019-07-11 12:02:56 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：aa4709ed-e71c-4bb5-a155-6668a1bb2498
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:56 
* 描述：存放每一次循环处理的结果，把最后的结果更新到事件表中 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:56 
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
   /// 存放每一次循环处理的结果，把最后的结果更新到事件表中服务实现类
   /// </summary>
   public class HisStepCycleService: IHisStepCycleService
   {
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public long Add(HisStepCycle  hisStepCycle)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepCycleRepository(dbContext).Add(hisStepCycle);
         }
      }
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepCycle>  hisStepCycles)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepCycleRepository(dbContext).Add(hisStepCycles);
         }
      }
      /// <summary>
      /// 更新存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public int Update(HisStepCycle  hisStepCycle)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepCycleRepository(dbContext).Update(hisStepCycle);
         }
      }
      /// <summary>
      /// 删除存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepCycleRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public HisStepCycle GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepCycleRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepCycleRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
