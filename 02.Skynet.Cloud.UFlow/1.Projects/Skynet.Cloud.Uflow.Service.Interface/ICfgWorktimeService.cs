/************************************************************************************
* Copyright (c) 2019-07-11 12:02:48 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgWorktime.cs
* 版本号：  V1.0.0.0
* 唯一标识：d3d150ae-f36c-4629-98c0-c4e4eb8a027e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:48 
* 描述：日工作时间 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:48 
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
   /// 日工作时间服务接口类
   /// </summary>
   public interface ICfgWorktimeService
   {
      /// <summary>
      /// 添加日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      long Add(CfgWorktime  cfgWorktime);
      /// <summary>
      /// 添加日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      void Add(IList<CfgWorktime>  cfgWorktimes);
      /// <summary>
      /// 更新日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      int Update(CfgWorktime  cfgWorktime);
      /// <summary>
      /// 删除日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      CfgWorktime GetById(string id);
      /// <summary>
      /// 获取所有的日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
