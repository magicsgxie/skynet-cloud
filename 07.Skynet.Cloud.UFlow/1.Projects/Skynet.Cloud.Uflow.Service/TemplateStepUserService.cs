/************************************************************************************
* Copyright (c) 2019-07-11 12:02:38 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：18ce6c4d-9dc5-49e1-95cc-239772269b3a
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:38 
* 描述：步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:38 
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
   /// 步骤处理人服务实现类
   /// </summary>
   public class TemplateStepUserService: ITemplateStepUserService
   {
      /// <summary>
      /// 添加步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepUser  templateStepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepUserRepository(dbContext).Add(templateStepUser);
         }
      }
      /// <summary>
      /// 添加步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepUser>  templateStepUsers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepUserRepository(dbContext).Add(templateStepUsers);
         }
      }
      /// <summary>
      /// 更新步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepUser  templateStepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepUserRepository(dbContext).Update(templateStepUser);
         }
      }
      /// <summary>
      /// 删除步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepUserRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public TemplateStepUser GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepUserRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepUserRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
