/************************************************************************************
* Copyright (c) 2019-07-11 12:02:56 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：3852a94b-8f23-40f4-8389-a777e0a8e616
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:56 
* 描述：步骤转入转出条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:56 
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
   public class HisStepConditionService: IHisStepConditionService
   {
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisStepCondition  hisStepCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepConditionRepository(dbContext).Add(hisStepCondition);
         }
      }
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepCondition>  hisStepConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepConditionRepository(dbContext).Add(hisStepConditions);
         }
      }
      /// <summary>
      /// 更新步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisStepCondition  hisStepCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepConditionRepository(dbContext).Update(hisStepCondition);
         }
      }
      /// <summary>
      /// 删除步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public HisStepCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
