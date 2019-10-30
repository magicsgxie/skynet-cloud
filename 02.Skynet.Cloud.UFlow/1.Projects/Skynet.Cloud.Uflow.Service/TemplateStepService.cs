/************************************************************************************
* Copyright (c) 2019-07-11 12:02:28 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStep.cs
* 版本号：  V1.0.0.0
* 唯一标识：de7f24fd-e513-4676-8dbc-13df45efa0e5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:28 
* 描述：流程步骤表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:28 
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
   /// 流程步骤表服务实现类
   /// </summary>
   public class TemplateStepService: ITemplateStepService
   {
      /// <summary>
      /// 添加流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStep  templateStep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepRepository(dbContext).Add(templateStep);
         }
      }
      /// <summary>
      /// 添加流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStep>  templateSteps)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepRepository(dbContext).Add(templateSteps);
         }
      }
      /// <summary>
      /// 更新流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStep  templateStep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepRepository(dbContext).Update(templateStep);
         }
      }
      /// <summary>
      /// 删除流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public TemplateStep GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程步骤表{流程步骤表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
