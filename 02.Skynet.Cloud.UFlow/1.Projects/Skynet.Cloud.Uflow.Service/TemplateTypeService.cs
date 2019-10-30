/************************************************************************************
* Copyright (c) 2019-07-11 12:02:40 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateType.cs
* 版本号：  V1.0.0.0
* 唯一标识：8e77fe3b-1019-4fe8-804b-7495bcdf1e2c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:40 
* 描述：流程分类 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:40 
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
   /// 流程分类服务实现类
   /// </summary>
   public class TemplateTypeService: ITemplateTypeService
   {
      /// <summary>
      /// 添加流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public long Add(TemplateType  templatetype)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTypeRepository(dbContext).Add(templatetype);
         }
      }
      /// <summary>
      /// 添加流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateType>  templatetypes)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateTypeRepository(dbContext).Add(templatetypes);
         }
      }
      /// <summary>
      /// 更新流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public int Update(TemplateType  templatetype)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTypeRepository(dbContext).Update(templatetype);
         }
      }
      /// <summary>
      /// 删除流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTypeRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public TemplateType GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTypeRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程分类{流程分类}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateTypeRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
