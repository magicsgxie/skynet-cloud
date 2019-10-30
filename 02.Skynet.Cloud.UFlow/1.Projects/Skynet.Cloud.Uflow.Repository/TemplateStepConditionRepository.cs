/************************************************************************************
* Copyright (c) 2019-07-11 12:07:43 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：cd69842d-697e-4d86-bd9f-a60f356a933c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:43 
* 描述：步骤转入转出条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:43 
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
   /// 步骤转入转出条件仓储类
   /// </summary>
   public class TemplateStepConditionRepository:ObjectRepository
   {
      public TemplateStepConditionRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepCondition  templateStepCondition)
      {
         return Add<TemplateStepCondition>(templateStepCondition);
      }
      /// <summary>
      /// 批量添加步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepCondition>  templateStepConditions)
      {
         Batch<long, TemplateStepCondition>(templateStepConditions, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepCondition  templateStepCondition)
      {
         return Update<TemplateStepCondition>(templateStepCondition);
      }
      /// <summary>
      /// 删除步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepCondition>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public TemplateStepCondition GetById(string id)
      {
         return GetByID<TemplateStepCondition>(id);
      }
      /// <summary>
      /// 获取所有的步骤转入转出条件{步骤转入转出条件}对象
      /// </summary>
      public IQueryable<TemplateStepCondition> Query()
      {
         return CreateQuery<TemplateStepCondition>();
      }
   }
}
