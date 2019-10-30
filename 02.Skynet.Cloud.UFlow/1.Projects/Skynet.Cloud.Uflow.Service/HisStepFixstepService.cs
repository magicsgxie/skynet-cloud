/************************************************************************************
* Copyright (c) 2019-07-11 12:02:57 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepFixstep.cs
* 版本号：  V1.0.0.0
* 唯一标识：932265ab-7f58-4ac8-a5b2-b217d1c5f12f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:57 
* 描述：存放用户指定的下一个处理步骤。 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:57 
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
   /// 存放用户指定的下一个处理步骤。服务实现类
   /// </summary>
   public class HisStepFixstepService: IHisStepFixstepService
   {
      /// <summary>
      /// 添加存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public long Add(HisStepFixstep  hisStepFixstep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepFixstepRepository(dbContext).Add(hisStepFixstep);
         }
      }
      /// <summary>
      /// 添加存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepFixstep>  hisStepFixsteps)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new HisStepFixstepRepository(dbContext).Add(hisStepFixsteps);
         }
      }
      /// <summary>
      /// 更新存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public int Update(HisStepFixstep  hisStepFixstep)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepFixstepRepository(dbContext).Update(hisStepFixstep);
         }
      }
      /// <summary>
      /// 删除存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepFixstepRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public HisStepFixstep GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepFixstepRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的存放用户指定的下一个处理步骤。{存放用户指定的下一个处理步骤。}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new HisStepFixstepRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
