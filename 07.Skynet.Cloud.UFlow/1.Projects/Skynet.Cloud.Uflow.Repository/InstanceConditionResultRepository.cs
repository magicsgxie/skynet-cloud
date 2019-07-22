/************************************************************************************
* Copyright (c) 2019-07-11 12:08:21 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceConditionResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：032ef578-71fc-46be-b74a-4d93438ab1f3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:21 
* 描述：条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:21 
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
   /// 条件结果仓储类
   /// </summary>
   public class InstanceConditionResultRepository:ObjectRepository
   {
      public InstanceConditionResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceConditionResult  instanceConditionResult)
      {
         return Add<InstanceConditionResult>(instanceConditionResult);
      }
      /// <summary>
      /// 批量添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceConditionResult>  instanceConditionResults)
      {
         Batch<long, InstanceConditionResult>(instanceConditionResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceConditionResult  instanceConditionResult)
      {
         return Update<InstanceConditionResult>(instanceConditionResult);
      }
      /// <summary>
      /// 删除条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceConditionResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public InstanceConditionResult GetById(string id)
      {
         return GetByID<InstanceConditionResult>(id);
      }
      /// <summary>
      /// 获取所有的条件结果{条件结果}对象
      /// </summary>
      public IQueryable<InstanceConditionResult> Query()
      {
         return CreateQuery<InstanceConditionResult>();
      }
   }
}
