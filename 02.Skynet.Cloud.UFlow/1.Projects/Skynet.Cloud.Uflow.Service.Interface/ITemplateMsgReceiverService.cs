/************************************************************************************
* Copyright (c) 2019-07-11 12:02:26 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateMsgReceiver.cs
* 版本号：  V1.0.0.0
* 唯一标识：321660c7-2958-4a92-a805-b0901163babe
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



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 消息接收者服务接口类
   /// </summary>
   public interface ITemplateMsgReceiverService
   {
      /// <summary>
      /// 添加消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      long Add(TemplateMsgReceiver  templateMsgReceiver);
      /// <summary>
      /// 添加消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateMsgReceiver>  templateMsgReceivers);
      /// <summary>
      /// 更新消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      int Update(TemplateMsgReceiver  templateMsgReceiver);
      /// <summary>
      /// 删除消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      TemplateMsgReceiver GetById(string id);
      /// <summary>
      /// 获取所有的消息接收者{消息接收者}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
