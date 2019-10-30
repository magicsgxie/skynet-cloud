/************************************************************************************
* Copyright (c) 2019-07-11 12:02:48 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgWorktimeDef.cs
* 版本号：  V1.0.0.0
* 唯一标识：3ac40f0b-6ff7-46f3-984a-7b6780e288bb
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:48 
* 描述：默认日工作时间 
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



namespace   UWay.Skynet.Cloud.Uflow.Service
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;

   using System.Linq;
   using UWay.Skynet.Cloud.Uflow.Service.Interface;
   using UWay.Skynet.Cloud.Uflow.Repository;
   using UWay.Skynet.Cloud.Linq;
   using UWay.Skynet.Cloud;

   /// <summary>
   /// 默认日工作时间服务实现类
   /// </summary>
   public class CfgWorktimeDefService: ICfgWorktimeDefService
   {
      /// <summary>
      /// 添加默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorktimeDef  cfgWorktimeDef)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeDefRepository(dbContext).Add(cfgWorktimeDef);
         }
      }
      /// <summary>
      /// 添加默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorktimeDef>  cfgWorktimeDefs)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new CfgWorktimeDefRepository(dbContext).Add(cfgWorktimeDefs);
         }
      }
      /// <summary>
      /// 更新默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorktimeDef  cfgWorktimeDef)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeDefRepository(dbContext).Update(cfgWorktimeDef);
         }
      }
      /// <summary>
      /// 删除默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeDefRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public CfgWorktimeDef GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeDefRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeDefRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
