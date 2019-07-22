/************************************************************************************
* Copyright (c) 2019-07-11 12:03:06 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceDataitemResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：1f38d6b7-ba1d-46dd-b4f2-3e8cbbdb028a
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:06 
* 描述：数据项结果表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:06 
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
   public class InstanceDataitemResultService: IInstanceDataitemResultService
   {
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public long Add(InstanceDataitemResult  instanceDataitemResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDataitemResultRepository(dbContext).Add(instanceDataitemResult);
         }
      }
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceDataitemResult>  instanceDataitemResults)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceDataitemResultRepository(dbContext).Add(instanceDataitemResults);
         }
      }
      /// <summary>
      /// 更新数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Update(InstanceDataitemResult  instanceDataitemResult)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDataitemResultRepository(dbContext).Update(instanceDataitemResult);
         }
      }
      /// <summary>
      /// 删除数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDataitemResultRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public InstanceDataitemResult GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDataitemResultRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceDataitemResultRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
