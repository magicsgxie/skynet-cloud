/************************************************************************************
* Copyright (c) 2019-07-11 12:02:58 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：e47d665e-fae3-4896-bf15-1d2b8ad66ea3
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:58 
* 描述：步骤消息处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:58 
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
   /// 步骤消息处理服务实现类
   /// </summary>
   public class HisStepMsgService: IHisStepMsgService
   {
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public long Add(HisStepMsg  hisStepMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepMsgRepository(dbContext).Add(hisStepMsg);
         }
      }
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepMsg>  hisStepMsgs)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepMsgRepository(dbContext).Add(hisStepMsgs);
         }
      }
      /// <summary>
      /// 更新步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Update(HisStepMsg  hisStepMsg)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepMsgRepository(dbContext).Update(hisStepMsg);
         }
      }
      /// <summary>
      /// 删除步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepMsgRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public HisStepMsg GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepMsgRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepMsgRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
