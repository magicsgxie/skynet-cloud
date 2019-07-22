/************************************************************************************
* Copyright (c) 2019-07-11 12:03:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：b0503b44-009f-492d-b3df-7dab4c5ade0c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:12 
* 描述：步骤前后期事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:12 
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
   /// 步骤前后期事件结果服务接口类
   /// </summary>
   public interface IInstanceStepEventService
   {
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepEvent  instanceStepEvent);
      /// <summary>
      /// 添加步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepEvent>  instanceStepEvents);
      /// <summary>
      /// 更新步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepEvent  instanceStepEvent);
      /// <summary>
      /// 删除步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      InstanceStepEvent GetById(string id);
      /// <summary>
      /// 获取所有的步骤前后期事件结果{步骤前后期事件结果}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
