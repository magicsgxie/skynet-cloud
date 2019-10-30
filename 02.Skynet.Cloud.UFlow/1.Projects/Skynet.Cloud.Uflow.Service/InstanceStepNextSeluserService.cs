/************************************************************************************
* Copyright (c) 2019-07-11 12:03:14 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepNextSeluser.cs
* 版本号：  V1.0.0.0
* 唯一标识：d9fe49a4-628a-4ead-9e51-fedae03dc46b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:14 
* 描述：存放由用户指定的下一步处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:14 
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
   /// 存放由用户指定的下一步处理人服务实现类
   /// </summary>
   public class InstanceStepNextSeluserService: IInstanceStepNextSeluserService
   {
      /// <summary>
      /// 添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public long Add(InstanceStepNextSeluser  instanceStepNextSeluser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepNextSeluserRepository(dbContext).Add(instanceStepNextSeluser);
         }
      }
      /// <summary>
      /// 添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<InstanceStepNextSeluser>  instanceStepNextSelusers)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new InstanceStepNextSeluserRepository(dbContext).Add(instanceStepNextSelusers);
         }
      }
      /// <summary>
      /// 更新存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public int Update(InstanceStepNextSeluser  instanceStepNextSeluser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepNextSeluserRepository(dbContext).Update(instanceStepNextSeluser);
         }
      }
      /// <summary>
      /// 删除存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public int Delete(InstanceStepNextSeluser instanceStepNextSeluser)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepNextSeluserRepository(dbContext).Delete(instanceStepNextSeluser);
         }
      }
      /// <summary>
      /// 获取指定的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public InstanceStepNextSeluser GetById(object id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepNextSeluserRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new InstanceStepNextSeluserRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
