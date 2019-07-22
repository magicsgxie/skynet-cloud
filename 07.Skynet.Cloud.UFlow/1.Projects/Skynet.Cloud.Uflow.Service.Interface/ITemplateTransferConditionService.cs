/************************************************************************************
* Copyright (c) 2019-07-11 12:02:39 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateTransferCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：b98318a6-4a9d-4f59-b366-f47ad75d0fca
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:39 
* 描述：转发条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:39 
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
   /// 转发条件服务接口类
   /// </summary>
   public interface ITemplateTransferConditionService
   {
      /// <summary>
      /// 添加转发条件{转发条件}对象(即:一条记录
      /// </summary>
      long Add(TemplateTransferCondition  templatetransferCondition);
      /// <summary>
      /// 添加转发条件{转发条件}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateTransferCondition>  templatetransferConditions);
      /// <summary>
      /// 更新转发条件{转发条件}对象(即:一条记录
      /// </summary>
      int Update(TemplateTransferCondition  templatetransferCondition);
      /// <summary>
      /// 删除转发条件{转发条件}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的转发条件{转发条件}对象(即:一条记录
      /// </summary>
      TemplateTransferCondition GetById(string id);
      /// <summary>
      /// 获取所有的转发条件{转发条件}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
