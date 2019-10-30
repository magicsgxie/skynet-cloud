/************************************************************************************
* Copyright (c) 2019-07-11 12:07:48 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：46c1eabb-d3e2-471b-8635-135e0d1aebb3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:48 
* 描述：子流程 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:48 
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
   /// 子流程仓储类
   /// </summary>
   public class TemplateStepSubflowRepository:ObjectRepository
   {
      public TemplateStepSubflowRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加子流程{子流程}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepSubflow  templateStepSubflow)
      {
         return Add<TemplateStepSubflow>(templateStepSubflow);
      }
      /// <summary>
      /// 批量添加子流程{子流程}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepSubflow>  templateStepSubflows)
      {
         Batch<long, TemplateStepSubflow>(templateStepSubflows, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新子流程{子流程}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepSubflow  templateStepSubflow)
      {
         return Update<TemplateStepSubflow>(templateStepSubflow);
      }
      /// <summary>
      /// 删除子流程{子流程}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepSubflow>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的子流程{子流程}对象(即:一条记录
      /// </summary>
      public TemplateStepSubflow GetById(string id)
      {
         return GetByID<TemplateStepSubflow>(id);
      }
      /// <summary>
      /// 获取所有的子流程{子流程}对象
      /// </summary>
      public IQueryable<TemplateStepSubflow> Query()
      {
         return CreateQuery<TemplateStepSubflow>();
      }
   }
}
