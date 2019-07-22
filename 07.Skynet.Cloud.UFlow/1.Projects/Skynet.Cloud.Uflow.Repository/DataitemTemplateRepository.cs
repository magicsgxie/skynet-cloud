/************************************************************************************
* Copyright (c) 2019-07-11 12:08:04 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.DataitemTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：0770eb69-989f-468a-a48c-66eaf9cbb44e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:04 
* 描述：数据项模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:04 
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
   /// 数据项模板仓储类
   /// </summary>
   public class DataitemTemplateRepository:ObjectRepository
   {
      public DataitemTemplateRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public long Add(DataitemTemplate  dataitemTemplate)
      {
         return Add<DataitemTemplate>(dataitemTemplate);
      }
      /// <summary>
      /// 批量添加数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public void Add(IList<DataitemTemplate>  dataitemTemplates)
      {
         Batch<long, DataitemTemplate>(dataitemTemplates, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public int Update(DataitemTemplate  dataitemTemplate)
      {
         return Update<DataitemTemplate>(dataitemTemplate);
      }
      /// <summary>
      /// 删除数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<DataitemTemplate>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public DataitemTemplate GetById(string id)
      {
         return GetByID<DataitemTemplate>(id);
      }
      /// <summary>
      /// 获取所有的数据项模板{数据项模板}对象
      /// </summary>
      public IQueryable<DataitemTemplate> Query()
      {
         return CreateQuery<DataitemTemplate>();
      }
   }
}
