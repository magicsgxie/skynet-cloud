/************************************************************************************
* Copyright (c) 2019-07-11 12:02:25 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateMsgCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：d9c3f4ba-82e8-4a0d-8aab-ae68add64760
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:25 
* 描述：消息启用条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:25 
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
   /// 消息启用条件服务接口类
   /// </summary>
   public interface ITemplateMsgConditionService
   {
      /// <summary>
      /// 添加消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      long Add(TemplateMsgCondition  templateMsgCondition);
      /// <summary>
      /// 添加消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateMsgCondition>  templateMsgConditions);
      /// <summary>
      /// 更新消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      int Update(TemplateMsgCondition  templateMsgCondition);
      /// <summary>
      /// 删除消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      TemplateMsgCondition GetById(string id);
      /// <summary>
      /// 获取所有的消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
