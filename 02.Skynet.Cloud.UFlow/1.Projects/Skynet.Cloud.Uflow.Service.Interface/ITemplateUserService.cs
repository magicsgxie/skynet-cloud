/************************************************************************************
* Copyright (c) 2019-07-11 12:02:40 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：d88a3b4e-c446-40d9-ad20-60efe24fd682
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



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 人员服务接口类
   /// </summary>
   public interface ITemplateUserService
   {
      /// <summary>
      /// 添加人员{人员}对象(即:一条记录
      /// </summary>
      long Add(TemplateUser  templateUser);
      /// <summary>
      /// 添加人员{人员}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateUser>  templateUsers);
      /// <summary>
      /// 更新人员{人员}对象(即:一条记录
      /// </summary>
      int Update(TemplateUser  templateUser);
      /// <summary>
      /// 删除人员{人员}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的人员{人员}对象(即:一条记录
      /// </summary>
      TemplateUser GetById(string id);
      /// <summary>
      /// 获取所有的人员{人员}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
