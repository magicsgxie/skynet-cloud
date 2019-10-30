/************************************************************************************
* Copyright (c) 2019-07-11 12:02:54 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：c489f55c-eeb4-4d72-8d88-a6eebd0f4f3c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:54 
* 描述：实例事件表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:54 
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
   /// 实例事件表服务实现类
   /// </summary>
   public class HisFlowEventService: IHisFlowEventService
   {
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public long Add(HisFlowEvent  hisFlowEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowEventRepository(dbContext).Add(hisFlowEvent);
         }
      }
      /// <summary>
      /// 添加实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisFlowEvent>  hisFlowEvents)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisFlowEventRepository(dbContext).Add(hisFlowEvents);
         }
      }
      /// <summary>
      /// 更新实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Update(HisFlowEvent  hisFlowEvent)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowEventRepository(dbContext).Update(hisFlowEvent);
         }
      }
      /// <summary>
      /// 删除实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowEventRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public HisFlowEvent GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowEventRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的实例事件表{实例事件表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisFlowEventRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
