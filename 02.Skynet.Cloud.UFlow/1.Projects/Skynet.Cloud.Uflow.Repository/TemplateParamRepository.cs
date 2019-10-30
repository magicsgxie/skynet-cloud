/************************************************************************************
* Copyright (c) 2019-07-11 12:07:41 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：ca23d773-3487-4490-847b-b0f3e4e17a9b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:41 
* 描述：参数 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:41 
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
   /// 参数仓储类
   /// </summary>
   public class TemplateParamRepository:ObjectRepository
   {
      public TemplateParamRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加参数{参数}对象(即:一条记录
      /// </summary>
      public long Add(TemplateParam  templateParam)
      {
         return Add<TemplateParam>(templateParam);
      }
      /// <summary>
      /// 批量添加参数{参数}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateParam>  templateParams)
      {
         Batch<long, TemplateParam>(templateParams, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新参数{参数}对象(即:一条记录
      /// </summary>
      public int Update(TemplateParam  templateParam)
      {
         return Update<TemplateParam>(templateParam);
      }
      /// <summary>
      /// 删除参数{参数}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateParam>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的参数{参数}对象(即:一条记录
      /// </summary>
      public TemplateParam GetById(string id)
      {
         return GetByID<TemplateParam>(id);
      }
      /// <summary>
      /// 获取所有的参数{参数}对象
      /// </summary>
      public IQueryable<TemplateParam> Query()
      {
         return CreateQuery<TemplateParam>();
      }
   }
}
