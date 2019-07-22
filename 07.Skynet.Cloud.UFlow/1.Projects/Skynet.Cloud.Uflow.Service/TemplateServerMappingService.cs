/************************************************************************************
* Copyright (c) 2019-07-11 12:02:28 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateServerMapping.cs
* 版本号：  V1.0.0.0
* 唯一标识：2a341dea-9971-4f4e-aec0-d1451fed5564
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:28 
* 描述：模版与引擎服务器映射关系表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:28 
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
   /// 模版与引擎服务器映射关系表服务实现类
   /// </summary>
   public class TemplateServerMappingService: ITemplateServerMappingService
   {
      /// <summary>
      /// 添加模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateServerMapping  templateServerMapping)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateServerMappingRepository(dbContext).Add(templateServerMapping);
         }
      }
      /// <summary>
      /// 添加模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateServerMapping>  templateServerMappings)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateServerMappingRepository(dbContext).Add(templateServerMappings);
         }
      }
      /// <summary>
      /// 更新模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateServerMapping  templateServerMapping)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateServerMappingRepository(dbContext).Update(templateServerMapping);
         }
      }
      /// <summary>
      /// 删除模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public int Delete(int[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateServerMappingRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public TemplateServerMapping GetById(int id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateServerMappingRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的模版与引擎服务器映射关系表{模版与引擎服务器映射关系表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateServerMappingRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
