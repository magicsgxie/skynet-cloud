/************************************************************************************
* Copyright (c) 2019-07-11 12:07:47 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepNextstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：dc677f3a-5af4-4aa5-b7ab-5f59c6b8c71f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:47 
* 描述：下一步可选人员列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:47 
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
   /// 下一步可选人员列表仓储类
   /// </summary>
   public class TemplateStepNextstepUserRepository:ObjectRepository
   {
      public TemplateStepNextstepUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepNextstepUser  templateStepNextstepUser)
      {
         return Add<TemplateStepNextstepUser>(templateStepNextstepUser);
      }
      /// <summary>
      /// 批量添加下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepNextstepUser>  templateStepNextstepUsers)
      {
         Batch<long, TemplateStepNextstepUser>(templateStepNextstepUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepNextstepUser  templateStepNextstepUser)
      {
         return Update<TemplateStepNextstepUser>(templateStepNextstepUser);
      }
      /// <summary>
      /// 删除下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepNextstepUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public TemplateStepNextstepUser GetById(string id)
      {
         return GetByID<TemplateStepNextstepUser>(id);
      }
      /// <summary>
      /// 获取所有的下一步可选人员列表{下一步可选人员列表}对象
      /// </summary>
      public IQueryable<TemplateStepNextstepUser> Query()
      {
         return CreateQuery<TemplateStepNextstepUser>();
      }
   }
}
