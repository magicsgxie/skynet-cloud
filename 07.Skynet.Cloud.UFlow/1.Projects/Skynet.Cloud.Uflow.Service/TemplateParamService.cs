/************************************************************************************
* Copyright (c) 2019-07-11 12:02:27 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：ae5fbd9f-eadd-4228-b0b2-47f7ffe4c020
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:27 
* 描述：参数 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:27 
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
   /// 参数服务实现类
   /// </summary>
   public class TemplateParamService: ITemplateParamService
   {
      /// <summary>
      /// 添加参数{参数}对象(即:一条记录
      /// </summary>
      public long Add(TemplateParam  templateParam)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateParamRepository(dbContext).Add(templateParam);
         }
      }
      /// <summary>
      /// 添加参数{参数}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateParam>  templateParams)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateParamRepository(dbContext).Add(templateParams);
         }
      }
      /// <summary>
      /// 更新参数{参数}对象(即:一条记录
      /// </summary>
      public int Update(TemplateParam  templateParam)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateParamRepository(dbContext).Update(templateParam);
         }
      }
      /// <summary>
      /// 删除参数{参数}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateParamRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的参数{参数}对象(即:一条记录
      /// </summary>
      public TemplateParam GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateParamRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的参数{参数}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateParamRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
