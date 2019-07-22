/************************************************************************************
* Copyright (c) 2019-07-11 12:07:14 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：bed9ac9f-543a-4e8d-939a-e8bf85b6dc27
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:14 
* 描述：实例步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:14 
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
   /// 实例步骤处理人仓储类
   /// </summary>
   public class InstanceStepUserRepository:ObjectRepository
   {
      public InstanceStepUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepUser  instanceStepUser)
      {
         return Add<InstanceStepUser>(instanceStepUser);
      }
      /// <summary>
      /// 批量添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepUser>  instanceStepUsers)
      {
         Batch<long, InstanceStepUser>(instanceStepUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepUser  instanceStepUser)
      {
         return Update<InstanceStepUser>(instanceStepUser);
      }
      /// <summary>
      /// 删除实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStepUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public InstanceStepUser GetById(string id)
      {
         return GetByID<InstanceStepUser>(id);
      }
      /// <summary>
      /// 获取所有的实例步骤处理人{实例步骤处理人}对象
      /// </summary>
      public IQueryable<InstanceStepUser> Query()
      {
         return CreateQuery<InstanceStepUser>();
      }
   }
}
