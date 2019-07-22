/************************************************************************************
* Copyright (c) 2019-07-11 12:07:43 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：b1701288-9558-4655-acc5-51478438a392
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:43 
* 描述：循环处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:43 
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
   /// 循环处理仓储类
   /// </summary>
   public class TemplateStepCycleRepository:ObjectRepository
   {
      public TemplateStepCycleRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepCycle  templateStepCycle)
      {
         return Add<TemplateStepCycle>(templateStepCycle);
      }
      /// <summary>
      /// 批量添加循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepCycle>  templateStepCycles)
      {
         Batch<long, TemplateStepCycle>(templateStepCycles, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepCycle  templateStepCycle)
      {
         return Update<TemplateStepCycle>(templateStepCycle);
      }
      /// <summary>
      /// 删除循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepCycle>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public TemplateStepCycle GetById(string id)
      {
         return GetByID<TemplateStepCycle>(id);
      }
      /// <summary>
      /// 获取所有的循环处理{循环处理}对象
      /// </summary>
      public IQueryable<TemplateStepCycle> Query()
      {
         return CreateQuery<TemplateStepCycle>();
      }
   }
}
