/************************************************************************************
* Copyright (c) 2019-07-11 12:02:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TaskFirst.cs
* 版本号：  V1.0.0.0
* 唯一标识：5295a160-d5df-49b6-9c81-94f0bd16fdf8
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:42 
* 描述：引擎优先处理任务列表 
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
   /// 引擎优先处理任务列表服务接口类
   /// </summary>
   public interface ITaskFirstService
   {
      /// <summary>
      /// 添加引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      long Add(TaskFirst  taskFirst);
      /// <summary>
      /// 添加引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      void Add(IList<TaskFirst>  taskFirsts);
      /// <summary>
      /// 更新引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      int Update(TaskFirst  taskFirst);
      /// <summary>
      /// 删除引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      TaskFirst GetById(string id);
      /// <summary>
      /// 获取所有的引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
