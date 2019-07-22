/************************************************************************************
* Copyright (c) 2019-07-11 12:02:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：fca982c7-f79d-417f-8cfd-e886e2500e03
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:23 
* 描述：流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:23 
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
   /// 流程表服务实现类
   /// </summary>
   public class TemplateFlowService: ITemplateFlowService
   {
      /// <summary>
      /// 添加流程表{流程表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateFlow  templateFlow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowRepository(dbContext).Add(templateFlow);
         }
      }
      /// <summary>
      /// 添加流程表{流程表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateFlow>  templateFlows)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateFlowRepository(dbContext).Add(templateFlows);
         }
      }
      /// <summary>
      /// 更新流程表{流程表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateFlow  templateFlow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowRepository(dbContext).Update(templateFlow);
         }
      }
      /// <summary>
      /// 删除流程表{流程表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程表{流程表}对象(即:一条记录
      /// </summary>
      public TemplateFlow GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程表{流程表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
