/************************************************************************************
* Copyright (c) 2019-07-11 12:02:47 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgWorkdayDtl.cs
* 版本号：  V1.0.0.0
* 唯一标识：83f4bbaa-8a95-4876-9393-fea5ad16b186
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:47 
* 描述：工作日设置详情 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:47 
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
   /// 工作日设置详情服务实现类
   /// </summary>
   public class CfgWorkdayDtlService: ICfgWorkdayDtlService
   {
      /// <summary>
      /// 添加工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorkdayDtl  cfgWorkdayDtl)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayDtlRepository(dbContext).Add(cfgWorkdayDtl);
         }
      }
      /// <summary>
      /// 添加工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorkdayDtl>  cfgWorkdayDtls)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new CfgWorkdayDtlRepository(dbContext).Add(cfgWorkdayDtls);
         }
      }
      /// <summary>
      /// 更新工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorkdayDtl  cfgWorkdayDtl)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayDtlRepository(dbContext).Update(cfgWorkdayDtl);
         }
      }
      /// <summary>
      /// 删除工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayDtlRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public CfgWorkdayDtl GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayDtlRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgWorkdayDtlRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
