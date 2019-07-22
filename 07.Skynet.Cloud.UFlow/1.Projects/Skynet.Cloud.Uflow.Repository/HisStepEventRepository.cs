/************************************************************************************
* Copyright (c) 2019-07-11 12:08:13 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：d011da84-a9a3-44cf-ab90-fc71964519b0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:13 
* 描述：步骤前后期事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:13 
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
   /// 步骤前后期事件结果仓储类
   /// </summary>
   public class HisStepEventRepository:ObjectRepository
   {
      public HisStepEventRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisStepEvent  hisStepEvent)
      {
         return Add<HisStepEvent>(hisStepEvent);
      }
      /// <summary>
      /// 批量添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepEvent>  hisStepEvents)
      {
         Batch<long, HisStepEvent>(hisStepEvents, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisStepEvent  hisStepEvent)
      {
         return Update<HisStepEvent>(hisStepEvent);
      }
      /// <summary>
      /// 删除步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepEvent>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      public HisStepEvent GetById(string id)
      {
         return GetByID<HisStepEvent>(id);
      }
      /// <summary>
      /// 获取所有的步骤前后期事件结果{步骤前后期事件结果}对象
      /// </summary>
      public IQueryable<HisStepEvent> Query()
      {
         return CreateQuery<HisStepEvent>();
      }
   }
}
