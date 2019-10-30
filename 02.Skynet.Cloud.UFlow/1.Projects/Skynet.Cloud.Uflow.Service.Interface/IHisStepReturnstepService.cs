/************************************************************************************
* Copyright (c) 2019-07-11 12:03:00 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepReturnstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：817c4fc8-c46f-4d99-846d-b2a065c2a854
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:00 
* 描述：退回到的步骤 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:00 
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
   /// 退回到的步骤服务接口类
   /// </summary>
   public interface IHisStepReturnstepService
   {
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      long Add(HisStepReturnstep  hisStepReturnstep);
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      void Add(IList<HisStepReturnstep>  hisStepReturnsteps);
      /// <summary>
      /// 更新退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      int Update(HisStepReturnstep  hisStepReturnstep);
      /// <summary>
      /// 删除退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      HisStepReturnstep GetById(string id);
      /// <summary>
      /// 获取所有的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
