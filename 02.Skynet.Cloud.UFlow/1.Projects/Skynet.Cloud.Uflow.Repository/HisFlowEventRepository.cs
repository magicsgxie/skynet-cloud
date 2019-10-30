/************************************************************************************
* Copyright (c) 2019-07-11 12:08:10 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：798ebb4a-8497-46a9-a487-b34941150216
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:10 
* 描述：实例事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:10 
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
   /// 实例事件表仓储类
   /// </summary>
   public class HisFlowEventRepository:ObjectRepository
   {
      public HisFlowEventRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public long Add(HisFlowEvent  hisFlowEvent)
      {
         return Add<HisFlowEvent>(hisFlowEvent);
      }
      /// <summary>
      /// 批量添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisFlowEvent>  hisFlowEvents)
      {
         Batch<long, HisFlowEvent>(hisFlowEvents, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Update(HisFlowEvent  hisFlowEvent)
      {
         return Update<HisFlowEvent>(hisFlowEvent);
      }
      /// <summary>
      /// 删除实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisFlowEvent>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public HisFlowEvent GetById(string id)
      {
         return GetByID<HisFlowEvent>(id);
      }
      /// <summary>
      /// 获取所有的实例事件表{实例事件表}对象
      /// </summary>
      public IQueryable<HisFlowEvent> Query()
      {
         return CreateQuery<HisFlowEvent>();
      }
   }
}
