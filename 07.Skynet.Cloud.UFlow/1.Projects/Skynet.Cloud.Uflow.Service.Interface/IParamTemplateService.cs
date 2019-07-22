/************************************************************************************
* Copyright (c) 2019-07-11 12:03:01 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.ParamTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：8d9c0b45-c2c6-4785-a0a7-e1a1ed777739
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:01 
* 描述：参数模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:01 
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
   /// 参数模板服务接口类
   /// </summary>
   public interface IParamTemplateService
   {
      /// <summary>
      /// 添加参数模板{参数模板}对象(即:一条记录
      /// </summary>
      long Add(ParamTemplate  paramTemplate);
      /// <summary>
      /// 添加参数模板{参数模板}对象(即:一条记录
      /// </summary>
      void Add(IList<ParamTemplate>  paramTemplates);
      /// <summary>
      /// 更新参数模板{参数模板}对象(即:一条记录
      /// </summary>
      int Update(ParamTemplate  paramTemplate);
      /// <summary>
      /// 删除参数模板{参数模板}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的参数模板{参数模板}对象(即:一条记录
      /// </summary>
      ParamTemplate GetById(string id);
      /// <summary>
      /// 获取所有的参数模板{参数模板}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
