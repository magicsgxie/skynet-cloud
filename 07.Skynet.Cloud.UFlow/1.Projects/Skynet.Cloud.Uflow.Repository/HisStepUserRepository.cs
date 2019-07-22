/************************************************************************************
* Copyright (c) 2019-07-11 12:07:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：89fd8df7-2249-4e71-83a3-d18cd58db318
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:12 
* 描述：实例步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:12 
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
   public class HisStepUserRepository:ObjectRepository
   {
      public HisStepUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public long Add(HisStepUser  hisStepUser)
      {
         return Add<HisStepUser>(hisStepUser);
      }
      /// <summary>
      /// 批量添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepUser>  hisStepUsers)
      {
         Batch<long, HisStepUser>(hisStepUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public int Update(HisStepUser  hisStepUser)
      {
         return Update<HisStepUser>(hisStepUser);
      }
      /// <summary>
      /// 删除实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public HisStepUser GetById(string id)
      {
         return GetByID<HisStepUser>(id);
      }
      /// <summary>
      /// 获取所有的实例步骤处理人{实例步骤处理人}对象
      /// </summary>
      public IQueryable<HisStepUser> Query()
      {
         return CreateQuery<HisStepUser>();
      }
   }
}
