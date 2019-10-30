/************************************************************************************
* Copyright (c) 2019-07-11 12:03:06 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceDataitemResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：3919cc3a-6863-4ad2-b932-77fc61538413
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:06 
* 描述：数据项结果表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:06 
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
   /// 数据项结果表服务接口类
   /// </summary>
   public interface IInstanceDataitemResultService
   {
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      long Add(InstanceDataitemResult  instanceDataitemResult);
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceDataitemResult>  instanceDataitemResults);
      /// <summary>
      /// 更新数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      int Update(InstanceDataitemResult  instanceDataitemResult);
      /// <summary>
      /// 删除数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      InstanceDataitemResult GetById(string id);
      /// <summary>
      /// 获取所有的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
