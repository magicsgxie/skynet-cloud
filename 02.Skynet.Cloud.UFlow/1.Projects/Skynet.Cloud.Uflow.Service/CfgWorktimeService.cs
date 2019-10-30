/************************************************************************************
* Copyright (c) 2019-07-11 12:02:48 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.CfgWorktimeService.cs
* 版本号：  V1.0.0.0
* 唯一标识：ae55e50c-89c3-44bb-930f-45c11b2c4ae2
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
   /// 日工作时间服务实现类
   /// </summary>
   public class CfgWorktimeService: ICfgWorktimeService
   {
      /// <summary>
      /// 添加日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorktime  cfgWorktime)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeRepository(dbContext).Add(cfgWorktime);
         }
      }
      /// <summary>
      /// 添加日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorktime>  cfgWorktimes)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new CfgWorktimeRepository(dbContext).Add(cfgWorktimes);
         }
      }
      /// <summary>
      /// 更新日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorktime  cfgWorktime)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeRepository(dbContext).Update(cfgWorktime);
         }
      }
      /// <summary>
      /// 删除日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public CfgWorktime GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorktimeRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
