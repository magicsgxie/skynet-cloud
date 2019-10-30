/************************************************************************************
* Copyright (c) 2019-07-11 12:07:46 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：55f918b7-9ddc-4ce6-81bc-9d64c9bef6c2
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:46 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:46 
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
   public class TemplateStepMsgRepository:ObjectRepository
   {
      public TemplateStepMsgRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepMsg  templateStepMsg)
      {
         return Add<TemplateStepMsg>(templateStepMsg);
      }
      /// <summary>
      /// 批量添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepMsg>  templateStepMsgs)
      {
         Batch<long, TemplateStepMsg>(templateStepMsgs, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepMsg  templateStepMsg)
      {
         return Update<TemplateStepMsg>(templateStepMsg);
      }
      /// <summary>
      /// 删除流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepMsg>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public TemplateStepMsg GetById(string id)
      {
         return GetByID<TemplateStepMsg>(id);
      }
      /// <summary>
      /// 获取所有的流程业务{流程业务}对象
      /// </summary>
      public IQueryable<TemplateStepMsg> Query()
      {
         return CreateQuery<TemplateStepMsg>();
      }
   }
}
