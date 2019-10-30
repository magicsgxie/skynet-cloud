/************************************************************************************
* Copyright (c) 2019-07-11 12:02:40 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateType.cs
* 版本号：  V1.0.0.0
* 唯一标识：e442f579-22a3-4a3e-bf58-5897cb5d363b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:40 
* 描述：流程分类 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:40 
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
   /// 流程分类服务接口类
   /// </summary>
   public interface ITemplateTypeService
   {
      /// <summary>
      /// 添加流程分类{流程分类}对象(即:一条记录
      /// </summary>
      long Add(TemplateType  templatetype);
      /// <summary>
      /// 添加流程分类{流程分类}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateType>  templatetypes);
      /// <summary>
      /// 更新流程分类{流程分类}对象(即:一条记录
      /// </summary>
      int Update(TemplateType  templatetype);
      /// <summary>
      /// 删除流程分类{流程分类}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的流程分类{流程分类}对象(即:一条记录
      /// </summary>
      TemplateType GetById(string id);
      /// <summary>
      /// 获取所有的流程分类{流程分类}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
