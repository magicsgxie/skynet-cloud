/************************************************************************************
* Copyright (c) 2019-07-11 12:07:57 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TaskMonitor.cs
* 版本号：  V1.0.0.0
* 唯一标识：344469e0-c832-4550-8193-346fa65a359e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:57 
* 描述：监控任务列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:57 
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
   /// 监控任务列表仓储类
   /// </summary>
   public class TaskMonitorRepository:ObjectRepository
   {
      public TaskMonitorRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public long Add(TaskMonitor  taskMonitor)
      {
         return Add<TaskMonitor>(taskMonitor);
      }
      /// <summary>
      /// 批量添加监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TaskMonitor>  taskMonitors)
      {
         Batch<long, TaskMonitor>(taskMonitors, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public int Update(TaskMonitor  taskMonitor)
      {
         return Update<TaskMonitor>(taskMonitor);
      }
      /// <summary>
      /// 删除监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TaskMonitor>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public TaskMonitor GetById(string id)
      {
         return GetByID<TaskMonitor>(id);
      }
      /// <summary>
      /// 获取所有的监控任务列表{监控任务列表}对象
      /// </summary>
      public IQueryable<TaskMonitor> Query()
      {
         return CreateQuery<TaskMonitor>();
      }
   }
}
