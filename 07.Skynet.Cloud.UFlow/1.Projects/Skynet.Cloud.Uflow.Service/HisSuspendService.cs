/************************************************************************************
* Copyright (c) 2019-07-11 12:03:03 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisSuspend.cs
* 版本号：  V1.0.0.0
* 唯一标识：bfa59583-8224-4fd4-89a8-6b09c8a536a5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:03 
* 描述：实例挂起记录 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:03 
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
   /// 实例挂起记录服务实现类
   /// </summary>
   public class HisSuspendService: IHisSuspendService
   {
      /// <summary>
      /// 添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public long Add(HisSuspend  hisSuspend)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSuspendRepository(dbContext).Add(hisSuspend);
         }
      }
      /// <summary>
      /// 添加实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisSuspend>  hisSuspends)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisSuspendRepository(dbContext).Add(hisSuspends);
         }
      }
      /// <summary>
      /// 更新实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public int Update(HisSuspend  hisSuspend)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSuspendRepository(dbContext).Update(hisSuspend);
         }
      }
      /// <summary>
      /// 删除实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSuspendRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public HisSuspend GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSuspendRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的实例挂起记录{实例挂起记录}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisSuspendRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
