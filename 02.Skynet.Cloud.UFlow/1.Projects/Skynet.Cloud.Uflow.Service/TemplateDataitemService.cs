/************************************************************************************
* Copyright (c) 2019-07-11 12:02:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateDataitem.cs
* 版本号：  V1.0.0.0
* 唯一标识：a96c0663-69e3-4cbc-a8c2-eb8c4dd83479
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:44 
* 描述：数据项 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:44 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Service
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;

   using System.Linq;
   using UWay.Skynet.Cloud.Uflow.Service.Interface;
   using UWay.Skynet.Cloud.Uflow.Repository;
   using UWay.Skynet.Cloud.Linq;
   using UWay.Skynet.Cloud;

   /// <summary>
   /// 数据项服务实现类
   /// </summary>
   public class TemplateDataitemService: ITemplateDataitemService
   {
      /// <summary>
      /// 添加数据项{数据项}对象(即:一条记录
      /// </summary>
      public long Add(TemplateDataitem  templateDataitem)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateDataitemRepository(dbContext).Add(templateDataitem);
         }
      }
      /// <summary>
      /// 添加数据项{数据项}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateDataitem>  templateDataitems)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateDataitemRepository(dbContext).Add(templateDataitems);
         }
      }
      /// <summary>
      /// 更新数据项{数据项}对象(即:一条记录
      /// </summary>
      public int Update(TemplateDataitem  templateDataitem)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateDataitemRepository(dbContext).Update(templateDataitem);
         }
      }
      /// <summary>
      /// 删除数据项{数据项}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateDataitemRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的数据项{数据项}对象(即:一条记录
      /// </summary>
      public TemplateDataitem GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateDataitemRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的数据项{数据项}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateDataitemRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
