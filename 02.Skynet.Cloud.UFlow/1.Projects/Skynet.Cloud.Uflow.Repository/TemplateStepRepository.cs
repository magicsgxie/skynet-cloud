/************************************************************************************
* Copyright (c) 2019-07-11 12:07:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStep.cs
* 版本号：  V1.0.0.0
* 唯一标识：f114d6e4-71ca-4eb9-aa20-9ce88f79c047
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:42 
* 描述：流程步骤表 
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
   /// 流程步骤表仓储类
   /// </summary>
   public class TemplateStepRepository:ObjectRepository
   {
      public TemplateStepRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStep  templateStep)
      {
         return Add<TemplateStep>(templateStep);
      }
      /// <summary>
      /// 批量添加流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStep>  templateSteps)
      {
         Batch<long, TemplateStep>(templateSteps, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStep  templateStep)
      {
         return Update<TemplateStep>(templateStep);
      }
      /// <summary>
      /// 删除流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStep>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public TemplateStep GetById(string id)
      {
         return GetByID<TemplateStep>(id);
      }
      /// <summary>
      /// 获取所有的流程步骤表{流程步骤表}对象
      /// </summary>
      public IQueryable<TemplateStep> Query()
      {
         return CreateQuery<TemplateStep>();
      }
   }
}
