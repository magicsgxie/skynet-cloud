/************************************************************************************
* Copyright (c) 2019-07-11 12:02:33 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepFixstepList.cs
* 版本号：  V1.0.0.0
* 唯一标识：1bcd7fd4-9f8b-4ecb-8d37-bac75b8f9498
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:33 
* 描述：可选下一步骤列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:33 
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
   /// 可选下一步骤列表服务接口类
   /// </summary>
   public interface ITemplateStepFixstepListService
   {
      /// <summary>
      /// 添加可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepFixstepList  templateStepFixstepList);
      /// <summary>
      /// 添加可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepFixstepList>  templateStepFixstepLists);
      /// <summary>
      /// 更新可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepFixstepList  templateStepFixstepList);
      /// <summary>
      /// 删除可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      TemplateStepFixstepList GetById(string id);
      /// <summary>
      /// 获取所有的可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
