/************************************************************************************
* Copyright (c) 2019-07-11 12:03:16 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：e79e546c-6b0c-419f-a46e-3321bfb487e3
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
   /// 子流程表服务实现类
   /// </summary>
   public class InstanceStepSubflowService: IInstanceStepSubflowService
   {
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepSubflow  instanceStepSubflow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepSubflowRepository(dbContext).Add(instanceStepSubflow);
         }
      }
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepSubflow>  instanceStepSubflows)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepSubflowRepository(dbContext).Add(instanceStepSubflows);
         }
      }
      /// <summary>
      /// 更新子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepSubflow  instanceStepSubflow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepSubflowRepository(dbContext).Update(instanceStepSubflow);
         }
      }
      /// <summary>
      /// 删除子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepSubflowRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public InstanceStepSubflow GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepSubflowRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepSubflowRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
