/************************************************************************************
* Copyright (c) 2019-07-11 12:02:56 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：5014755b-36ed-4676-91f1-5b3ac74fbaef
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:56 
* 描述：步骤转入转出条件结果 
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
   /// 步骤转入转出条件结果服务接口类
   /// </summary>
   public interface IHisStepConditionService
   {
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      long Add(HisStepCondition  hisStepCondition);
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      void Add(IList<HisStepCondition>  hisStepConditions);
      /// <summary>
      /// 更新步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      int Update(HisStepCondition  hisStepCondition);
      /// <summary>
      /// 删除步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      HisStepCondition GetById(string id);
      /// <summary>
      /// 获取所有的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
