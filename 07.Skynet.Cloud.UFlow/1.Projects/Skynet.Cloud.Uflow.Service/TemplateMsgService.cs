/************************************************************************************
* Copyright (c) 2019-07-11 12:02:24 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：9d88ba9d-a5b8-4683-948e-5796cf5192e7
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:24 
* 描述：消息 
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
   /// 消息服务实现类
   /// </summary>
   public class TemplateMsgService: ITemplateMsgService
   {
      /// <summary>
      /// 添加消息{消息}对象(即:一条记录
      /// </summary>
      public long Add(TemplateMsg  templateMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgRepository(dbContext).Add(templateMsg);
         }
      }
      /// <summary>
      /// 添加消息{消息}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateMsg>  templateMsgs)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateMsgRepository(dbContext).Add(templateMsgs);
         }
      }
      /// <summary>
      /// 更新消息{消息}对象(即:一条记录
      /// </summary>
      public int Update(TemplateMsg  templateMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgRepository(dbContext).Update(templateMsg);
         }
      }
      /// <summary>
      /// 删除消息{消息}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的消息{消息}对象(即:一条记录
      /// </summary>
      public TemplateMsg GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的消息{消息}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateMsgRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
