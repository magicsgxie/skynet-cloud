/************************************************************************************
* Copyright (c) 2019-07-11 12:03:17 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgWorkday.cs
* 版本号：  V1.0.0.0
* 唯一标识：b29db603-9414-436f-8f20-3f0cd3de6772
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:17 
* 描述：工作日设置 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:17 
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
   /// 工作日设置服务接口类
   /// </summary>
   public interface ICfgWorkdayService
   {
      /// <summary>
      /// 添加工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      long Add(CfgWorkday  cfgWorkday);
      /// <summary>
      /// 添加工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      void Add(IList<CfgWorkday>  cfgWorkdays);
      /// <summary>
      /// 更新工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      int Update(CfgWorkday  cfgWorkday);
      /// <summary>
      /// 删除工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      CfgWorkday GetById(string id);
      /// <summary>
      /// 获取所有的工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
