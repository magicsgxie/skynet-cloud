/************************************************************************************
* Copyright (c) 2019-07-11 12:02:34 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepFixstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：47862050-78c8-4da9-8097-e3996aa5ad17
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:34 
* 描述：指定步骤可选人员列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:34 
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
   /// 指定步骤可选人员列表服务实现类
   /// </summary>
   public class TemplateStepFixstepUserService: ITemplateStepFixstepUserService
   {
      /// <summary>
      /// 添加指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepFixstepUser  templateStepFixstepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepUserRepository(dbContext).Add(templateStepFixstepUser);
         }
      }
      /// <summary>
      /// 添加指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepFixstepUser>  templateStepFixstepUsers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepFixstepUserRepository(dbContext).Add(templateStepFixstepUsers);
         }
      }
      /// <summary>
      /// 更新指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepFixstepUser  templateStepFixstepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepUserRepository(dbContext).Update(templateStepFixstepUser);
         }
      }
      /// <summary>
      /// 删除指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepUserRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public TemplateStepFixstepUser GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepUserRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepUserRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
