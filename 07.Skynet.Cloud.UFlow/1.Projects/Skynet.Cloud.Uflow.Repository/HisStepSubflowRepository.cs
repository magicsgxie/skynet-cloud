/************************************************************************************
* Copyright (c) 2019-07-11 12:08:00 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：39668c27-363e-42c7-a297-265b444abff3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:00 
* 描述：子流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:00 
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
   /// 子流程表仓储类
   /// </summary>
   public class HisStepSubflowRepository:ObjectRepository
   {
      public HisStepSubflowRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public long Add(HisStepSubflow  hisStepSubflow)
      {
         return Add<HisStepSubflow>(hisStepSubflow);
      }
      /// <summary>
      /// 批量添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepSubflow>  hisStepSubflows)
      {
         Batch<long, HisStepSubflow>(hisStepSubflows, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public int Update(HisStepSubflow  hisStepSubflow)
      {
         return Update<HisStepSubflow>(hisStepSubflow);
      }
      /// <summary>
      /// 删除子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepSubflow>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public HisStepSubflow GetById(string id)
      {
         return GetByID<HisStepSubflow>(id);
      }
      /// <summary>
      /// 获取所有的子流程表{子流程表}对象
      /// </summary>
      public IQueryable<HisStepSubflow> Query()
      {
         return CreateQuery<HisStepSubflow>();
      }
   }
}
