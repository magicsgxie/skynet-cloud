/************************************************************************************
* Copyright (c) 2019-07-11 12:02:35 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepNextstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：c860ac63-4de4-40e3-9f77-04e3b14c77bd
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:35 
* 描述：下一步可选人员列表 
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
   /// 下一步可选人员列表服务实现类
   /// </summary>
   public class TemplateStepNextstepUserService: ITemplateStepNextstepUserService
   {
      /// <summary>
      /// 添加下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepNextstepUser  templateStepNextstepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepNextstepUserRepository(dbContext).Add(templateStepNextstepUser);
         }
      }
      /// <summary>
      /// 添加下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepNextstepUser>  templateStepNextstepUsers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepNextstepUserRepository(dbContext).Add(templateStepNextstepUsers);
         }
      }
      /// <summary>
      /// 更新下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepNextstepUser  templateStepNextstepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepNextstepUserRepository(dbContext).Update(templateStepNextstepUser);
         }
      }
      /// <summary>
      /// 删除下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepNextstepUserRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public TemplateStepNextstepUser GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepNextstepUserRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepNextstepUserRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
