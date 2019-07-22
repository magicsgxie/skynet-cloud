/************************************************************************************
* Copyright (c) 2019-07-11 12:07:54 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：b3b9e8a6-bd92-4cc2-9881-1b7ce645219a
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:54 
* 描述：人员 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:54 
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
   /// 人员仓储类
   /// </summary>
   public class TemplateUserRepository:ObjectRepository
   {
      public TemplateUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加人员{人员}对象(即:一条记录
      /// </summary>
      public long Add(TemplateUser  templateUser)
      {
         return Add<TemplateUser>(templateUser);
      }
      /// <summary>
      /// 批量添加人员{人员}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateUser>  templateUsers)
      {
         Batch<long, TemplateUser>(templateUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新人员{人员}对象(即:一条记录
      /// </summary>
      public int Update(TemplateUser  templateUser)
      {
         return Update<TemplateUser>(templateUser);
      }
      /// <summary>
      /// 删除人员{人员}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的人员{人员}对象(即:一条记录
      /// </summary>
      public TemplateUser GetById(string id)
      {
         return GetByID<TemplateUser>(id);
      }
      /// <summary>
      /// 获取所有的人员{人员}对象
      /// </summary>
      public IQueryable<TemplateUser> Query()
      {
         return CreateQuery<TemplateUser>();
      }
   }
}
