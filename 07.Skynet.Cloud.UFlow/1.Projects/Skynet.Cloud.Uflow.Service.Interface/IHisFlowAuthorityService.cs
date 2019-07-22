/************************************************************************************
* Copyright (c) 2019-07-11 12:02:54 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisFlowAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：10a39fb3-afa7-409e-baf5-b176f439d871
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:54 
* 描述：记录流程实例的发起人，和所有可以监控这个流程的人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:54 
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
   public interface IHisFlowAuthorityService
   {
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      long Add(HisFlowAuthority  hisFlowAuthority);
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      void Add(IList<HisFlowAuthority>  hisFlowAuthoritys);
      /// <summary>
      /// 更新记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      int Update(HisFlowAuthority  hisFlowAuthority);
      /// <summary>
      /// 删除记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      HisFlowAuthority GetById(string id);
      /// <summary>
      /// 获取所有的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
