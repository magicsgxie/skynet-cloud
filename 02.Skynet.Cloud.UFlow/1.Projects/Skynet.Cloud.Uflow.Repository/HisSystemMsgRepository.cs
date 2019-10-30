/************************************************************************************
* Copyright (c) 2019-07-11 12:08:19 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisSystemMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：f97f7694-8360-42f1-b988-4693aa45e068
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:19 
* 描述：用户接收的系统消息 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:19 
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
   /// 用户接收的系统消息仓储类
   /// </summary>
   public class HisSystemMsgRepository:ObjectRepository
   {
      public HisSystemMsgRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public long Add(HisSystemMsg  hisSystemMsg)
      {
         return Add<HisSystemMsg>(hisSystemMsg);
      }
      /// <summary>
      /// 批量添加用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisSystemMsg>  hisSystemMsgs)
      {
         Batch<long, HisSystemMsg>(hisSystemMsgs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public int Update(HisSystemMsg  hisSystemMsg)
      {
         return Update<HisSystemMsg>(hisSystemMsg);
      }
      /// <summary>
      /// 删除用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisSystemMsg>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public HisSystemMsg GetById(string id)
      {
         return GetByID<HisSystemMsg>(id);
      }
      /// <summary>
      /// 获取所有的用户接收的系统消息{用户接收的系统消息}对象
      /// </summary>
      public IQueryable<HisSystemMsg> Query()
      {
         return CreateQuery<HisSystemMsg>();
      }
   }
}
