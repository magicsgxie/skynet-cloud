/************************************************************************************
* Copyright (c) 2019-07-11 12:02:24 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateFlowEventParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：aa65d292-4c75-4b77-9d13-a0893f5a98d7
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:24 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:24 
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
   /// 流程业务服务实现类
   /// </summary>
   public class TemplateFlowEventParamService: ITemplateFlowEventParamService
   {
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public long Add(TemplateFlowEventParam  templateFlowEventParam)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventParamRepository(dbContext).Add(templateFlowEventParam);
         }
      }
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateFlowEventParam>  templateFlowEventParams)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateFlowEventParamRepository(dbContext).Add(templateFlowEventParams);
         }
      }
      /// <summary>
      /// 更新流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Update(TemplateFlowEventParam  templateFlowEventParam)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventParamRepository(dbContext).Update(templateFlowEventParam);
         }
      }
      /// <summary>
      /// 删除流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventParamRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public TemplateFlowEventParam GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventParamRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateFlowEventParamRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
