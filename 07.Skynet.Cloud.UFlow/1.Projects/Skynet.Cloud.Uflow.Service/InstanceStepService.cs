/************************************************************************************
* Copyright (c) 2019-07-11 12:03:09 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStep.cs
* 版本号：  V1.0.0.0
* 唯一标识：d151b4de-9129-4661-9845-37bef071d9f6
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:09 
* 描述：实例步骤表 
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
   /// 实例步骤表服务实现类
   /// </summary>
   public class InstanceStepService: IInstanceStepService
   {
      /// <summary>
      /// 添加实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStep  instanceStep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepRepository(dbContext).Add(instanceStep);
         }
      }
      /// <summary>
      /// 添加实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStep>  instanceSteps)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepRepository(dbContext).Add(instanceSteps);
         }
      }
      /// <summary>
      /// 更新实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStep  instanceStep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepRepository(dbContext).Update(instanceStep);
         }
      }
      /// <summary>
      /// 删除实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public InstanceStep GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
