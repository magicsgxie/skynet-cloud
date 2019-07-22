/************************************************************************************
* Copyright (c) 2019-07-11 12:01:49 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceDraft.cs
* 版本号：  V1.0.0.0
* 唯一标识：67022a3c-32ec-4f33-a7ae-87592b0648c5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:01:49 
* 描述：发起草稿记录表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:01:49 
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
   /// 发起草稿记录表服务实现类
   /// </summary>
   public class InstanceDraftService: IInstanceDraftService
   {
      /// <summary>
      /// 添加发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceDraft  instanceDraft)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDraftRepository(dbContext).Add(instanceDraft);
         }
      }
      /// <summary>
      /// 添加发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceDraft>  instanceDrafts)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceDraftRepository(dbContext).Add(instanceDrafts);
         }
      }
      /// <summary>
      /// 更新发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceDraft  instanceDraft)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDraftRepository(dbContext).Update(instanceDraft);
         }
      }
      /// <summary>
      /// 删除发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDraftRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public InstanceDraft GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDraftRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的发起草稿记录表{发起草稿记录表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDraftRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
