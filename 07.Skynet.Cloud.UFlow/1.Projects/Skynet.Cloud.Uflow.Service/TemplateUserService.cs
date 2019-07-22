/************************************************************************************
* Copyright (c) 2019-07-11 12:02:40 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：6d3b4f80-494a-434e-bff7-01ffff2c6a98
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:40 
* 描述：人员 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:40 
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
   /// 人员服务实现类
   /// </summary>
   public class TemplateUserService: ITemplateUserService
   {
      /// <summary>
      /// 添加人员{人员}对象(即:一条记录
      /// </summary>
      public long Add(TemplateUser  templateUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateUserRepository(dbContext).Add(templateUser);
         }
      }
      /// <summary>
      /// 添加人员{人员}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateUser>  templateUsers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateUserRepository(dbContext).Add(templateUsers);
         }
      }
      /// <summary>
      /// 更新人员{人员}对象(即:一条记录
      /// </summary>
      public int Update(TemplateUser  templateUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateUserRepository(dbContext).Update(templateUser);
         }
      }
      /// <summary>
      /// 删除人员{人员}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateUserRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的人员{人员}对象(即:一条记录
      /// </summary>
      public TemplateUser GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateUserRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的人员{人员}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateUserRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
