/************************************************************************************
* Copyright (c) 2019-07-11 12:02:35 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：6cd55034-eb7f-439c-ba88-647a824a25ab
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:35 
* 描述：子流程 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:35 
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
   /// 子流程服务接口类
   /// </summary>
   public interface ITemplateStepSubflowService
   {
      /// <summary>
      /// 添加子流程{子流程}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepSubflow  templateStepSubflow);
      /// <summary>
      /// 添加子流程{子流程}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepSubflow>  templateStepSubflows);
      /// <summary>
      /// 更新子流程{子流程}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepSubflow  templateStepSubflow);
      /// <summary>
      /// 删除子流程{子流程}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的子流程{子流程}对象(即:一条记录
      /// </summary>
      TemplateStepSubflow GetById(string id);
      /// <summary>
      /// 获取所有的子流程{子流程}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
