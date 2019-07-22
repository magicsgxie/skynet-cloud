/************************************************************************************
* Copyright (c) 2019-07-11 12:07:56 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TaskFirst.cs
* 版本号：  V1.0.0.0
* 唯一标识：1d4b0f9c-19d8-436b-9c38-afd88dd986b5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:56 
* 描述：引擎优先处理任务列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:56 
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
   /// 引擎优先处理任务列表仓储类
   /// </summary>
   public class TaskFirstRepository:ObjectRepository
   {
      public TaskFirstRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public long Add(TaskFirst  taskFirst)
      {
         return Add<TaskFirst>(taskFirst);
      }
      /// <summary>
      /// 批量添加引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TaskFirst>  taskFirsts)
      {
         Batch<long, TaskFirst>(taskFirsts, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public int Update(TaskFirst  taskFirst)
      {
         return Update<TaskFirst>(taskFirst);
      }
      /// <summary>
      /// 删除引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TaskFirst>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的引擎优先处理任务列表{引擎优先处理任务列表}对象(即:一条记录
      /// </summary>
      public TaskFirst GetById(string id)
      {
         return GetByID<TaskFirst>(id);
      }
      /// <summary>
      /// 获取所有的引擎优先处理任务列表{引擎优先处理任务列表}对象
      /// </summary>
      public IQueryable<TaskFirst> Query()
      {
         return CreateQuery<TaskFirst>();
      }
   }
}
