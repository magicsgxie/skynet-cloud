/************************************************************************************
* Copyright (c) 2019-07-11 12:03:03 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisSuspend.cs
* 版本号：  V1.0.0.0
* 唯一标识：c1553122-2ec3-483f-8377-cdd9f4a6b150
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:03 
* 描述：实例挂起记录 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:03 
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
   /// 实例挂起记录服务接口类
   /// </summary>
   public interface IHisSuspendService
   {
      /// <summary>
      /// 添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      long Add(HisSuspend  hisSuspend);
      /// <summary>
      /// 添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      void Add(IList<HisSuspend>  hisSuspends);
      /// <summary>
      /// 更新实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      int Update(HisSuspend  hisSuspend);
      /// <summary>
      /// 删除实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      HisSuspend GetById(string id);
      /// <summary>
      /// 获取所有的实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
