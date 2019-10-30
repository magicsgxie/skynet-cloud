/************************************************************************************
* Copyright (c) 2019-07-11 12:08:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：785733be-39e1-47b4-8075-03390ad76bb2
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:23 
* 描述：流程实例表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:23 
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
   /// 流程实例表仓储类
   /// </summary>
   public class InstanceFlowRepository:ObjectRepository
   {
      public InstanceFlowRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceFlow  instanceFlow)
      {
         return Add<InstanceFlow>(instanceFlow);
      }
      /// <summary>
      /// 批量添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceFlow>  instanceFlows)
      {
         Batch<long, InstanceFlow>(instanceFlows, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceFlow  instanceFlow)
      {
         return Update<InstanceFlow>(instanceFlow);
      }
      /// <summary>
      /// 删除流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceFlow>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public InstanceFlow GetById(string id)
      {
         return GetByID<InstanceFlow>(id);
      }
      /// <summary>
      /// 获取所有的流程实例表{流程实例表}对象
      /// </summary>
      public IQueryable<InstanceFlow> Query()
      {
         return CreateQuery<InstanceFlow>();
      }
   }
}
