/************************************************************************************
* Copyright (c) 2019-07-11 12:02:53 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisEventResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：2fd713c1-adeb-49fa-90ca-3984813436db
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:53 
* 描述：实例事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:53 
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
   /// 实例事件结果服务接口类
   /// </summary>
   public interface IHisEventResultService
   {
      /// <summary>
      /// 添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      long Add(HisEventResult  hisEventResult);
      /// <summary>
      /// 添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      void Add(IList<HisEventResult>  hisEventResults);
      /// <summary>
      /// 更新实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      int Update(HisEventResult  hisEventResult);
      /// <summary>
      /// 删除实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      HisEventResult GetById(string id);
      /// <summary>
      /// 获取所有的实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
