/************************************************************************************
* Copyright (c) 2019-07-11 12:08:09 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：c773273b-7764-4ba6-93a3-a52a0aa5924f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:09 
* 描述：流程实例表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:09 
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
   /// 流程实例表仓储类
   /// </summary>
   public class HisFlowRepository:ObjectRepository
   {
      public HisFlowRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public long Add(HisFlow  hisFlow)
      {
         return Add<HisFlow>(hisFlow);
      }
      /// <summary>
      /// 批量添加流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisFlow>  hisFlows)
      {
         Batch<long, HisFlow>(hisFlows, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public int Update(HisFlow  hisFlow)
      {
         return Update<HisFlow>(hisFlow);
      }
      /// <summary>
      /// 删除流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisFlow>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程实例表{流程实例表}对象(即:一条记录
      /// </summary>
      public HisFlow GetById(string id)
      {
         return GetByID<HisFlow>(id);
      }
      /// <summary>
      /// 获取所有的流程实例表{流程实例表}对象
      /// </summary>
      public IQueryable<HisFlow> Query()
      {
         return CreateQuery<HisFlow>();
      }
   }
}
