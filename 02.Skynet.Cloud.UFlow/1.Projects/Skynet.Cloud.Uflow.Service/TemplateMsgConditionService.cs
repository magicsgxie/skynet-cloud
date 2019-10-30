/************************************************************************************
* Copyright (c) 2019-07-11 12:02:25 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateMsgCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：ae41e94e-0738-4585-ac2c-64715c98c580
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:25 
* 描述：消息启用条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:25 
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
   /// 消息启用条件服务实现类
   /// </summary>
   public class TemplateMsgConditionService: ITemplateMsgConditionService
   {
      /// <summary>
      /// 添加消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateMsgCondition  templateMsgCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgConditionRepository(dbContext).Add(templateMsgCondition);
         }
      }
      /// <summary>
      /// 添加消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateMsgCondition>  templateMsgConditions)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateMsgConditionRepository(dbContext).Add(templateMsgConditions);
         }
      }
      /// <summary>
      /// 更新消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateMsgCondition  templateMsgCondition)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgConditionRepository(dbContext).Update(templateMsgCondition);
         }
      }
      /// <summary>
      /// 删除消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgConditionRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public TemplateMsgCondition GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgConditionRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的消息启用条件{消息启用条件}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgConditionRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
