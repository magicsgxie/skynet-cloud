/************************************************************************************
* Copyright (c) 2019-07-11 12:02:34 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepFixstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：3729f5e8-62d2-4482-b1d3-02c6cc0defaa
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:34 
* 描述：指定步骤可选人员列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:34 
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
   /// 指定步骤可选人员列表服务接口类
   /// </summary>
   public interface ITemplateStepFixstepUserService
   {
      /// <summary>
      /// 添加指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepFixstepUser  templateStepFixstepUser);
      /// <summary>
      /// 添加指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepFixstepUser>  templateStepFixstepUsers);
      /// <summary>
      /// 更新指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepFixstepUser  templateStepFixstepUser);
      /// <summary>
      /// 删除指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      TemplateStepFixstepUser GetById(string id);
      /// <summary>
      /// 获取所有的指定步骤可选人员列表{指定步骤可选人员列表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
