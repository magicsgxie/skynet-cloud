/************************************************************************************
* Copyright (c) 2019-07-11 12:02:29 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：30c57c0c-48e9-411e-8bbd-bf37a49709a0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:29 
* 描述：步骤转入转出条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:29 
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
   /// 步骤转入转出条件服务实现类
   /// </summary>
   public class TemplateStepConditionService: ITemplateStepConditionService
   {
      /// <summary>
      /// 添加步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepCondition  templateStepCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepConditionRepository(dbContext).Add(templateStepCondition);
         }
      }
      /// <summary>
      /// 添加步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepCondition>  templateStepConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepConditionRepository(dbContext).Add(templateStepConditions);
         }
      }
      /// <summary>
      /// 更新步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepCondition  templateStepCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepConditionRepository(dbContext).Update(templateStepCondition);
         }
      }
      /// <summary>
      /// 删除步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public TemplateStepCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤转入转出条件{步骤转入转出条件}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
