/************************************************************************************
* Copyright (c) 2019-07-11 12:03:08 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceFlowAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：643f0c54-a8d4-4109-8aa4-1f79e4f6270d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:08 
* 描述：记录流程实例的发起人，和所有可以监控这个流程的人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:08 
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
   /// 记录流程实例的发起人，和所有可以监控这个流程的人服务接口类
   /// </summary>
   public interface IInstanceFlowAuthorityService
   {
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      long Add(InstanceFlowAuthority  instanceFlowAuthority);
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceFlowAuthority>  instanceFlowAuthoritys);
      /// <summary>
      /// 更新记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      int Update(InstanceFlowAuthority  instanceFlowAuthority);
      /// <summary>
      /// 删除记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      InstanceFlowAuthority GetById(string id);
      /// <summary>
      /// 获取所有的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
