/************************************************************************************
* Copyright (c) 2019-07-11 12:07:55 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.UserReplace.cs
* 版本号：  V1.0.0.0
* 唯一标识：00b2e4a4-8895-422a-90b4-afc9d91fec20
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:55 
* 描述：设置代办人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:55 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Repository
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using  UWay.Skynet.Cloud.Uflow.Entity;
   using System.Linq;
   using System.Collections.Generic;

   /// <summary>
   /// 设置代办人仓储类
   /// </summary>
   public class UserReplaceRepository:ObjectRepository
   {
      public UserReplaceRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public long Add(UserReplace  userReplace)
      {
         return Add<UserReplace>(userReplace);
      }
      /// <summary>
      /// 批量添加设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public void Add(IList<UserReplace>  userReplaces)
      {
         Batch<long, UserReplace>(userReplaces, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public int Update(UserReplace  userReplace)
      {
         return Update<UserReplace>(userReplace);
      }
      /// <summary>
      /// 删除设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<UserReplace>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      public UserReplace GetById(string id)
      {
         return GetByID<UserReplace>(id);
      }
      /// <summary>
      /// 获取所有的设置代办人{设置代办人}对象
      /// </summary>
      public IQueryable<UserReplace> Query()
      {
         return CreateQuery<UserReplace>();
      }
   }
}
