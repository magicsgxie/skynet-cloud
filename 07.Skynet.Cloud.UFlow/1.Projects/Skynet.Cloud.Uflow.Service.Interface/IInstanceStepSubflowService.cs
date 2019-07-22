/************************************************************************************
* Copyright (c) 2019-07-11 12:03:16 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：edf18f03-4faf-4249-acd7-d123e2795c12
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:16 
* 描述：子流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:16 
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
   /// 子流程表服务接口类
   /// </summary>
   public interface IInstanceStepSubflowService
   {
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepSubflow  instanceStepSubflow);
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepSubflow>  instanceStepSubflows);
      /// <summary>
      /// 更新子流程表{子流程表}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepSubflow  instanceStepSubflow);
      /// <summary>
      /// 删除子流程表{子流程表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      InstanceStepSubflow GetById(string id);
      /// <summary>
      /// 获取所有的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
