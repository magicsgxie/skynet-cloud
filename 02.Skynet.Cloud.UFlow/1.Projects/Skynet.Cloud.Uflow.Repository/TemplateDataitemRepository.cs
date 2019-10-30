/************************************************************************************
* Copyright (c) 2019-07-11 12:07:59 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.TemplateDataitem.cs
* 版本号：  V1.0.0.0
* 唯一标识：bbddc0b3-299a-45fc-af5c-7bacbbce0444
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:07:59 
* 描述：数据项 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:07:59 
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
   /// 数据项仓储类
   /// </summary>
   public class TemplateDataitemRepository:ObjectRepository
   {
      public TemplateDataitemRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加数据项{数据项}对象(即:一条记录
      /// </summary>
      public long Add(TemplateDataitem  templateDataitem)
      {
         return Add<TemplateDataitem>(templateDataitem);
      }
      /// <summary>
      /// 批量添加数据项{数据项}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateDataitem>  templateDataitems)
      {
         Batch<long, TemplateDataitem>(templateDataitems, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新数据项{数据项}对象(即:一条记录
      /// </summary>
      public int Update(TemplateDataitem  templateDataitem)
      {
         return Update<TemplateDataitem>(templateDataitem);
      }
      /// <summary>
      /// 删除数据项{数据项}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<TemplateDataitem>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的数据项{数据项}对象(即:一条记录
      /// </summary>
      public TemplateDataitem GetById(string id)
      {
         return GetByID<TemplateDataitem>(id);
      }
      /// <summary>
      /// 获取所有的数据项{数据项}对象
      /// </summary>
      public IQueryable<TemplateDataitem> Query()
      {
         return CreateQuery<TemplateDataitem>();
      }
   }
}
