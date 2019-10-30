/************************************************************************************
* Copyright (c) 2019-07-11 12:08:03 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.CfgWorktimeDef.cs
* 版本号：  V1.0.0.0
* 唯一标识：4b4b9959-fdea-4917-ab53-a53e05b99eac
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:03 
* 描述：默认日工作时间 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:03 
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
   /// 默认日工作时间仓储类
   /// </summary>
   public class CfgWorktimeDefRepository:ObjectRepository
   {
      public CfgWorktimeDefRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorktimeDef  cfgWorktimeDef)
      {
         return Add<CfgWorktimeDef>(cfgWorktimeDef);
      }
      /// <summary>
      /// 批量添加默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorktimeDef>  cfgWorktimeDefs)
      {
         Batch<long, CfgWorktimeDef>(cfgWorktimeDefs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorktimeDef  cfgWorktimeDef)
      {
         return Update<CfgWorktimeDef>(cfgWorktimeDef);
      }
      /// <summary>
      /// 删除默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<CfgWorktimeDef>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的默认日工作时间{默认日工作时间}对象(即:一条记录
      /// </summary>
      public CfgWorktimeDef GetById(string id)
      {
         return GetByID<CfgWorktimeDef>(id);
      }
      /// <summary>
      /// 获取所有的默认日工作时间{默认日工作时间}对象
      /// </summary>
      public IQueryable<CfgWorktimeDef> Query()
      {
         return CreateQuery<CfgWorktimeDef>();
      }
   }
}
