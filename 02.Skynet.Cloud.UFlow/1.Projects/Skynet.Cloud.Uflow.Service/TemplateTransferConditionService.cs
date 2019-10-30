/************************************************************************************
* Copyright (c) 2019-07-11 12:02:39 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateTransferCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：d6c6f56e-8007-479d-a93e-2e84285b665d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:39 
* 描述：转发条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:39 
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
   /// 转发条件服务实现类
   /// </summary>
   public class TemplateTransferConditionService: ITemplateTransferConditionService
   {
      /// <summary>
      /// 添加转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateTransferCondition  templatetransferCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTransferConditionRepository(dbContext).Add(templatetransferCondition);
         }
      }
      /// <summary>
      /// 添加转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateTransferCondition>  templatetransferConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateTransferConditionRepository(dbContext).Add(templatetransferConditions);
         }
      }
      /// <summary>
      /// 更新转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateTransferCondition  templatetransferCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTransferConditionRepository(dbContext).Update(templatetransferCondition);
         }
      }
      /// <summary>
      /// 删除转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTransferConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public TemplateTransferCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTransferConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTransferConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
