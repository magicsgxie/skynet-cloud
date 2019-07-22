/************************************************************************************
* Copyright (c) 2019-07-11 12:01:45 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateChart.cs
* 版本号：  V1.0.0.0
* 唯一标识：f28d6251-eed9-471f-a11f-944561591ecb
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:01:45 
* 描述：流程图 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:01:45 
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
   /// 流程图服务实现类
   /// </summary>
   public class TemplateChartService: ITemplateChartService
   {
      /// <summary>
      /// 添加流程图{流程图}对象(即:一条记录
      /// </summary>
      public long Add(TemplateChart  templateChart)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateChartRepository(dbContext).Add(templateChart);
         }
      }
      /// <summary>
      /// 添加流程图{流程图}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateChart>  templateCharts)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateChartRepository(dbContext).Add(templateCharts);
         }
      }
      /// <summary>
      /// 更新流程图{流程图}对象(即:一条记录
      /// </summary>
      public int Update(TemplateChart  templateChart)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateChartRepository(dbContext).Update(templateChart);
         }
      }
      /// <summary>
      /// 删除流程图{流程图}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateChartRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程图{流程图}对象(即:一条记录
      /// </summary>
      public TemplateChart GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateChartRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程图{流程图}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateChartRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
