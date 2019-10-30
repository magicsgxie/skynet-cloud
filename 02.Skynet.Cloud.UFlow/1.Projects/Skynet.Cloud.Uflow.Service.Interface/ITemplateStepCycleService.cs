/************************************************************************************
* Copyright (c) 2019-07-11 12:02:30 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：40498000-ae33-4ebf-9e70-5bf9ee2b8a0e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:30 
* 描述：循环处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:30 
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
   /// 循环处理服务接口类
   /// </summary>
   public interface ITemplateStepCycleService
   {
      /// <summary>
      /// 添加循环处理{循环处理}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepCycle  templateStepCycle);
      /// <summary>
      /// 添加循环处理{循环处理}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepCycle>  templateStepCycles);
      /// <summary>
      /// 更新循环处理{循环处理}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepCycle  templateStepCycle);
      /// <summary>
      /// 删除循环处理{循环处理}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的循环处理{循环处理}对象(即:一条记录
      /// </summary>
      TemplateStepCycle GetById(string id);
      /// <summary>
      /// 获取所有的循环处理{循环处理}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
