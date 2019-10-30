/************************************************************************************
* Copyright (c) 2019-07-11 12:02:37 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：e02dda18-3cb5-4bba-958b-978901888368
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:37 
* 描述：转发控制 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:37 
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
   /// 转发控制服务实现类
   /// </summary>
   public class TemplateStepTransferService: ITemplateStepTransferService
   {
      /// <summary>
      /// 添加转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepTransfer  templateSteptransfer)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepTransferRepository(dbContext).Add(templateSteptransfer);
         }
      }
      /// <summary>
      /// 添加转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepTransfer>  templateSteptransfers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepTransferRepository(dbContext).Add(templateSteptransfers);
         }
      }
      /// <summary>
      /// 更新转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepTransfer  templateSteptransfer)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepTransferRepository(dbContext).Update(templateSteptransfer);
         }
      }
      /// <summary>
      /// 删除转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepTransferRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public TemplateStepTransfer GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepTransferRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的转发控制{转发控制}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepTransferRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
