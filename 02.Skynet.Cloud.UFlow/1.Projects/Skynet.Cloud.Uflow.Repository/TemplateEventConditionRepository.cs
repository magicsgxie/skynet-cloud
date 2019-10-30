/************************************************************************************
* Copyright (c) 2019-07-11 12:07:37 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateEventCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：962ea252-23af-4a91-a7c2-1b9d5d113659
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:37 
* 描述：事件启用条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:37 
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
   /// 事件启用条件仓储类
   /// </summary>
   public class TemplateEventConditionRepository:ObjectRepository
   {
      public TemplateEventConditionRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateEventCondition  templateEventCondition)
      {
         return Add<TemplateEventCondition>(templateEventCondition);
      }
      /// <summary>
      /// 批量添加事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateEventCondition>  templateEventConditions)
      {
         Batch<long, TemplateEventCondition>(templateEventConditions, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateEventCondition  templateEventCondition)
      {
         return Update<TemplateEventCondition>(templateEventCondition);
      }
      /// <summary>
      /// 删除事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateEventCondition>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public TemplateEventCondition GetById(string id)
      {
         return GetByID<TemplateEventCondition>(id);
      }
      /// <summary>
      /// 获取所有的事件启用条件{事件启用条件}对象
      /// </summary>
      public IQueryable<TemplateEventCondition> Query()
      {
         return CreateQuery<TemplateEventCondition>();
      }
   }
}
