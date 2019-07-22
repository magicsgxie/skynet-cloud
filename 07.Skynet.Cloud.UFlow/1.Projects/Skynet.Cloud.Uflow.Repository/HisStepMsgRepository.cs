/************************************************************************************
* Copyright (c) 2019-07-11 12:08:15 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：7be1287a-3f5e-40fe-93b0-e62622085a98
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:15 
* 描述：步骤消息处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:15 
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
   /// 步骤消息处理仓储类
   /// </summary>
   public class HisStepMsgRepository:ObjectRepository
   {
      public HisStepMsgRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public long Add(HisStepMsg  hisStepMsg)
      {
         return Add<HisStepMsg>(hisStepMsg);
      }
      /// <summary>
      /// 批量添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepMsg>  hisStepMsgs)
      {
         Batch<long, HisStepMsg>(hisStepMsgs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Update(HisStepMsg  hisStepMsg)
      {
         return Update<HisStepMsg>(hisStepMsg);
      }
      /// <summary>
      /// 删除步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepMsg>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public HisStepMsg GetById(string id)
      {
         return GetByID<HisStepMsg>(id);
      }
      /// <summary>
      /// 获取所有的步骤消息处理{步骤消息处理}对象
      /// </summary>
      public IQueryable<HisStepMsg> Query()
      {
         return CreateQuery<HisStepMsg>();
      }
   }
}
