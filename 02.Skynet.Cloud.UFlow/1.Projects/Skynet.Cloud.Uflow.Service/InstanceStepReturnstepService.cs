/************************************************************************************
* Copyright (c) 2019-07-11 12:03:15 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepReturnstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：2095473c-2267-4dab-8463-48ae0d7ee19d
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
   public class InstanceStepReturnstepService: IInstanceStepReturnstepService
   {
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepReturnstep  instanceStepReturnstep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepReturnstepRepository(dbContext).Add(instanceStepReturnstep);
         }
      }
      /// <summary>
      /// 添加退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepReturnstep>  instanceStepReturnsteps)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepReturnstepRepository(dbContext).Add(instanceStepReturnsteps);
         }
      }
      /// <summary>
      /// 更新退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepReturnstep  instanceStepReturnstep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepReturnstepRepository(dbContext).Update(instanceStepReturnstep);
         }
      }
      /// <summary>
      /// 删除退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepReturnstepRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public InstanceStepReturnstep GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepReturnstepRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的退回到的步骤{退回到的步骤}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepReturnstepRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }

        public int DeleteByInstanceStepId(string id)
        {

            using (var dbContext = UnitOfWork.Get(Unity.ContainerName))
            {
                return new InstanceStepReturnstepRepository(dbContext).DeleteByInstanceStepId(id);
            }
        }
    }
}
