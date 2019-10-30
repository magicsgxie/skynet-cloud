/************************************************************************************
* Copyright (c) 2019-07-11 12:08:24 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceFlowAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：a8ca8056-bab5-4b00-a594-40a63a2d6679
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:24 
* 描述：记录流程实例的发起人，和所有可以监控这个流程的人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:24 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Repository
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using  UWay.Skynet.Cloud.Uflow.Entity;
   using System.Linq;
   using System.Collections.Generic;

   /// <summary>
   /// 记录流程实例的发起人，和所有可以监控这个流程的人仓储类
   /// </summary>
   public class InstanceFlowAuthorityRepository:ObjectRepository
   {
      public InstanceFlowAuthorityRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public long Add(InstanceFlowAuthority  instanceFlowAuthority)
      {
         return Add<InstanceFlowAuthority>(instanceFlowAuthority);
      }
      /// <summary>
      /// 批量添加记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceFlowAuthority>  instanceFlowAuthoritys)
      {
         Batch<long, InstanceFlowAuthority>(instanceFlowAuthoritys, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public int Update(InstanceFlowAuthority  instanceFlowAuthority)
      {
         return Update<InstanceFlowAuthority>(instanceFlowAuthority);
      }
      /// <summary>
      /// 删除记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceFlowAuthority>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象(即:一条记录
      /// </summary>
      public InstanceFlowAuthority GetById(string id)
      {
         return GetByID<InstanceFlowAuthority>(id);
      }
      /// <summary>
      /// 获取所有的记录流程实例的发起人，和所有可以监控这个流程的人{记录流程实例的发起人，和所有可以监控这个流程的人}对象
      /// </summary>
      public IQueryable<InstanceFlowAuthority> Query()
      {
         return CreateQuery<InstanceFlowAuthority>();
      }
   }
}
