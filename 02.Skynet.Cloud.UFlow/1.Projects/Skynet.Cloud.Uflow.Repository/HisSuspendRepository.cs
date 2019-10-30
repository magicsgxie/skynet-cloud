/************************************************************************************
* Copyright (c) 2019-07-11 12:08:18 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisSuspend.cs
* 版本号：  V1.0.0.0
* 唯一标识：db3db85b-d4e3-4d20-b25d-2be6d869e4ff
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:18 
* 描述：实例挂起记录 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:18 
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
   public class HisSuspendRepository:ObjectRepository
   {
      public HisSuspendRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public long Add(HisSuspend  hisSuspend)
      {
         return Add<HisSuspend>(hisSuspend);
      }
      /// <summary>
      /// 批量添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisSuspend>  hisSuspends)
      {
         Batch<long, HisSuspend>(hisSuspends, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public int Update(HisSuspend  hisSuspend)
      {
         return Update<HisSuspend>(hisSuspend);
      }
      /// <summary>
      /// 删除实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisSuspend>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public HisSuspend GetById(string id)
      {
         return GetByID<HisSuspend>(id);
      }
      /// <summary>
      /// 获取所有的实例挂起记录{实例挂起记录}对象
      /// </summary>
      public IQueryable<HisSuspend> Query()
      {
         return CreateQuery<HisSuspend>();
      }
   }
}
