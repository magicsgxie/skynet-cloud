/************************************************************************************
* Copyright (c) 2019-07-11 12:07:39 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：464b4a36-5bdd-46cf-aa73-2173ef6fd3d9
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:39 
* 描述：消息 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:39 
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
   /// 消息仓储类
   /// </summary>
   public class TemplateMsgRepository:ObjectRepository
   {
      public TemplateMsgRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加消息{消息}对象(即:一条记录
      /// </summary>
      public long Add(TemplateMsg  templateMsg)
      {
         return Add<TemplateMsg>(templateMsg);
      }
      /// <summary>
      /// 批量添加消息{消息}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateMsg>  templateMsgs)
      {
         Batch<long, TemplateMsg>(templateMsgs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新消息{消息}对象(即:一条记录
      /// </summary>
      public int Update(TemplateMsg  templateMsg)
      {
         return Update<TemplateMsg>(templateMsg);
      }
      /// <summary>
      /// 删除消息{消息}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateMsg>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的消息{消息}对象(即:一条记录
      /// </summary>
      public TemplateMsg GetById(string id)
      {
         return GetByID<TemplateMsg>(id);
      }
      /// <summary>
      /// 获取所有的消息{消息}对象
      /// </summary>
      public IQueryable<TemplateMsg> Query()
      {
         return CreateQuery<TemplateMsg>();
      }
   }
}
