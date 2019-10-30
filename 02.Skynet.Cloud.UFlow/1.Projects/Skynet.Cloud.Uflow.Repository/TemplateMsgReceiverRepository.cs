/************************************************************************************
* Copyright (c) 2019-07-11 12:07:41 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateMsgReceiver.cs
* 版本号：  V1.0.0.0
* 唯一标识：e3af9df6-785f-41b6-a37a-26cb11ef006f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:41 
* 描述：消息接收者 
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
   /// 消息接收者仓储类
   /// </summary>
   public class TemplateMsgReceiverRepository:ObjectRepository
   {
      public TemplateMsgReceiverRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public long Add(TemplateMsgReceiver  templateMsgReceiver)
      {
         return Add<TemplateMsgReceiver>(templateMsgReceiver);
      }
      /// <summary>
      /// 批量添加消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateMsgReceiver>  templateMsgReceivers)
      {
         Batch<long, TemplateMsgReceiver>(templateMsgReceivers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public int Update(TemplateMsgReceiver  templateMsgReceiver)
      {
         return Update<TemplateMsgReceiver>(templateMsgReceiver);
      }
      /// <summary>
      /// 删除消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateMsgReceiver>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public TemplateMsgReceiver GetById(string id)
      {
         return GetByID<TemplateMsgReceiver>(id);
      }
      /// <summary>
      /// 获取所有的消息接收者{消息接收者}对象
      /// </summary>
      public IQueryable<TemplateMsgReceiver> Query()
      {
         return CreateQuery<TemplateMsgReceiver>();
      }
   }
}
