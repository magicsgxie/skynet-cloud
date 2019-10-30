/************************************************************************************
* Copyright (c) 2019-07-11 12:02:22 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateEventCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：72b19351-a36b-4617-8078-1ce69ab7dbfd
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:22 
* 描述：事件启用条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:22 
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
   /// 事件启用条件服务实现类
   /// </summary>
   public class TemplateEventConditionService: ITemplateEventConditionService
   {
      /// <summary>
      /// 添加事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateEventCondition  templateEventCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventConditionRepository(dbContext).Add(templateEventCondition);
         }
      }
      /// <summary>
      /// 添加事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateEventCondition>  templateEventConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateEventConditionRepository(dbContext).Add(templateEventConditions);
         }
      }
      /// <summary>
      /// 更新事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateEventCondition  templateEventCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventConditionRepository(dbContext).Update(templateEventCondition);
         }
      }
      /// <summary>
      /// 删除事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public TemplateEventCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的事件启用条件{事件启用条件}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateEventConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
