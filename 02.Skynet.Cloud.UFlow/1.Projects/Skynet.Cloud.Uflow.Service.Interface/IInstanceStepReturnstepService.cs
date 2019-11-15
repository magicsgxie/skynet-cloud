/************************************************************************************
* Copyright (c) 2019-07-11 12:03:15 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepReturnstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：d04e5190-c3f0-48be-bc57-ac380b7ccebd
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:15 
* 描述：退回到的步骤 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:15 
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
   public interface IInstanceStepReturnstepService
   {
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepReturnstep  instanceStepReturnstep);
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepReturnstep>  instanceStepReturnsteps);
      /// <summary>
      /// 更新退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepReturnstep  instanceStepReturnstep);
      /// <summary>
      /// 删除退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );

      int DeleteByInstanceStepId(string id);
      /// <summary>
      /// 获取指定的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
        InstanceStepReturnstep GetById(string id);
      /// <summary>
      /// 获取所有的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
