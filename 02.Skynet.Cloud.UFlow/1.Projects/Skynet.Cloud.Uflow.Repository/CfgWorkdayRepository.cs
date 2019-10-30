/************************************************************************************
* Copyright (c) 2019-07-11 12:08:35 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.CfgWorkday.cs
* 版本号：  V1.0.0.0
* 唯一标识：517e0ee2-a384-428b-826d-7f931b6fb262
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:35 
* 描述：工作日设置 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:35 
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
   /// 工作日设置仓储类
   /// </summary>
   public class CfgWorkdayRepository:ObjectRepository
   {
      public CfgWorkdayRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorkday  cfgWorkday)
      {
         return Add<CfgWorkday>(cfgWorkday);
      }
      /// <summary>
      /// 批量添加工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorkday>  cfgWorkdays)
      {
         Batch<long, CfgWorkday>(cfgWorkdays, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorkday  cfgWorkday)
      {
         return Update<CfgWorkday>(cfgWorkday);
      }
      /// <summary>
      /// 删除工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<CfgWorkday>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的工作日设置{工作日设置}对象(即:一条记录
      /// </summary>
      public CfgWorkday GetById(string id)
      {
         return GetByID<CfgWorkday>(id);
      }
      /// <summary>
      /// 获取所有的工作日设置{工作日设置}对象
      /// </summary>
      public IQueryable<CfgWorkday> Query()
      {
         return CreateQuery<CfgWorkday>();
      }
   }
}
