/************************************************************************************
* Copyright (c) 2019-07-11 12:02:26 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateMsgReceiver.cs
* 版本号：  V1.0.0.0
* 唯一标识：dcca8499-5584-4365-b310-2898b00e149f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:26 
* 描述：消息接收者 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:26 
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
   /// 消息接收者服务实现类
   /// </summary>
   public class TemplateMsgReceiverService: ITemplateMsgReceiverService
   {
      /// <summary>
      /// 添加消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public long Add(TemplateMsgReceiver  templateMsgReceiver)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgReceiverRepository(dbContext).Add(templateMsgReceiver);
         }
      }
      /// <summary>
      /// 添加消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateMsgReceiver>  templateMsgReceivers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateMsgReceiverRepository(dbContext).Add(templateMsgReceivers);
         }
      }
      /// <summary>
      /// 更新消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public int Update(TemplateMsgReceiver  templateMsgReceiver)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgReceiverRepository(dbContext).Update(templateMsgReceiver);
         }
      }
      /// <summary>
      /// 删除消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgReceiverRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public TemplateMsgReceiver GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgReceiverRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgReceiverRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
