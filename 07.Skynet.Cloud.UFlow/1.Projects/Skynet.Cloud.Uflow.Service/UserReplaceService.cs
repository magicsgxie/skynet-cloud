/************************************************************************************
* Copyright (c) 2019-07-11 12:02:41 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.UserReplace.cs
* 版本号：  V1.0.0.0
* 唯一标识：4b0d2b3a-f628-4233-a46d-b7973406e37a
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:41 
* 描述：设置代办人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:41 
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
   /// 设置代办人服务实现类
   /// </summary>
   public class UserReplaceService: IUserReplaceService
   {
      /// <summary>
      /// 添加设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public long Add(UserReplace  userReplace)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new UserReplaceRepository(dbContext).Add(userReplace);
         }
      }
      /// <summary>
      /// 添加设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public void Add(IList<UserReplace>  userReplaces)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new UserReplaceRepository(dbContext).Add(userReplaces);
         }
      }
      /// <summary>
      /// 更新设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public int Update(UserReplace  userReplace)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new UserReplaceRepository(dbContext).Update(userReplace);
         }
      }
      /// <summary>
      /// 删除设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new UserReplaceRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public UserReplace GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new UserReplaceRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new UserReplaceRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
