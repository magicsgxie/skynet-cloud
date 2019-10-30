/************************************************************************************
* Copyright (c) 2019-07-11 12:03:09 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：590458c8-1cc5-4b7d-a77f-2ad2476b3280
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:09 
* 描述：实例事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:09 
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
   /// 实例事件表服务接口类
   /// </summary>
   public interface IInstanceFlowEventService
   {
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      long Add(InstanceFlowEvent  instanceFlowEvent);
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceFlowEvent>  instanceFlowEvents);
      /// <summary>
      /// 更新实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      int Update(InstanceFlowEvent  instanceFlowEvent);
      /// <summary>
      /// 删除实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      InstanceFlowEvent GetById(string id);
      /// <summary>
      /// 获取所有的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
