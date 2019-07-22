/************************************************************************************
* Copyright (c) 2019-07-11 12:03:17 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgWorkday.cs
* 版本号：  V1.0.0.0
* 唯一标识：70a9f9a7-b1d8-4e69-a54d-fdff7dd4b935
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
   /// 工作日设置服务实现类
   /// </summary>
   public class CfgWorkdayService: ICfgWorkdayService
   {
      /// <summary>
      /// 添加工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorkday  cfgWorkday)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayRepository(dbContext).Add(cfgWorkday);
         }
      }
      /// <summary>
      /// 添加工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorkday>  cfgWorkdays)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new CfgWorkdayRepository(dbContext).Add(cfgWorkdays);
         }
      }
      /// <summary>
      /// 更新工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorkday  cfgWorkday)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayRepository(dbContext).Update(cfgWorkday);
         }
      }
      /// <summary>
      /// 删除工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public CfgWorkday GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
