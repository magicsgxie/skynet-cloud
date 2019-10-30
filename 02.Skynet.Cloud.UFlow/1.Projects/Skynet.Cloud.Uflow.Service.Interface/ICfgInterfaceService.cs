/************************************************************************************
* Copyright (c) 2019-07-11 12:03:18 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgInterface.cs
* 版本号：  V1.0.0.0
* 唯一标识：3b4cc0c8-1d0d-4868-8fc9-f35e6cb2ff24
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:18 
* 描述：接口配置 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:18 
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
   /// 接口配置服务接口类
   /// </summary>
   public interface ICfgInterfaceService
   {
      /// <summary>
      /// 添加接口配置{接口配置}对象(即:一条记录
      /// </summary>
      long Add(CfgInterface  cfgInterface);
      /// <summary>
      /// 添加接口配置{接口配置}对象(即:一条记录
      /// </summary>
      void Add(IList<CfgInterface>  cfgInterfaces);
      /// <summary>
      /// 更新接口配置{接口配置}对象(即:一条记录
      /// </summary>
      int Update(CfgInterface  cfgInterface);
      /// <summary>
      /// 删除接口配置{接口配置}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的接口配置{接口配置}对象(即:一条记录
      /// </summary>
      CfgInterface GetById(string id);
      /// <summary>
      /// 获取所有的接口配置{接口配置}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
