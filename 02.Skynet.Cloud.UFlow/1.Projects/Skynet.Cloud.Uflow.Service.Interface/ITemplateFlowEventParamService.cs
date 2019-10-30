/************************************************************************************
* Copyright (c) 2019-07-11 12:02:24 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateFlowEventParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：f49d99c7-d71a-4b1c-8308-0169f297806c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:24 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:24 
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
   /// 流程业务服务接口类
   /// </summary>
   public interface ITemplateFlowEventParamService
   {
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      long Add(TemplateFlowEventParam  templateFlowEventParam);
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateFlowEventParam>  templateFlowEventParams);
      /// <summary>
      /// 更新流程业务{流程业务}对象(即:一条记录
      /// </summary>
      int Update(TemplateFlowEventParam  templateFlowEventParam);
      /// <summary>
      /// 删除流程业务{流程业务}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      TemplateFlowEventParam GetById(string id);
      /// <summary>
      /// 获取所有的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
