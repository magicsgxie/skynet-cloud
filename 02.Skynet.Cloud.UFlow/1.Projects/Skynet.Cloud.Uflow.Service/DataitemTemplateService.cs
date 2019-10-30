/************************************************************************************
* Copyright (c) 2019-07-11 12:02:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.DataitemTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：99b5abeb-db6b-4032-8d21-9bf9c5eac057
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:49 
* 描述：数据项模板 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:49 
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
   /// 数据项模板服务实现类
   /// </summary>
   public class DataitemTemplateService: IDataitemTemplateService
   {
      /// <summary>
      /// 添加数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public long Add(DataitemTemplate  dataitemTemplate)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new DataitemTemplateRepository(dbContext).Add(dataitemTemplate);
         }
      }
      /// <summary>
      /// 添加数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public void Add(IList<DataitemTemplate>  dataitemTemplates)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new DataitemTemplateRepository(dbContext).Add(dataitemTemplates);
         }
      }
      /// <summary>
      /// 更新数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public int Update(DataitemTemplate  dataitemTemplate)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new DataitemTemplateRepository(dbContext).Update(dataitemTemplate);
         }
      }
      /// <summary>
      /// 删除数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new DataitemTemplateRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public DataitemTemplate GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new DataitemTemplateRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的数据项模板{数据项模板}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new DataitemTemplateRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
