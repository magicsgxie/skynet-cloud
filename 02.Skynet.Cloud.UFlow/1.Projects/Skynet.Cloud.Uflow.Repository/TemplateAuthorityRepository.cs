/************************************************************************************
* Copyright (c) 2019-07-11 12:07:57 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：090f2b8e-1eb3-4129-830c-e43ea6a6518d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:57 
* 描述：流程权限表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:57 
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
   /// 流程权限表仓储类
   /// </summary>
   public class TemplateAuthorityRepository:ObjectRepository
   {
      public TemplateAuthorityRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateAuthority  templateAuthority)
      {
         return Add<TemplateAuthority>(templateAuthority);
      }
      /// <summary>
      /// 批量添加流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateAuthority>  templateAuthoritys)
      {
         Batch<long, TemplateAuthority>(templateAuthoritys, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateAuthority  templateAuthority)
      {
         return Update<TemplateAuthority>(templateAuthority);
      }
      /// <summary>
      /// 删除流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateAuthority>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public TemplateAuthority GetById(string id)
      {
         return GetByID<TemplateAuthority>(id);
      }
      /// <summary>
      /// 获取所有的流程权限表{流程权限表}对象
      /// </summary>
      public IQueryable<TemplateAuthority> Query()
      {
         return CreateQuery<TemplateAuthority>();
      }
   }
}
