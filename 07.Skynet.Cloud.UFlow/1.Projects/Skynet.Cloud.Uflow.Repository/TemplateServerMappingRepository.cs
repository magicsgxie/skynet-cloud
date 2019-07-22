/************************************************************************************
* Copyright (c) 2019-07-11 12:07:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateServerMapping.cs
* 版本号：  V1.0.0.0
* 唯一标识：cc0d2e82-1361-4888-aaf0-488b8db2cf8e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:42 
* 描述：模版与引擎服务器映射关系表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:42 
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
   /// 模版与引擎服务器映射关系表仓储类
   /// </summary>
   public class TemplateServerMappingRepository:ObjectRepository
   {
      public TemplateServerMappingRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateServerMapping  templateServerMapping)
      {
         return Add<TemplateServerMapping>(templateServerMapping);
      }
      /// <summary>
      /// 批量添加模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateServerMapping>  templateServerMappings)
      {
         Batch<long, TemplateServerMapping>(templateServerMappings, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateServerMapping  templateServerMapping)
      {
         return Update<TemplateServerMapping>(templateServerMapping);
      }
      /// <summary>
      /// 删除模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public int Delete(int[] idArrays )
      {
         return Delete<TemplateServerMapping>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public TemplateServerMapping GetById(int id)
      {
         return GetByID<TemplateServerMapping>(id);
      }
      /// <summary>
      /// 获取所有的模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象
      /// </summary>
      public IQueryable<TemplateServerMapping> Query()
      {
         return CreateQuery<TemplateServerMapping>();
      }
   }
}
