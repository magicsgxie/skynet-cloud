/************************************************************************************
* Copyright (c) 2019-07-11 12:02:58 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：468f336c-c362-47f1-a6e0-f2135d643fe5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:58 
* 描述：步骤消息处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:58 
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
   /// 步骤消息处理服务接口类
   /// </summary>
   public interface IHisStepMsgService
   {
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      long Add(HisStepMsg  hisStepMsg);
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      void Add(IList<HisStepMsg>  hisStepMsgs);
      /// <summary>
      /// 更新步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      int Update(HisStepMsg  hisStepMsg);
      /// <summary>
      /// 删除步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      HisStepMsg GetById(string id);
      /// <summary>
      /// 获取所有的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
