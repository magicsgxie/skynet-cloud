/************************************************************************************
* Copyright (c) 2019-07-11 12:02:30 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：e26e69e3-f7c0-4629-a953-4ae821a61471
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:30 
* 描述：循环处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:30 
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
   /// 循环处理服务实现类
   /// </summary>
   public class TemplateStepCycleService: ITemplateStepCycleService
   {
      /// <summary>
      /// 添加循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepCycle  templateStepCycle)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepCycleRepository(dbContext).Add(templateStepCycle);
         }
      }
      /// <summary>
      /// 添加循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepCycle>  templateStepCycles)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepCycleRepository(dbContext).Add(templateStepCycles);
         }
      }
      /// <summary>
      /// 更新循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepCycle  templateStepCycle)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepCycleRepository(dbContext).Update(templateStepCycle);
         }
      }
      /// <summary>
      /// 删除循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepCycleRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public TemplateStepCycle GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepCycleRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的循环处理{循环处理}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepCycleRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
