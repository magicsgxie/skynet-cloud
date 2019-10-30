/************************************************************************************
* Copyright (c) 2019-07-11 12:02:35 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：dcc7a9a0-9bb6-4e42-860e-f420a184a50f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:35 
* 描述：子流程 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:35 
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
   /// 子流程服务实现类
   /// </summary>
   public class TemplateStepSubflowService: ITemplateStepSubflowService
   {
      /// <summary>
      /// 添加子流程{子流程}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepSubflow  templateStepSubflow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepSubflowRepository(dbContext).Add(templateStepSubflow);
         }
      }
      /// <summary>
      /// 添加子流程{子流程}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepSubflow>  templateStepSubflows)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepSubflowRepository(dbContext).Add(templateStepSubflows);
         }
      }
      /// <summary>
      /// 更新子流程{子流程}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepSubflow  templateStepSubflow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepSubflowRepository(dbContext).Update(templateStepSubflow);
         }
      }
      /// <summary>
      /// 删除子流程{子流程}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepSubflowRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的子流程{子流程}对象(即:一条记录
      /// </summary>
      public TemplateStepSubflow GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepSubflowRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的子流程{子流程}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepSubflowRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
