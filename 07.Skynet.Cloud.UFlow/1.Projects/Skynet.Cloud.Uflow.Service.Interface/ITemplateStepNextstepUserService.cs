/************************************************************************************
* Copyright (c) 2019-07-11 12:02:35 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateStepNextstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：86e73d68-8821-4d0e-97f7-7028f1f7eb57
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:35 
* 描述：下一步可选人员列表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:35 
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
   /// 下一步可选人员列表服务接口类
   /// </summary>
   public interface ITemplateStepNextstepUserService
   {
      /// <summary>
      /// 添加下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      long Add(TemplateStepNextstepUser  templateStepNextstepUser);
      /// <summary>
      /// 添加下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateStepNextstepUser>  templateStepNextstepUsers);
      /// <summary>
      /// 更新下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      int Update(TemplateStepNextstepUser  templateStepNextstepUser);
      /// <summary>
      /// 删除下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      TemplateStepNextstepUser GetById(string id);
      /// <summary>
      /// 获取所有的下一步可选人员列表{下一步可选人员列表}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
