/************************************************************************************
* Copyright (c) 2019-07-11 12:03:10 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：691253c9-10fa-41f2-b729-a3a974d76c6d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:10 
* 描述：步骤转入转出条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:10 
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
   /// 步骤转入转出条件结果服务接口类
   /// </summary>
   public interface IInstanceStepConditionService
   {
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepCondition  instanceStepCondition);
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepCondition>  instanceStepConditions);
      /// <summary>
      /// 更新步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepCondition  instanceStepCondition);
      /// <summary>
      /// 删除步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      InstanceStepCondition GetById(string id);
      /// <summary>
      /// 获取所有的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
