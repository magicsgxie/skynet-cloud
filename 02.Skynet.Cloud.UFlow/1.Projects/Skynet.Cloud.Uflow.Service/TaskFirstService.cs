/************************************************************************************
* Copyright (c) 2019-07-11 12:02:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TaskFirst.cs
* 版本号：  V1.0.0.0
* 唯一标识：32868b29-8f10-41ff-b12b-14adc98c235d
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
   /// 引擎优先处理任务列表服务实现类
   /// </summary>
   public class TaskFirstService: ITaskFirstService
   {
      /// <summary>
      /// 添加引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public long Add(TaskFirst  taskFirst)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskFirstRepository(dbContext).Add(taskFirst);
         }
      }
      /// <summary>
      /// 添加引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TaskFirst>  taskFirsts)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TaskFirstRepository(dbContext).Add(taskFirsts);
         }
      }
      /// <summary>
      /// 更新引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public int Update(TaskFirst  taskFirst)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskFirstRepository(dbContext).Update(taskFirst);
         }
      }
      /// <summary>
      /// 删除引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskFirstRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public TaskFirst GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskFirstRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskFirstRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
