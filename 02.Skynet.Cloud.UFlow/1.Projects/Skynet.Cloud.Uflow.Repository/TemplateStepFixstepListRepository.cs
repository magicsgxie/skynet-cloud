/************************************************************************************
* Copyright (c) 2019-07-11 12:07:45 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepFixstepList.cs
* 版本号：  V1.0.0.0
* 唯一标识：832322e5-0c58-448b-b271-1121e4081013
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:45 
* 描述：可选下一步骤列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:45 
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
   /// 可选下一步骤列表仓储类
   /// </summary>
   public class TemplateStepFixstepListRepository:ObjectRepository
   {
      public TemplateStepFixstepListRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepFixstepList  templateStepFixstepList)
      {
         return Add<TemplateStepFixstepList>(templateStepFixstepList);
      }
      /// <summary>
      /// 批量添加可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepFixstepList>  templateStepFixstepLists)
      {
         Batch<long, TemplateStepFixstepList>(templateStepFixstepLists, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepFixstepList  templateStepFixstepList)
      {
         return Update<TemplateStepFixstepList>(templateStepFixstepList);
      }
      /// <summary>
      /// 删除可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepFixstepList>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public TemplateStepFixstepList GetById(string id)
      {
         return GetByID<TemplateStepFixstepList>(id);
      }
      /// <summary>
      /// 获取所有的可选下一步骤列表{可选下一步骤列表}对象
      /// </summary>
      public IQueryable<TemplateStepFixstepList> Query()
      {
         return CreateQuery<TemplateStepFixstepList>();
      }
   }
}
