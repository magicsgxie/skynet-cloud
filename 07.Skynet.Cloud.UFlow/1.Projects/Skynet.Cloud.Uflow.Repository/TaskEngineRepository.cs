/************************************************************************************
* Copyright (c) 2019-07-11 12:07:55 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TaskEngine.cs
* 版本号：  V1.0.0.0
* 唯一标识：2b4a1252-d5e6-4ae2-b5e9-ea8c8f07a67e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:55 
* 描述：正在运行的引擎列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:55 
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
   /// 正在运行的引擎列表仓储类
   /// </summary>
   public class TaskEngineRepository:ObjectRepository
   {
      public TaskEngineRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public long Add(TaskEngine  taskEngine)
      {
         return Add<TaskEngine>(taskEngine);
      }
      /// <summary>
      /// 批量添加正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TaskEngine>  taskEngines)
      {
         Batch<long, TaskEngine>(taskEngines, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public int Update(TaskEngine  taskEngine)
      {
         return Update<TaskEngine>(taskEngine);
      }
      /// <summary>
      /// 删除正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TaskEngine>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的正在运行的引擎列表{正在运行的引擎列表}对象(即:一条记录
      /// </summary>
      public TaskEngine GetById(string id)
      {
         return GetByID<TaskEngine>(id);
      }
      /// <summary>
      /// 获取所有的正在运行的引擎列表{正在运行的引擎列表}对象
      /// </summary>
      public IQueryable<TaskEngine> Query()
      {
         return CreateQuery<TaskEngine>();
      }
   }
}
