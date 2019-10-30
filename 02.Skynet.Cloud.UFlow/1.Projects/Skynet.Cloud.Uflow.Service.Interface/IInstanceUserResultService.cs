/************************************************************************************
* Copyright (c) 2019-07-11 12:02:45 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceUserResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：009ac17c-20a4-416f-ac7e-84af614030e7
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:45 
* 描述：人员处理结果表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:45 
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
   /// 人员处理结果表服务接口类
   /// </summary>
   public interface IInstanceUserResultService
   {
      /// <summary>
      /// 添加人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      long Add(InstanceUserResult  instanceUserResult);
      /// <summary>
      /// 添加人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceUserResult>  instanceUserResults);
      /// <summary>
      /// 更新人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      int Update(InstanceUserResult  instanceUserResult);
      /// <summary>
      /// 删除人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      InstanceUserResult GetById(string id);
      /// <summary>
      /// 获取所有的人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
