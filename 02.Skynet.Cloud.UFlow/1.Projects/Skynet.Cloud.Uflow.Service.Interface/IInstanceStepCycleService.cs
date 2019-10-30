/************************************************************************************
* Copyright (c) 2019-07-11 12:03:11 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：4196b4bd-8776-4faf-9b66-966f8032640c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:11 
* 描述：存放每一次循环处理的结果，把最后的结果更新到事件表中 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:11 
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
   /// 存放每一次循环处理的结果，把最后的结果更新到事件表中服务接口类
   /// </summary>
   public interface IInstanceStepCycleService
   {
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepCycle  instanceStepCycle);
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepCycle>  instanceStepCycles);
      /// <summary>
      /// 更新存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepCycle  instanceStepCycle);
      /// <summary>
      /// 删除存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      InstanceStepCycle GetById(string id);
      /// <summary>
      /// 获取所有的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
