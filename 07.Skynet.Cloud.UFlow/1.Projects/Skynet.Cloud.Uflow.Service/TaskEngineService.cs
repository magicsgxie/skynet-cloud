/************************************************************************************
* Copyright (c) 2019-07-11 12:02:41 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TaskEngine.cs
* 版本号：  V1.0.0.0
* 唯一标识：e3692154-df67-408e-86f9-8d6e7b3aad80
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:41 
* 描述：正在运行的引擎列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:41 
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
   /// 正在运行的引擎列表服务实现类
   /// </summary>
   public class TaskEngineService: ITaskEngineService
   {
      /// <summary>
      /// 添加正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public long Add(TaskEngine  taskEngine)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskEngineRepository(dbContext).Add(taskEngine);
         }
      }
      /// <summary>
      /// 添加正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TaskEngine>  taskEngines)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TaskEngineRepository(dbContext).Add(taskEngines);
         }
      }
      /// <summary>
      /// 更新正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public int Update(TaskEngine  taskEngine)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskEngineRepository(dbContext).Update(taskEngine);
         }
      }
      /// <summary>
      /// 删除正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskEngineRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public TaskEngine GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskEngineRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TaskEngineRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
