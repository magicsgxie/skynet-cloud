/************************************************************************************
* Copyright (c) 2019-07-11 12:07:46 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepFixstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：edd098d8-1f7a-4218-ac8a-65e6a75fbcdd
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:46 
* 描述：指定步骤可选人员列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:46 
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
   /// 指定步骤可选人员列表仓储类
   /// </summary>
   public class TemplateStepFixstepUserRepository:ObjectRepository
   {
      public TemplateStepFixstepUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepFixstepUser  templateStepFixstepUser)
      {
         return Add<TemplateStepFixstepUser>(templateStepFixstepUser);
      }
      /// <summary>
      /// 批量添加指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepFixstepUser>  templateStepFixstepUsers)
      {
         Batch<long, TemplateStepFixstepUser>(templateStepFixstepUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepFixstepUser  templateStepFixstepUser)
      {
         return Update<TemplateStepFixstepUser>(templateStepFixstepUser);
      }
      /// <summary>
      /// 删除指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepFixstepUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public TemplateStepFixstepUser GetById(string id)
      {
         return GetByID<TemplateStepFixstepUser>(id);
      }
      /// <summary>
      /// 获取所有的指定步骤可选人员列表{指定步骤可选人员列表}对象
      /// </summary>
      public IQueryable<TemplateStepFixstepUser> Query()
      {
         return CreateQuery<TemplateStepFixstepUser>();
      }
   }
}
