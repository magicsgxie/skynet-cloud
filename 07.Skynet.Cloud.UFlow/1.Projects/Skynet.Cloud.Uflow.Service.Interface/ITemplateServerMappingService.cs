/************************************************************************************
* Copyright (c) 2019-07-11 12:02:28 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateServerMapping.cs
* 版本号：  V1.0.0.0
* 唯一标识：714da85c-42a0-45e5-bb4e-6b1be30222fa
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:28 
* 描述：模版与引擎服务器映射关系表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:28 
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
   /// 模版与引擎服务器映射关系表服务接口类
   /// </summary>
   public interface ITemplateServerMappingService
   {
      /// <summary>
      /// 添加模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      long Add(TemplateServerMapping  templateServerMapping);
      /// <summary>
      /// 添加模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateServerMapping>  templateServerMappings);
      /// <summary>
      /// 更新模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      int Update(TemplateServerMapping  templateServerMapping);
      /// <summary>
      /// 删除模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      int Delete(int[] idArrays );
      /// <summary>
      /// 获取指定的模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      TemplateServerMapping GetById(int id);
      /// <summary>
      /// 获取所有的模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
