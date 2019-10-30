/************************************************************************************
* Copyright (c) 2019-07-11 12:02:53 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：45ab0262-d62b-4788-ac8b-ffcc967c4749
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:53 
* 描述：流程实例表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:53 
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
   /// 流程实例表服务接口类
   /// </summary>
   public interface IHisFlowService
   {
      /// <summary>
      /// 添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      long Add(HisFlow  hisFlow);
      /// <summary>
      /// 添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      void Add(IList<HisFlow>  hisFlows);
      /// <summary>
      /// 更新流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      int Update(HisFlow  hisFlow);
      /// <summary>
      /// 删除流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      HisFlow GetById(string id);
      /// <summary>
      /// 获取所有的流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
