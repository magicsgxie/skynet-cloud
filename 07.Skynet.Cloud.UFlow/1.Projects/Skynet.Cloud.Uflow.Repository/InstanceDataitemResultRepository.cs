/************************************************************************************
* Copyright (c) 2019-07-11 12:08:22 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceDataitemResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：7a8cd3b1-5f94-4423-9a74-d4c9ae2d5c48
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:22 
* 描述：数据项结果表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:22 
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
   /// 数据项结果表仓储类
   /// </summary>
   public class InstanceDataitemResultRepository:ObjectRepository
   {
      public InstanceDataitemResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceDataitemResult  instanceDataitemResult)
      {
         return Add<InstanceDataitemResult>(instanceDataitemResult);
      }
      /// <summary>
      /// 批量添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceDataitemResult>  instanceDataitemResults)
      {
         Batch<long, InstanceDataitemResult>(instanceDataitemResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceDataitemResult  instanceDataitemResult)
      {
         return Update<InstanceDataitemResult>(instanceDataitemResult);
      }
      /// <summary>
      /// 删除数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceDataitemResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public InstanceDataitemResult GetById(string id)
      {
         return GetByID<InstanceDataitemResult>(id);
      }
      /// <summary>
      /// 获取所有的数据项结果表{数据项结果表}对象
      /// </summary>
      public IQueryable<InstanceDataitemResult> Query()
      {
         return CreateQuery<InstanceDataitemResult>();
      }
   }
}
