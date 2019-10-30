/************************************************************************************
* Copyright (c) 2019-07-11 12:02:45 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：0d182f54-7f38-4e68-9d5d-b1fcec32eaad
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:45 
* 描述：子流程表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:45 
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
   /// 子流程表服务实现类
   /// </summary>
   public class HisStepSubflowService: IHisStepSubflowService
   {
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public long Add(HisStepSubflow  hisStepSubflow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepSubflowRepository(dbContext).Add(hisStepSubflow);
         }
      }
      /// <summary>
      /// 添加子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepSubflow>  hisStepSubflows)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepSubflowRepository(dbContext).Add(hisStepSubflows);
         }
      }
      /// <summary>
      /// 更新子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public int Update(HisStepSubflow  hisStepSubflow)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepSubflowRepository(dbContext).Update(hisStepSubflow);
         }
      }
      /// <summary>
      /// 删除子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepSubflowRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public HisStepSubflow GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepSubflowRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的子流程表{子流程表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepSubflowRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
