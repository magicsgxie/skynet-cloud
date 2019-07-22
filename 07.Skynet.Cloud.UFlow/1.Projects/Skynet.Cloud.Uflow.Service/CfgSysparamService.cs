/************************************************************************************
* Copyright (c) 2019-07-11 12:03:17 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.CfgSysparam.cs
* 版本号：  V1.0.0.0
* 唯一标识：a9ff63af-e9df-4814-8e5c-4ea9fd8942df
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:17 
* 描述：系统参数 
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
   /// 系统参数服务实现类
   /// </summary>
   public class CfgSysparamService: ICfgSysparamService
   {
      /// <summary>
      /// 添加系统参数{系统参数}对象(即:一条记录
      /// </summary>
      public long Add(CfgSysparam  cfgSysparam)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgSysparamRepository(dbContext).Add(cfgSysparam);
         }
      }
      /// <summary>
      /// 添加系统参数{系统参数}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgSysparam>  cfgSysparams)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new CfgSysparamRepository(dbContext).Add(cfgSysparams);
         }
      }
      /// <summary>
      /// 更新系统参数{系统参数}对象(即:一条记录
      /// </summary>
      public int Update(CfgSysparam  cfgSysparam)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgSysparamRepository(dbContext).Update(cfgSysparam);
         }
      }
      /// <summary>
      /// 删除系统参数{系统参数}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgSysparamRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的系统参数{系统参数}对象(即:一条记录
      /// </summary>
      public CfgSysparam GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgSysparamRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的系统参数{系统参数}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new CfgSysparamRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
