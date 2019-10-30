/************************************************************************************
* Copyright (c) 2019-07-11 12:07:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：f0748313-7d0b-43db-8036-c272dde5a5b4
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:49 
* 描述：步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:49 
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
   /// 步骤处理人仓储类
   /// </summary>
   public class TemplateStepUserRepository:ObjectRepository
   {
      public TemplateStepUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepUser  templateStepUser)
      {
         return Add<TemplateStepUser>(templateStepUser);
      }
      /// <summary>
      /// 批量添加步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepUser>  templateStepUsers)
      {
         Batch<long, TemplateStepUser>(templateStepUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepUser  templateStepUser)
      {
         return Update<TemplateStepUser>(templateStepUser);
      }
      /// <summary>
      /// 删除步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public TemplateStepUser GetById(string id)
      {
         return GetByID<TemplateStepUser>(id);
      }
      /// <summary>
      /// 获取所有的步骤处理人{步骤处理人}对象
      /// </summary>
      public IQueryable<TemplateStepUser> Query()
      {
         return CreateQuery<TemplateStepUser>();
      }
   }
}
