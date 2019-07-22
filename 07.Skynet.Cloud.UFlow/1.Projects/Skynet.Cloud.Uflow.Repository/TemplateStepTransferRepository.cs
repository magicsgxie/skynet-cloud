/************************************************************************************
* Copyright (c) 2019-07-11 12:07:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：9c0712c3-1072-4a29-b301-90f266ab3d62
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:49 
* 描述：转发控制 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:49 
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
   /// 转发控制仓储类
   /// </summary>
   public class TemplateStepTransferRepository:ObjectRepository
   {
      public TemplateStepTransferRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepTransfer  templateSteptransfer)
      {
         return Add<TemplateStepTransfer>(templateSteptransfer);
      }
      /// <summary>
      /// 批量添加转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepTransfer>  templateSteptransfers)
      {
         Batch<long, TemplateStepTransfer>(templateSteptransfers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepTransfer  templateSteptransfer)
      {
         return Update<TemplateStepTransfer>(templateSteptransfer);
      }
      /// <summary>
      /// 删除转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateStepTransfer>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public TemplateStepTransfer GetById(string id)
      {
         return GetByID<TemplateStepTransfer>(id);
      }
      /// <summary>
      /// 获取所有的转发控制{转发控制}对象
      /// </summary>
      public IQueryable<TemplateStepTransfer> Query()
      {
         return CreateQuery<TemplateStepTransfer>();
      }
   }
}
