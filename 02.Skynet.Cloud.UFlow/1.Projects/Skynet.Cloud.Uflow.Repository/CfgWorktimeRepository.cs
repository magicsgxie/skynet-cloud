/************************************************************************************
* Copyright (c) 2019-07-11 12:08:03 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.CfgWorktime.cs
* 版本号：  V1.0.0.0
* 唯一标识：66b86deb-ed1c-47cf-bc32-04e88b4da2a5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:03 
* 描述：日工作时间 
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
   /// 日工作时间仓储类
   /// </summary>
   public class CfgWorktimeRepository:ObjectRepository
   {
      public CfgWorktimeRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorktime  cfgWorktime)
      {
         return Add<CfgWorktime>(cfgWorktime);
      }
      /// <summary>
      /// 批量添加日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorktime>  cfgWorktimes)
      {
         Batch<long, CfgWorktime>(cfgWorktimes, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorktime  cfgWorktime)
      {
         return Update<CfgWorktime>(cfgWorktime);
      }
      /// <summary>
      /// 删除日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<CfgWorktime>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的日工作时间{日工作时间}对象(即:一条记录
      /// </summary>
      public CfgWorktime GetById(string id)
      {
         return GetByID<CfgWorktime>(id);
      }
      /// <summary>
      /// 获取所有的日工作时间{日工作时间}对象
      /// </summary>
      public IQueryable<CfgWorktime> Query()
      {
         return CreateQuery<CfgWorktime>();
      }
   }
}
