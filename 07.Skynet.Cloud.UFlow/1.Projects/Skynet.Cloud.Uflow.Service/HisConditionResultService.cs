/************************************************************************************
* Copyright (c) 2019-07-11 12:02:52 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisConditionResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：01d25595-db6e-4c0f-aadd-16891b24afe7
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:52 
* 描述：条件结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:52 
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
   /// 条件结果服务实现类
   /// </summary>
   public class HisConditionResultService: IHisConditionResultService
   {
      /// <summary>
      /// 添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public long Add(HisConditionResult  hisConditionResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisConditionResultRepository(dbContext).Add(hisConditionResult);
         }
      }
      /// <summary>
      /// 添加条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisConditionResult>  hisConditionResults)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisConditionResultRepository(dbContext).Add(hisConditionResults);
         }
      }
      /// <summary>
      /// 更新条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public int Update(HisConditionResult  hisConditionResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisConditionResultRepository(dbContext).Update(hisConditionResult);
         }
      }
      /// <summary>
      /// 删除条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisConditionResultRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public HisConditionResult GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisConditionResultRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的条件结果{条件结果}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisConditionResultRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
