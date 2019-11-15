/************************************************************************************
* Copyright (c) 2019-07-11 12:08:30 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.InstanceStepFixstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：d897b956-dcb2-42dd-883b-5ffe5940af51
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:30 
* 描述：存放用户指定的下一个处理步骤。 
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
   /// 存放用户指定的下一个处理步骤。仓储类
   /// </summary>
   public class InstanceStepFixstepRepository:ObjectRepository
   {
      public InstanceStepFixstepRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepFixstep  instanceStepFixstep)
      {
         return Add<InstanceStepFixstep>(instanceStepFixstep);
      }
      /// <summary>
      /// 批量添加存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepFixstep>  instanceStepFixsteps)
      {
         Batch<long, InstanceStepFixstep>(instanceStepFixsteps, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepFixstep  instanceStepFixstep)
      {
         return Update<InstanceStepFixstep>(instanceStepFixstep);
      }
      /// <summary>
      /// 删除存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<InstanceStepFixstep>(p => idArrays.Contains(p.Fid)); 
      }

        public int DeleteByInstanceStepId(string id)
        {
            return Delete<InstanceStepFixstep>(p => p.InstanceStepId.Equals(id));
        }
      /// <summary>
      /// 获取指定的存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
        public InstanceStepFixstep GetById(string id)
      {
         return GetByID<InstanceStepFixstep>(id);
      }
      /// <summary>
      /// 获取所有的存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象
      /// </summary>
      public IQueryable<InstanceStepFixstep> Query()
      {
         return CreateQuery<InstanceStepFixstep>();
      }
   }
}
