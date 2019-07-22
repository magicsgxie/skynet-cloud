/************************************************************************************
* Copyright (c) 2019-07-11 12:03:07 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：e5dcc9b6-429a-4153-a3c0-5d55078beb61
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:07 
* 描述：流程实例表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:07 
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
   /// 流程实例表服务实现类
   /// </summary>
   public class InstanceFlowService: IInstanceFlowService
   {
      /// <summary>
      /// 添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceFlow  instanceFlow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowRepository(dbContext).Add(instanceFlow);
         }
      }
      /// <summary>
      /// 添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceFlow>  instanceFlows)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceFlowRepository(dbContext).Add(instanceFlows);
         }
      }
      /// <summary>
      /// 更新流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceFlow  instanceFlow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowRepository(dbContext).Update(instanceFlow);
         }
      }
      /// <summary>
      /// 删除流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public InstanceFlow GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceFlowRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
