/************************************************************************************
* Copyright (c) 2019-07-11 12:03:00 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepReturnstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：d40ecc67-edcd-4789-9b7b-eea950b0e4cf
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
   /// 退回到的步骤服务实现类
   /// </summary>
   public class HisStepReturnstepService: IHisStepReturnstepService
   {
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public long Add(HisStepReturnstep  hisStepReturnstep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepReturnstepRepository(dbContext).Add(hisStepReturnstep);
         }
      }
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepReturnstep>  hisStepReturnsteps)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepReturnstepRepository(dbContext).Add(hisStepReturnsteps);
         }
      }
      /// <summary>
      /// 更新退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public int Update(HisStepReturnstep  hisStepReturnstep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepReturnstepRepository(dbContext).Update(hisStepReturnstep);
         }
      }
      /// <summary>
      /// 删除退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepReturnstepRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public HisStepReturnstep GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepReturnstepRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepReturnstepRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
