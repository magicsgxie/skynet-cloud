/************************************************************************************
* Copyright (c) 2019-07-11 12:08:02 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceSuspend.cs
* 版本号：  V1.0.0.0
* 唯一标识：cfae0ae8-29b6-42d8-b61a-bfa5284a433b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:02 
* 描述：实例挂起记录 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:02 
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
   /// 实例挂起记录仓储类
   /// </summary>
   public class InstanceSuspendRepository:ObjectRepository
   {
      public InstanceSuspendRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public long Add(InstanceSuspend  instanceSuspend)
      {
         return Add<InstanceSuspend>(instanceSuspend);
      }
      /// <summary>
      /// 批量添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceSuspend>  instanceSuspends)
      {
         Batch<long, InstanceSuspend>(instanceSuspends, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public int Update(InstanceSuspend  instanceSuspend)
      {
         return Update<InstanceSuspend>(instanceSuspend);
      }
      /// <summary>
      /// 删除实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceSuspend>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public InstanceSuspend GetById(string id)
      {
         return GetByID<InstanceSuspend>(id);
      }
      /// <summary>
      /// 获取所有的实例挂起记录{实例挂起记录}对象
      /// </summary>
      public IQueryable<InstanceSuspend> Query()
      {
         return CreateQuery<InstanceSuspend>();
      }
   }
}
