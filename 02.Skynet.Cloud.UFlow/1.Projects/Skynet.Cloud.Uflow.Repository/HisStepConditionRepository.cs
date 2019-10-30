/************************************************************************************
* Copyright (c) 2019-07-11 12:08:11 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：109eda25-59a5-4484-ad0e-e6727ae0f024
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:11 
* 描述：步骤转入转出条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:11 
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
   /// 步骤转入转出条件结果仓储类
   /// </summary>
   public class HisStepConditionRepository:ObjectRepository
   {
      public HisStepConditionRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisStepCondition  hisStepCondition)
      {
         return Add<HisStepCondition>(hisStepCondition);
      }
      /// <summary>
      /// 批量添加步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepCondition>  hisStepConditions)
      {
         Batch<long, HisStepCondition>(hisStepConditions, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisStepCondition  hisStepCondition)
      {
         return Update<HisStepCondition>(hisStepCondition);
      }
      /// <summary>
      /// 删除步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepCondition>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的步骤转入转出条件结果{步骤转入转出条件结果}对象(即:一条记录
      /// </summary>
      public HisStepCondition GetById(string id)
      {
         return GetByID<HisStepCondition>(id);
      }
      /// <summary>
      /// 获取所有的步骤转入转出条件结果{步骤转入转出条件结果}对象
      /// </summary>
      public IQueryable<HisStepCondition> Query()
      {
         return CreateQuery<HisStepCondition>();
      }
   }
}
