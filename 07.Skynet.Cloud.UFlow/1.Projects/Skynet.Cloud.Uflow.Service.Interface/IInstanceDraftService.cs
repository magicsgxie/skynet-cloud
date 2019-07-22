/************************************************************************************
* Copyright (c) 2019-07-11 12:01:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceDraft.cs
* 版本号：  V1.0.0.0
* 唯一标识：2b700576-be97-4c1a-8a53-e350e5dbff82
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:01:49 
* 描述：发起草稿记录表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:01:49 
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
   /// 发起草稿记录表服务接口类
   /// </summary>
   public interface IInstanceDraftService
   {
      /// <summary>
      /// 添加发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      long Add(InstanceDraft  instanceDraft);
      /// <summary>
      /// 添加发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceDraft>  instanceDrafts);
      /// <summary>
      /// 更新发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      int Update(InstanceDraft  instanceDraft);
      /// <summary>
      /// 删除发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      InstanceDraft GetById(string id);
      /// <summary>
      /// 获取所有的发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
