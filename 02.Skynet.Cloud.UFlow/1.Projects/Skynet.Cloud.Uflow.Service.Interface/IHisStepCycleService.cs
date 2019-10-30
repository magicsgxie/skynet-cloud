/************************************************************************************
* Copyright (c) 2019-07-11 12:02:56 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：c265e46c-ace5-42bd-95c7-b43dc67b1cc0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:56 
* 描述：存放每一次循环处理的结果，把最后的结果更新到事件表中 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:56 
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
   public interface IHisStepCycleService
   {
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      long Add(HisStepCycle  hisStepCycle);
      /// <summary>
      /// 添加存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      void Add(IList<HisStepCycle>  hisStepCycles);
      /// <summary>
      /// 更新存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      int Update(HisStepCycle  hisStepCycle);
      /// <summary>
      /// 删除存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      HisStepCycle GetById(string id);
      /// <summary>
      /// 获取所有的存放每一次循环处理的结果，把最后的结果更新到事件表中{存放每一次循环处理的结果，把最后的结果更新到事件表中}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
