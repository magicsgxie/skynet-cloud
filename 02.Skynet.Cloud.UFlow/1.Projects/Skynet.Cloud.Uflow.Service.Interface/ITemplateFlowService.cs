/************************************************************************************
* Copyright (c) 2019-07-11 12:02:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：3537fc9a-2915-4a51-ac23-f13c06fe58b8
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:23 
* 描述：流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:23 
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
   /// 流程表服务接口类
   /// </summary>
   public interface ITemplateFlowService
   {
      /// <summary>
      /// 添加流程表{流程表}对象(即:一条记录
      /// </summary>
      long Add(TemplateFlow  templateFlow);
      /// <summary>
      /// 添加流程表{流程表}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateFlow>  templateFlows);
      /// <summary>
      /// 更新流程表{流程表}对象(即:一条记录
      /// </summary>
      int Update(TemplateFlow  templateFlow);
      /// <summary>
      /// 删除流程表{流程表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的流程表{流程表}对象(即:一条记录
      /// </summary>
      TemplateFlow GetById(string id);
      /// <summary>
      /// 获取所有的流程表{流程表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
