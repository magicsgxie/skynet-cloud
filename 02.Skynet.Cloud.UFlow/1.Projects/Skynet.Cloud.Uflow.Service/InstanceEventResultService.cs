/************************************************************************************
* Copyright (c) 2019-07-11 12:03:07 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.InstanceEventResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：7639d972-9d69-44d8-9882-9cea706fb3ac
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:07 
* 描述：实例事件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:07 
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
   /// 实例事件结果服务实现类
   /// </summary>
   public class InstanceEventResultService: IInstanceEventResultService
   {
      /// <summary>
      /// 添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public long Add(InstanceEventResult  instanceEventResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceEventResultRepository(dbContext).Add(instanceEventResult);
         }
      }
      /// <summary>
      /// 添加实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceEventResult>  instanceEventResults)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceEventResultRepository(dbContext).Add(instanceEventResults);
         }
      }
      /// <summary>
      /// 更新实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public int Update(InstanceEventResult  instanceEventResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceEventResultRepository(dbContext).Update(instanceEventResult);
         }
      }
      /// <summary>
      /// 删除实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceEventResultRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public InstanceEventResult GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceEventResultRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的实例事件结果{实例事件结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceEventResultRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
