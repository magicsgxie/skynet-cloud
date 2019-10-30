/************************************************************************************
* Copyright (c) 2019-07-11 12:03:05 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceConditionResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：d720343b-cf0c-4ad1-9c17-39625d0bbb44
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:05 
* 描述：条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:05 
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
   /// 条件结果服务接口类
   /// </summary>
   public interface IInstanceConditionResultService
   {
      /// <summary>
      /// 添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      long Add(InstanceConditionResult  instanceConditionResult);
      /// <summary>
      /// 添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceConditionResult>  instanceConditionResults);
      /// <summary>
      /// 更新条件结果{条件结果}对象(即:一条记录
      /// </summary>
      int Update(InstanceConditionResult  instanceConditionResult);
      /// <summary>
      /// 删除条件结果{条件结果}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的条件结果{条件结果}对象(即:一条记录
      /// </summary>
      InstanceConditionResult GetById(string id);
      /// <summary>
      /// 获取所有的条件结果{条件结果}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
