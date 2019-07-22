/************************************************************************************
* Copyright (c) 2019-07-11 12:01:46 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：28f9266f-fb85-4fae-9cc9-9d6a11f4d2f6
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:01:46 
* 描述：实例步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:01:46 
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
   /// 实例步骤处理人服务接口类
   /// </summary>
   public interface IInstanceStepUserService
   {
      /// <summary>
      /// 添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepUser  instanceStepUser);
      /// <summary>
      /// 添加实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepUser>  instanceStepUsers);
      /// <summary>
      /// 更新实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepUser  instanceStepUser);
      /// <summary>
      /// 删除实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      InstanceStepUser GetById(string id);
      /// <summary>
      /// 获取所有的实例步骤处理人{实例步骤处理人}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
