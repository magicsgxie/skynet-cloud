/************************************************************************************
* Copyright (c) 2019-07-11 12:07:40 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateMsgCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：3d95e926-caa9-4ce2-818b-fa0d20d4454c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:40 
* 描述：消息启用条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:40 
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
   /// 消息启用条件仓储类
   /// </summary>
   public class TemplateMsgConditionRepository:ObjectRepository
   {
      public TemplateMsgConditionRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateMsgCondition  templateMsgCondition)
      {
         return Add<TemplateMsgCondition>(templateMsgCondition);
      }
      /// <summary>
      /// 批量添加消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateMsgCondition>  templateMsgConditions)
      {
         Batch<long, TemplateMsgCondition>(templateMsgConditions, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateMsgCondition  templateMsgCondition)
      {
         return Update<TemplateMsgCondition>(templateMsgCondition);
      }
      /// <summary>
      /// 删除消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateMsgCondition>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public TemplateMsgCondition GetById(string id)
      {
         return GetByID<TemplateMsgCondition>(id);
      }
      /// <summary>
      /// 获取所有的消息启用条件{消息启用条件}对象
      /// </summary>
      public IQueryable<TemplateMsgCondition> Query()
      {
         return CreateQuery<TemplateMsgCondition>();
      }
   }
}
