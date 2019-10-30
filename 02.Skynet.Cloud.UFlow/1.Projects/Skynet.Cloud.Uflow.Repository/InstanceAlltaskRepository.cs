/************************************************************************************
* Copyright (c) 2019-07-11 12:08:21 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceAlltask.cs
* 版本号：  V1.0.0.0
* 唯一标识：e10fc252-d289-4dad-9988-1e31b401c927
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:21 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:21 
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
   /// 流程业务仓储类
   /// </summary>
   public class InstanceAlltaskRepository:ObjectRepository
   {
      public InstanceAlltaskRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public long Add(InstanceAlltask  instanceAlltask)
      {
         return Add<InstanceAlltask>(instanceAlltask);
      }
      /// <summary>
      /// 批量添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceAlltask>  instanceAlltasks)
      {
         Batch<long, InstanceAlltask>(instanceAlltasks, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Update(InstanceAlltask  instanceAlltask)
      {
         return Update<InstanceAlltask>(instanceAlltask);
      }
      /// <summary>
      /// 删除流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Delete(InstanceAlltask instanceAlltask )
      {
         return Delete<InstanceAlltask>(instanceAlltask); 
      }
      /// <summary>
      /// 获取指定的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public InstanceAlltask GetById(object id)
      {
         return GetByID<InstanceAlltask>(id);
      }
      /// <summary>
      /// 获取所有的流程业务{流程业务}对象
      /// </summary>
      public IQueryable<InstanceAlltask> Query()
      {
         return CreateQuery<InstanceAlltask>();
      }
   }
}
