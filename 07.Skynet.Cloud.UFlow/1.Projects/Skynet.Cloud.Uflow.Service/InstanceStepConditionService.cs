/************************************************************************************
* Copyright (c) 2019-07-11 12:03:10 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：2ede3caf-6a8b-45cf-acb5-faf07ecb730c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:10 
* 描述：步骤转入转出条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:10 
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
   /// 步骤转入转出条件结果服务实现类
   /// </summary>
   public class InstanceStepConditionService: IInstanceStepConditionService
   {
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepCondition  instanceStepCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepConditionRepository(dbContext).Add(instanceStepCondition);
         }
      }
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepCondition>  instanceStepConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepConditionRepository(dbContext).Add(instanceStepConditions);
         }
      }
      /// <summary>
      /// 更新步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepCondition  instanceStepCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepConditionRepository(dbContext).Update(instanceStepCondition);
         }
      }
      /// <summary>
      /// 删除步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public InstanceStepCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
