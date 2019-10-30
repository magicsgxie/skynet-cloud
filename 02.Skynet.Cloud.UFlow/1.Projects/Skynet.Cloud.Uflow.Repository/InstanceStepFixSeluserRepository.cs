/************************************************************************************
* Copyright (c) 2019-07-11 12:08:30 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepFixSeluser.cs
* 版本号：  V1.0.0.0
* 唯一标识：27911d77-aabe-4a17-a6c0-7df96e10d182
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:30 
* 描述：存放由用户指定的下一步处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:30 
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
   /// 存放由用户指定的下一步处理人仓储类
   /// </summary>
   public class InstanceStepFixSeluserRepository:ObjectRepository
   {
      public InstanceStepFixSeluserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepFixSeluser  instanceStepFixSeluser)
      {
         return Add<InstanceStepFixSeluser>(instanceStepFixSeluser);
      }
      /// <summary>
      /// 批量添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepFixSeluser>  instanceStepFixSelusers)
      {
         Batch<long, InstanceStepFixSeluser>(instanceStepFixSelusers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepFixSeluser  instanceStepFixSeluser)
      {
         return Update<InstanceStepFixSeluser>(instanceStepFixSeluser);
      }
      /// <summary>
      /// 删除存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public int Delete(InstanceStepFixSeluser instanceStepFixSeluser )
      {
         return Delete<InstanceStepFixSeluser>(instanceStepFixSeluser); 
      }
      /// <summary>
      /// 获取指定的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public InstanceStepFixSeluser GetById(object id)
      {
         return GetByID<InstanceStepFixSeluser>(id);
      }
      /// <summary>
      /// 获取所有的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象
      /// </summary>
      public IQueryable<InstanceStepFixSeluser> Query()
      {
         return CreateQuery<InstanceStepFixSeluser>();
      }
   }
}
