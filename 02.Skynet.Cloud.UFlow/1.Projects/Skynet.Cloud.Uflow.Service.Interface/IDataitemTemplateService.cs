/************************************************************************************
* Copyright (c) 2019-07-11 12:02:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.DataitemTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：df1103b2-5b16-4928-a7c3-bd08b430c957
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:49 
* 描述：数据项模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:49 
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
   /// 数据项模板服务接口类
   /// </summary>
   public interface IDataitemTemplateService
   {
      /// <summary>
      /// 添加数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      long Add(DataitemTemplate  dataitemTemplate);
      /// <summary>
      /// 添加数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      void Add(IList<DataitemTemplate>  dataitemTemplates);
      /// <summary>
      /// 更新数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      int Update(DataitemTemplate  dataitemTemplate);
      /// <summary>
      /// 删除数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      DataitemTemplate GetById(string id);
      /// <summary>
      /// 获取所有的数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
