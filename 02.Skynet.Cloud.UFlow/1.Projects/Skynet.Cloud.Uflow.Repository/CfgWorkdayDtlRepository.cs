/************************************************************************************
* Copyright (c) 2019-07-11 12:08:02 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.CfgWorkdayDtl.cs
* 版本号：  V1.0.0.0
* 唯一标识：e52e7c9f-e4ce-48de-b395-941ad6dc8ba6
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:02 
* 描述：工作日设置详情 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:02 
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
   /// 工作日设置详情仓储类
   /// </summary>
   public class CfgWorkdayDtlRepository:ObjectRepository
   {
      public CfgWorkdayDtlRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public long Add(CfgWorkdayDtl  cfgWorkdayDtl)
      {
         return Add<CfgWorkdayDtl>(cfgWorkdayDtl);
      }
      /// <summary>
      /// 批量添加工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgWorkdayDtl>  cfgWorkdayDtls)
      {
         Batch<long, CfgWorkdayDtl>(cfgWorkdayDtls, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public int Update(CfgWorkdayDtl  cfgWorkdayDtl)
      {
         return Update<CfgWorkdayDtl>(cfgWorkdayDtl);
      }
      /// <summary>
      /// 删除工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<CfgWorkdayDtl>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的工作日设置详情{工作日设置详情}对象(即:一条记录
      /// </summary>
      public CfgWorkdayDtl GetById(string id)
      {
         return GetByID<CfgWorkdayDtl>(id);
      }
      /// <summary>
      /// 获取所有的工作日设置详情{工作日设置详情}对象
      /// </summary>
      public IQueryable<CfgWorkdayDtl> Query()
      {
         return CreateQuery<CfgWorkdayDtl>();
      }
   }
}
