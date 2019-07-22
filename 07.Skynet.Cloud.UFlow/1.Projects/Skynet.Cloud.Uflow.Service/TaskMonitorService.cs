/************************************************************************************
* Copyright (c) 2019-07-11 12:02:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TaskMonitor.cs
* 版本号：  V1.0.0.0
* 唯一标识：5aa520fe-3aa6-4332-95db-fe3568849be3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:42 
* 描述：监控任务列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:42 
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
   /// 监控任务列表服务实现类
   /// </summary>
   public class TaskMonitorService: ITaskMonitorService
   {
      /// <summary>
      /// 添加监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public long Add(TaskMonitor  taskMonitor)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskMonitorRepository(dbContext).Add(taskMonitor);
         }
      }
      /// <summary>
      /// 添加监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TaskMonitor>  taskMonitors)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TaskMonitorRepository(dbContext).Add(taskMonitors);
         }
      }
      /// <summary>
      /// 更新监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public int Update(TaskMonitor  taskMonitor)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskMonitorRepository(dbContext).Update(taskMonitor);
         }
      }
      /// <summary>
      /// 删除监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskMonitorRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public TaskMonitor GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskMonitorRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskMonitorRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
