/************************************************************************************
* Copyright (c) 2019-07-11 12:07:53 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateType.cs
* 版本号：  V1.0.0.0
* 唯一标识：7cc4facb-7ab7-4f9c-90d9-687d51eab0d1
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:53 
* 描述：流程分类 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:53 
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
   /// 流程分类仓储类
   /// </summary>
   public class TemplateTypeRepository:ObjectRepository
   {
      public TemplateTypeRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public long Add(TemplateType  templatetype)
      {
         return Add<TemplateType>(templatetype);
      }
      /// <summary>
      /// 批量添加流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateType>  templatetypes)
      {
         Batch<long, TemplateType>(templatetypes, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public int Update(TemplateType  templatetype)
      {
         return Update<TemplateType>(templatetype);
      }
      /// <summary>
      /// 删除流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateType>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public TemplateType GetById(string id)
      {
         return GetByID<TemplateType>(id);
      }
      /// <summary>
      /// 获取所有的流程分类{流程分类}对象
      /// </summary>
      public IQueryable<TemplateType> Query()
      {
         return CreateQuery<TemplateType>();
      }
   }
}
