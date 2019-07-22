/************************************************************************************
* Copyright (c) 2019-07-11 12:07:58 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：679bfd16-0028-483f-b6e9-6a9e32974c36
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:58 
* 描述：条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:58 
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
   /// 条件仓储类
   /// </summary>
   public class TemplateConditionRepository:ObjectRepository
   {
      public TemplateConditionRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加条件{条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateCondition  templateCondition)
      {
         return Add<TemplateCondition>(templateCondition);
      }
      /// <summary>
      /// 批量添加条件{条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateCondition>  templateConditions)
      {
         Batch<long, TemplateCondition>(templateConditions, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新条件{条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateCondition  templateCondition)
      {
         return Update<TemplateCondition>(templateCondition);
      }
      /// <summary>
      /// 删除条件{条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateCondition>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的条件{条件}对象(即:一条记录
      /// </summary>
      public TemplateCondition GetById(string id)
      {
         return GetByID<TemplateCondition>(id);
      }
      /// <summary>
      /// 获取所有的条件{条件}对象
      /// </summary>
      public IQueryable<TemplateCondition> Query()
      {
         return CreateQuery<TemplateCondition>();
      }
   }
}
