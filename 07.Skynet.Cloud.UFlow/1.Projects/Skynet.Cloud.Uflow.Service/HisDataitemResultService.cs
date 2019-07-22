/************************************************************************************
* Copyright (c) 2019-07-11 12:02:52 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisDataitemResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：46621db7-855d-4d18-a092-469eba1b83ca
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:52 
* 描述：数据项结果表 
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
   /// 数据项结果表服务实现类
   /// </summary>
   public class HisDataitemResultService: IHisDataitemResultService
   {
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public long Add(HisDataitemResult  hisDataitemResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisDataitemResultRepository(dbContext).Add(hisDataitemResult);
         }
      }
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisDataitemResult>  hisDataitemResults)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisDataitemResultRepository(dbContext).Add(hisDataitemResults);
         }
      }
      /// <summary>
      /// 更新数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Update(HisDataitemResult  hisDataitemResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisDataitemResultRepository(dbContext).Update(hisDataitemResult);
         }
      }
      /// <summary>
      /// 删除数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisDataitemResultRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public HisDataitemResult GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisDataitemResultRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisDataitemResultRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
