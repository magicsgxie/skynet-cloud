/************************************************************************************
* Copyright (c) 2019-07-11 12:02:38 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：7746a6a6-737e-494f-9139-c64fbb4a3634
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:38 
* 描述：步骤处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:38 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 步骤处理人服务接口类
   /// </summary>
   public interface ITemplateStepUserService
   {
      /// <summary>
      /// 添加步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepUser  templateStepUser);
      /// <summary>
      /// 添加步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepUser>  templateStepUsers);
      /// <summary>
      /// 更新步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepUser  templateStepUser);
      /// <summary>
      /// 删除步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      TemplateStepUser GetById(string id);
      /// <summary>
      /// 获取所有的步骤处理人{步骤处理人}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
