/************************************************************************************
* Copyright (c) 2019-07-11 12:07:15 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceDraft.cs
* 版本号：  V1.0.0.0
* 唯一标识：46e41e49-da65-41a6-b070-12f442395877
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:15 
* 描述：发起草稿记录表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:15 
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
   /// 发起草稿记录表仓储类
   /// </summary>
   public class InstanceDraftRepository:ObjectRepository
   {
      public InstanceDraftRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceDraft  instanceDraft)
      {
         return Add<InstanceDraft>(instanceDraft);
      }
      /// <summary>
      /// 批量添加发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceDraft>  instanceDrafts)
      {
         Batch<long, InstanceDraft>(instanceDrafts, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceDraft  instanceDraft)
      {
         return Update<InstanceDraft>(instanceDraft);
      }
      /// <summary>
      /// 删除发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceDraft>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public InstanceDraft GetById(string id)
      {
         return GetByID<InstanceDraft>(id);
      }
      /// <summary>
      /// 获取所有的发起草稿记录表{发起草稿记录表}对象
      /// </summary>
      public IQueryable<InstanceDraft> Query()
      {
         return CreateQuery<InstanceDraft>();
      }
   }
}
