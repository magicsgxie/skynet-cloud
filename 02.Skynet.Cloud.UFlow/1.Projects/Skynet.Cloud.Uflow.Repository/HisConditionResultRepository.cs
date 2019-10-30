/************************************************************************************
* Copyright (c) 2019-07-11 12:08:07 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisConditionResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：b6eefd3f-d3dc-4865-8897-e5723ba2dc58
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:07 
* 描述：条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:07 
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
   public class HisConditionResultRepository:ObjectRepository
   {
      public HisConditionResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisConditionResult  hisConditionResult)
      {
         return Add<HisConditionResult>(hisConditionResult);
      }
      /// <summary>
      /// 批量添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisConditionResult>  hisConditionResults)
      {
         Batch<long, HisConditionResult>(hisConditionResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisConditionResult  hisConditionResult)
      {
         return Update<HisConditionResult>(hisConditionResult);
      }
      /// <summary>
      /// 删除条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisConditionResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public HisConditionResult GetById(string id)
      {
         return GetByID<HisConditionResult>(id);
      }
      /// <summary>
      /// 获取所有的条件结果{条件结果}对象
      /// </summary>
      public IQueryable<HisConditionResult> Query()
      {
         return CreateQuery<HisConditionResult>();
      }
   }
}
