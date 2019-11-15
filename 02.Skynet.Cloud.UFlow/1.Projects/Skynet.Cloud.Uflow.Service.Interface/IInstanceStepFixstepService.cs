/************************************************************************************
* Copyright (c) 2019-07-11 12:03:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepFixstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：78a32f49-7f29-4c04-b4e6-bcbc9c3367df
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:12 
* 描述：存放用户指定的下一个处理步骤。 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:12 
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
   /// 存放用户指定的下一个处理步骤。服务接口类
   /// </summary>
   public interface IInstanceStepFixstepService
   {
      /// <summary>
      /// 添加存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepFixstep  instanceStepFixstep);
      /// <summary>
      /// 添加存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepFixstep>  instanceStepFixsteps);
      /// <summary>
      /// 更新存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepFixstep  instanceStepFixstep);
      /// <summary>
      /// 删除存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );

        /// <summary>
        /// 删除存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
        /// </summary>
        int DeleteByInstanceStepId(string id);
        /// <summary>
        /// 获取指定的存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
        /// </summary>
        InstanceStepFixstep GetById(string id);
      /// <summary>
      /// 获取所有的存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
