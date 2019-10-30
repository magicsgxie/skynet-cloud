/************************************************************************************
* Copyright (c) 2019-07-11 12:08:25 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：087fb713-2796-48fd-b8a0-63eaf49462a9
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:25 
* 描述：实例事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:25 
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
   public class InstanceFlowEventRepository:ObjectRepository
   {
      public InstanceFlowEventRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceFlowEvent  instanceFlowEvent)
      {
         return Add<InstanceFlowEvent>(instanceFlowEvent);
      }
      /// <summary>
      /// 批量添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceFlowEvent>  instanceFlowEvents)
      {
         Batch<long, InstanceFlowEvent>(instanceFlowEvents, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceFlowEvent  instanceFlowEvent)
      {
         return Update<InstanceFlowEvent>(instanceFlowEvent);
      }
      /// <summary>
      /// 删除实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceFlowEvent>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public InstanceFlowEvent GetById(string id)
      {
         return GetByID<InstanceFlowEvent>(id);
      }
      /// <summary>
      /// 获取所有的实例事件表{实例事件表}对象
      /// </summary>
      public IQueryable<InstanceFlowEvent> Query()
      {
         return CreateQuery<InstanceFlowEvent>();
      }
   }
}
