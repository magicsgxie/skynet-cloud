/************************************************************************************
* Copyright (c) 2019-07-11 12:08:01 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceSystemMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：9bf541f7-143e-4a27-adfc-cb37340b9fbb
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:01 
* 描述：用户接收的系统消息 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:01 
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
   public class InstanceSystemMsgRepository:ObjectRepository
   {
      public InstanceSystemMsgRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public long Add(InstanceSystemMsg  instanceSystemMsg)
      {
         return Add<InstanceSystemMsg>(instanceSystemMsg);
      }
      /// <summary>
      /// 批量添加用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceSystemMsg>  instanceSystemMsgs)
      {
         Batch<long, InstanceSystemMsg>(instanceSystemMsgs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public int Update(InstanceSystemMsg  instanceSystemMsg)
      {
         return Update<InstanceSystemMsg>(instanceSystemMsg);
      }
      /// <summary>
      /// 删除用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceSystemMsg>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public InstanceSystemMsg GetById(string id)
      {
         return GetByID<InstanceSystemMsg>(id);
      }
      /// <summary>
      /// 获取所有的用户接收的系统消息{用户接收的系统消息}对象
      /// </summary>
      public IQueryable<InstanceSystemMsg> Query()
      {
         return CreateQuery<InstanceSystemMsg>();
      }
   }
}
