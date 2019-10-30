/************************************************************************************
* Copyright (c) 2019-07-11 12:08:26 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStep.cs
* 版本号：  V1.0.0.0
* 唯一标识：dd865b90-b91a-4794-b942-fb4bab79e65d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:26 
* 描述：实例步骤表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:26 
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
   /// 实例步骤表仓储类
   /// </summary>
   public class InstanceStepRepository:ObjectRepository
   {
      public InstanceStepRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStep  instanceStep)
      {
         return Add<InstanceStep>(instanceStep);
      }
      /// <summary>
      /// 批量添加实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStep>  instanceSteps)
      {
         Batch<long, InstanceStep>(instanceSteps, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStep  instanceStep)
      {
         return Update<InstanceStep>(instanceStep);
      }
      /// <summary>
      /// 删除实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStep>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的实例步骤表{实例步骤表}对象(即:一条记录
      /// </summary>
      public InstanceStep GetById(string id)
      {
         return GetByID<InstanceStep>(id);
      }
      /// <summary>
      /// 获取所有的实例步骤表{实例步骤表}对象
      /// </summary>
      public IQueryable<InstanceStep> Query()
      {
         return CreateQuery<InstanceStep>();
      }
   }
}
