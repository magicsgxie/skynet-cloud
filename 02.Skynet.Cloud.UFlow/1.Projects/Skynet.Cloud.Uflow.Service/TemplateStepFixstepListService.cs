/************************************************************************************
* Copyright (c) 2019-07-11 12:02:33 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepFixstepList.cs
* 版本号：  V1.0.0.0
* 唯一标识：17d41151-c18d-4028-a75a-637cf9daea52
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:33 
* 描述：可选下一步骤列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:33 
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
   /// 可选下一步骤列表服务实现类
   /// </summary>
   public class TemplateStepFixstepListService: ITemplateStepFixstepListService
   {
      /// <summary>
      /// 添加可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateStepFixstepList  templateStepFixstepList)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepListRepository(dbContext).Add(templateStepFixstepList);
         }
      }
      /// <summary>
      /// 添加可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateStepFixstepList>  templateStepFixstepLists)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateStepFixstepListRepository(dbContext).Add(templateStepFixstepLists);
         }
      }
      /// <summary>
      /// 更新可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateStepFixstepList  templateStepFixstepList)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepListRepository(dbContext).Update(templateStepFixstepList);
         }
      }
      /// <summary>
      /// 删除可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepListRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public TemplateStepFixstepList GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepListRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的可选下一步骤列表{可选下一步骤列表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateStepFixstepListRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
