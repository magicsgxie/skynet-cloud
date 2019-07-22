/************************************************************************************
* Copyright (c) 2019-07-11 12:02:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateDataitem.cs
* 版本号：  V1.0.0.0
* 唯一标识：e35b1098-dac9-438b-ad5a-254ce3a706d5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:44 
* 描述：数据项 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:44 
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
   /// 数据项服务接口类
   /// </summary>
   public interface ITemplateDataitemService
   {
      /// <summary>
      /// 添加数据项{数据项}对象(即:一条记录
      /// </summary>
      long Add(TemplateDataitem  templateDataitem);
      /// <summary>
      /// 添加数据项{数据项}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateDataitem>  templateDataitems);
      /// <summary>
      /// 更新数据项{数据项}对象(即:一条记录
      /// </summary>
      int Update(TemplateDataitem  templateDataitem);
      /// <summary>
      /// 删除数据项{数据项}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的数据项{数据项}对象(即:一条记录
      /// </summary>
      TemplateDataitem GetById(string id);
      /// <summary>
      /// 获取所有的数据项{数据项}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
