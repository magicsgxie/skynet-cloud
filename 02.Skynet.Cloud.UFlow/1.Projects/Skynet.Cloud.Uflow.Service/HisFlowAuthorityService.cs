/************************************************************************************
* Copyright (c) 2019-07-11 12:02:54 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisFlowAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：22918178-0d7f-4952-822b-3e9d2a5ee254
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



namespace   UWay.Skynet.Cloud.Uflow.Service
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;

   using System.Linq;
   using UWay.Skynet.Cloud.Uflow.Service.Interface;
   using UWay.Skynet.Cloud.Uflow.Repository;
   using UWay.Skynet.Cloud.Linq;
   using UWay.Skynet.Cloud;

   /// <summary>
   /// 记录流程实例的发起人，和所有可以监控这个流程的人服务实现类
   /// </summary>
   public class HisFlowAuthorityService: IHisFlowAuthorityService
   {
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public long Add(HisFlowAuthority  hisFlowAuthority)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowAuthorityRepository(dbContext).Add(hisFlowAuthority);
         }
      }
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisFlowAuthority>  hisFlowAuthoritys)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisFlowAuthorityRepository(dbContext).Add(hisFlowAuthoritys);
         }
      }
      /// <summary>
      /// 更新记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public int Update(HisFlowAuthority  hisFlowAuthority)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowAuthorityRepository(dbContext).Update(hisFlowAuthority);
         }
      }
      /// <summary>
      /// 删除记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowAuthorityRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public HisFlowAuthority GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowAuthorityRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowAuthorityRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
