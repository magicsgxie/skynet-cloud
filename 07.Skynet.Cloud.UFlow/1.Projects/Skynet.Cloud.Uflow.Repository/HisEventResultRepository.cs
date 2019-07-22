/************************************************************************************
* Copyright (c) 2019-07-11 12:08:09 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisEventResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：4f9dd1f0-bd37-428b-9a99-19d7d1040dea
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:09 
* 描述：实例事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:09 
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
   /// 实例事件结果仓储类
   /// </summary>
   public class HisEventResultRepository:ObjectRepository
   {
      public HisEventResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisEventResult  hisEventResult)
      {
         return Add<HisEventResult>(hisEventResult);
      }
      /// <summary>
      /// 批量添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisEventResult>  hisEventResults)
      {
         Batch<long, HisEventResult>(hisEventResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisEventResult  hisEventResult)
      {
         return Update<HisEventResult>(hisEventResult);
      }
      /// <summary>
      /// 删除实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisEventResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public HisEventResult GetById(string id)
      {
         return GetByID<HisEventResult>(id);
      }
      /// <summary>
      /// 获取所有的实例事件结果{实例事件结果}对象
      /// </summary>
      public IQueryable<HisEventResult> Query()
      {
         return CreateQuery<HisEventResult>();
      }
   }
}
