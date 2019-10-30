/************************************************************************************
* Copyright (c) 2019-07-11 12:02:37 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：eacfe697-15c8-43b8-b90a-bb5e0bca011f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:37 
* 描述：转发控制 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:37 
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
   /// 转发控制服务接口类
   /// </summary>
   public interface ITemplateStepTransferService
   {
      /// <summary>
      /// 添加转发控制{转发控制}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepTransfer  templateSteptransfer);
      /// <summary>
      /// 添加转发控制{转发控制}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepTransfer>  templateSteptransfers);
      /// <summary>
      /// 更新转发控制{转发控制}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepTransfer  templateSteptransfer);
      /// <summary>
      /// 删除转发控制{转发控制}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的转发控制{转发控制}对象(即:一条记录
      /// </summary>
      TemplateStepTransfer GetById(string id);
      /// <summary>
      /// 获取所有的转发控制{转发控制}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
