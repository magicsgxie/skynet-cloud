/************************************************************************************
* Copyright (c) 2019-07-11 12:02:41 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.UserReplace.cs
* 版本号：  V1.0.0.0
* 唯一标识：d219addd-a0ff-424d-a11b-167b15f3ed35
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



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 设置代办人服务接口类
   /// </summary>
   public interface IUserReplaceService
   {
      /// <summary>
      /// 添加设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      long Add(UserReplace  userReplace);
      /// <summary>
      /// 添加设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      void Add(IList<UserReplace>  userReplaces);
      /// <summary>
      /// 更新设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      int Update(UserReplace  userReplace);
      /// <summary>
      /// 删除设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      UserReplace GetById(string id);
      /// <summary>
      /// 获取所有的设置代办人{设置代办人}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
