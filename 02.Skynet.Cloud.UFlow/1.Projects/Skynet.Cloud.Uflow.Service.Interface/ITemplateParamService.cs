/************************************************************************************
* Copyright (c) 2019-07-11 12:02:27 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：24e96a6e-3561-4b21-86d2-bb244fea509b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:27 
* 描述：参数 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:27 
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
   /// 参数服务接口类
   /// </summary>
   public interface ITemplateParamService
   {
      /// <summary>
      /// 添加参数{参数}对象(即:一条记录
      /// </summary>
      long Add(TemplateParam  templateParam);
      /// <summary>
      /// 添加参数{参数}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateParam>  templateParams);
      /// <summary>
      /// 更新参数{参数}对象(即:一条记录
      /// </summary>
      int Update(TemplateParam  templateParam);
      /// <summary>
      /// 删除参数{参数}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的参数{参数}对象(即:一条记录
      /// </summary>
      TemplateParam GetById(string id);
      /// <summary>
      /// 获取所有的参数{参数}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
