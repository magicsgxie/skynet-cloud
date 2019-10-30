/************************************************************************************
* Copyright (c) 2019-07-11 12:02:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：e312d65b-921b-41f7-8a16-7e18cce235e1
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:44 
* 描述：条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:44 
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
   /// 条件服务实现类
   /// </summary>
   public class TemplateConditionService: ITemplateConditionService
   {
      /// <summary>
      /// 添加条件{条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateCondition  templateCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateConditionRepository(dbContext).Add(templateCondition);
         }
      }
      /// <summary>
      /// 添加条件{条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateCondition>  templateConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateConditionRepository(dbContext).Add(templateConditions);
         }
      }
      /// <summary>
      /// 更新条件{条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateCondition  templateCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateConditionRepository(dbContext).Update(templateCondition);
         }
      }
      /// <summary>
      /// 删除条件{条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的条件{条件}对象(即:一条记录
      /// </summary>
      public TemplateCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的条件{条件}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
