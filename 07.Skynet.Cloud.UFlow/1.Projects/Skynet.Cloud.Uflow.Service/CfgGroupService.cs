/************************************************************************************
* Copyright (c) 2019-07-11 12:03:19 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgGroup.cs
* 版本号：  V1.0.0.0
* 唯一标识：fb1fb122-dd83-4bab-80ff-5c9611a8540e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:19 
* 描述：工作组织 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:19 
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
   /// 工作组织服务实现类
   /// </summary>
   public class CfgGroupService: ICfgGroupService
   {
      /// <summary>
      /// 添加工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public long Add(CfgGroup  cfgGroup)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgGroupRepository(dbContext).Add(cfgGroup);
         }
      }
      /// <summary>
      /// 添加工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgGroup>  cfgGroups)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new CfgGroupRepository(dbContext).Add(cfgGroups);
         }
      }
      /// <summary>
      /// 更新工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public int Update(CfgGroup  cfgGroup)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgGroupRepository(dbContext).Update(cfgGroup);
         }
      }
      /// <summary>
      /// 删除工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgGroupRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public CfgGroup GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgGroupRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgGroupRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
