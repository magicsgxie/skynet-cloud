/************************************************************************************
* Copyright (c) 2019-07-11 12:02:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：ec791dc0-f2f1-4b2d-b692-7239eb86dd88
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:44 
* 描述：条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:44 
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
   /// 条件服务接口类
   /// </summary>
   public interface ITemplateConditionService
   {
      /// <summary>
      /// 添加条件{条件}对象(即:一条记录
      /// </summary>
      long Add(TemplateCondition  templateCondition);
      /// <summary>
      /// 添加条件{条件}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateCondition>  templateConditions);
      /// <summary>
      /// 更新条件{条件}对象(即:一条记录
      /// </summary>
      int Update(TemplateCondition  templateCondition);
      /// <summary>
      /// 删除条件{条件}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的条件{条件}对象(即:一条记录
      /// </summary>
      TemplateCondition GetById(string id);
      /// <summary>
      /// 获取所有的条件{条件}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
