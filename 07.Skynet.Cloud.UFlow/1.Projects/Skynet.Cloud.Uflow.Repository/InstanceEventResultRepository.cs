/************************************************************************************
* Copyright (c) 2019-07-11 12:08:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceEventResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：a5c975bb-33f9-488c-a9a8-e2d364070c8d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:23 
* 描述：实例事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:23 
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
   public class InstanceEventResultRepository:ObjectRepository
   {
      public InstanceEventResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceEventResult  instanceEventResult)
      {
         return Add<InstanceEventResult>(instanceEventResult);
      }
      /// <summary>
      /// 批量添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceEventResult>  instanceEventResults)
      {
         Batch<long, InstanceEventResult>(instanceEventResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceEventResult  instanceEventResult)
      {
         return Update<InstanceEventResult>(instanceEventResult);
      }
      /// <summary>
      /// 删除实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceEventResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public InstanceEventResult GetById(string id)
      {
         return GetByID<InstanceEventResult>(id);
      }
      /// <summary>
      /// 获取所有的实例事件结果{实例事件结果}对象
      /// </summary>
      public IQueryable<InstanceEventResult> Query()
      {
         return CreateQuery<InstanceEventResult>();
      }
   }
}
