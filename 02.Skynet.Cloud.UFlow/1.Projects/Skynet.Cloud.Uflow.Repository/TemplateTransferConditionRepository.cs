/************************************************************************************
* Copyright (c) 2019-07-11 12:07:51 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateTransferCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：c51cec10-5172-4c91-8a6b-264582b87781
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:51 
* 描述：转发条件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:51 
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
   /// 转发条件仓储类
   /// </summary>
   public class TemplateTransferConditionRepository:ObjectRepository
   {
      public TemplateTransferConditionRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public long Add(TemplateTransferCondition  templatetransferCondition)
      {
         return Add<TemplateTransferCondition>(templatetransferCondition);
      }
      /// <summary>
      /// 批量添加转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateTransferCondition>  templatetransferConditions)
      {
         Batch<long, TemplateTransferCondition>(templatetransferConditions, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public int Update(TemplateTransferCondition  templatetransferCondition)
      {
         return Update<TemplateTransferCondition>(templatetransferCondition);
      }
      /// <summary>
      /// 删除转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateTransferCondition>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的转发条件{转发条件}对象(即:一条记录
      /// </summary>
      public TemplateTransferCondition GetById(string id)
      {
         return GetByID<TemplateTransferCondition>(id);
      }
      /// <summary>
      /// 获取所有的转发条件{转发条件}对象
      /// </summary>
      public IQueryable<TemplateTransferCondition> Query()
      {
         return CreateQuery<TemplateTransferCondition>();
      }
   }
}
