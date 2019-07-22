/************************************************************************************
* Copyright (c) 2019-07-11 12:02:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TaskMonitor.cs
* 版本号：  V1.0.0.0
* 唯一标识：24f39a0b-d049-4d72-9ee3-94c216afd70c
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



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 监控任务列表服务接口类
   /// </summary>
   public interface ITaskMonitorService
   {
      /// <summary>
      /// 添加监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      long Add(TaskMonitor  taskMonitor);
      /// <summary>
      /// 添加监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      void Add(IList<TaskMonitor>  taskMonitors);
      /// <summary>
      /// 更新监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      int Update(TaskMonitor  taskMonitor);
      /// <summary>
      /// 删除监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      TaskMonitor GetById(string id);
      /// <summary>
      /// 获取所有的监控任务列表{监控任务列表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
