/************************************************************************************
* Copyright (c) 2019-07-11 12:02:45 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：6858fcee-9d46-4b1c-9c7e-d9ec397fe109
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:45 
* 描述：子流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:45 
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
   /// 子流程表服务接口类
   /// </summary>
   public interface IHisStepSubflowService
   {
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      long Add(HisStepSubflow  hisStepSubflow);
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      void Add(IList<HisStepSubflow>  hisStepSubflows);
      /// <summary>
      /// 更新子流程表{子流程表}对象(即:一条记录
      /// </summary>
      int Update(HisStepSubflow  hisStepSubflow);
      /// <summary>
      /// 删除子流程表{子流程表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      HisStepSubflow GetById(string id);
      /// <summary>
      /// 获取所有的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
