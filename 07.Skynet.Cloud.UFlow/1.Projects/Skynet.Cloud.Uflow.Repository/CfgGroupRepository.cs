/************************************************************************************
* Copyright (c) 2019-07-11 12:08:37 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.CfgGroup.cs
* 版本号：  V1.0.0.0
* 唯一标识：d42129be-e364-4a08-bb03-409b19b13bad
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:37 
* 描述：工作组织 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:37 
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
   /// 工作组织仓储类
   /// </summary>
   public class CfgGroupRepository:ObjectRepository
   {
      public CfgGroupRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public long Add(CfgGroup  cfgGroup)
      {
         return Add<CfgGroup>(cfgGroup);
      }
      /// <summary>
      /// 批量添加工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public void Add(IList<CfgGroup>  cfgGroups)
      {
         Batch<long, CfgGroup>(cfgGroups, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public int Update(CfgGroup  cfgGroup)
      {
         return Update<CfgGroup>(cfgGroup);
      }
      /// <summary>
      /// 删除工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<CfgGroup>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的工作组织{工作组织}对象(即:一条记录
      /// </summary>
      public CfgGroup GetById(string id)
      {
         return GetByID<CfgGroup>(id);
      }
      /// <summary>
      /// 获取所有的工作组织{工作组织}对象
      /// </summary>
      public IQueryable<CfgGroup> Query()
      {
         return CreateQuery<CfgGroup>();
      }
   }
}
