/************************************************************************************
* Copyright (c) 2019-07-11 12:03:03 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisSystemMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：0508ab1b-d7a3-4c6f-b59a-45de4096efdc
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:03 
* 描述：用户接收的系统消息 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:03 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Service
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;

   using System.Linq;
   using UWay.Skynet.Cloud.Uflow.Service.Interface;
   using UWay.Skynet.Cloud.Uflow.Repository;
   using UWay.Skynet.Cloud.Linq;
   using UWay.Skynet.Cloud;

   /// <summary>
   /// 用户接收的系统消息服务实现类
   /// </summary>
   public class HisSystemMsgService: IHisSystemMsgService
   {
      /// <summary>
      /// 添加用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public long Add(HisSystemMsg  hisSystemMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSystemMsgRepository(dbContext).Add(hisSystemMsg);
         }
      }
      /// <summary>
      /// 添加用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisSystemMsg>  hisSystemMsgs)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisSystemMsgRepository(dbContext).Add(hisSystemMsgs);
         }
      }
      /// <summary>
      /// 更新用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public int Update(HisSystemMsg  hisSystemMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSystemMsgRepository(dbContext).Update(hisSystemMsg);
         }
      }
      /// <summary>
      /// 删除用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSystemMsgRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public HisSystemMsg GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSystemMsgRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的用户接收的系统消息{用户接收的系统消息}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSystemMsgRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
