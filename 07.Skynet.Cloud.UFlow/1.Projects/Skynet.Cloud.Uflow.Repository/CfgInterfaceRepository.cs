/************************************************************************************
* Copyright (c) 2019-07-11 12:08:36 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.CfgInterface.cs
* 版本号：  V1.0.0.0
* 唯一标识：45871964-9b37-4946-b246-941784e2f282
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:36 
* 描述：接口配置 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:36 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Repository
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using  UWay.Skynet.Cloud.Uflow.Entity;
   using System.Linq;
   using System.Collections.Generic;

   /// <summary>
   /// 接口配置仓储类
   /// </summary>
   public class CfgInterfaceRepository:ObjectRepository
   {
      public CfgInterfaceRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加接口配置{接口配置}对象(即:一条记录
      /// </summary>
      public long Add(CfgInterface  cfgInterface)
      {
         return Add<CfgInterface>(cfgInterface);
      }
      /// <summary>
      /// 批量添加接口配置{接口配置}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgInterface>  cfgInterfaces)
      {
         Batch<long, CfgInterface>(cfgInterfaces, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新接口配置{接口配置}对象(即:一条记录
      /// </summary>
      public int Update(CfgInterface  cfgInterface)
      {
         return Update<CfgInterface>(cfgInterface);
      }
      /// <summary>
      /// 删除接口配置{接口配置}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<CfgInterface>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的接口配置{接口配置}对象(即:一条记录
      /// </summary>
      public CfgInterface GetById(string id)
      {
         return GetByID<CfgInterface>(id);
      }
      /// <summary>
      /// 获取所有的接口配置{接口配置}对象
      /// </summary>
      public IQueryable<CfgInterface> Query()
      {
         return CreateQuery<CfgInterface>();
      }
   }
}
