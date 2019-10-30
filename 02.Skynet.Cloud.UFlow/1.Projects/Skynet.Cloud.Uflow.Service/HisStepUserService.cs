/************************************************************************************
* Copyright (c) 2019-07-11 12:01:43 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：51c5222a-029f-4a10-9468-164f44b7ffb3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:01:43 
* 描述：实例步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:01:43 
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
   /// 实例步骤处理人服务实现类
   /// </summary>
   public class HisStepUserService: IHisStepUserService
   {
      /// <summary>
      /// 添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public long Add(HisStepUser  hisStepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepUserRepository(dbContext).Add(hisStepUser);
         }
      }
      /// <summary>
      /// 添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepUser>  hisStepUsers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepUserRepository(dbContext).Add(hisStepUsers);
         }
      }
      /// <summary>
      /// 更新实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public int Update(HisStepUser  hisStepUser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepUserRepository(dbContext).Update(hisStepUser);
         }
      }
      /// <summary>
      /// 删除实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepUserRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public HisStepUser GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepUserRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepUserRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
