/************************************************************************************
* Copyright (c) 2019-07-11 12:02:51 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.FormTreemenuStatus.cs
* 版本号：  V1.0.0.0
* 唯一标识：1a03e9f4-b708-4b07-87c4-a906d5a01530
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:51 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:51 
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
   /// 流程业务服务实现类
   /// </summary>
   public class FormTreemenuStatusService: IFormTreemenuStatusService
   {
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public long Add(FormTreemenuStatus  formTreemenuStatus)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new FormTreemenuStatusRepository(dbContext).Add(formTreemenuStatus);
         }
      }
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public void Add(IList<FormTreemenuStatus>  formTreemenuStatuss)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new FormTreemenuStatusRepository(dbContext).Add(formTreemenuStatuss);
         }
      }
      /// <summary>
      /// 更新流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Update(FormTreemenuStatus  formTreemenuStatus)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new FormTreemenuStatusRepository(dbContext).Update(formTreemenuStatus);
         }
      }
      /// <summary>
      /// 删除流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new FormTreemenuStatusRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public FormTreemenuStatus GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new FormTreemenuStatusRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new FormTreemenuStatusRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
