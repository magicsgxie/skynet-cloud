/************************************************************************************
* Copyright (c) 2019-07-11 12:02:58 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.HisStepFixSeluser.cs
* 版本号：  V1.0.0.0
* 唯一标识：cf549c16-8e0a-4808-bb6b-fd9769188e77
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:58 
* 描述：存放由用户指定的下一步处理人 
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



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 存放由用户指定的下一步处理人服务接口类
   /// </summary>
   public interface IHisStepFixSeluserService
   {
      /// <summary>
      /// 添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      long Add(HisStepFixSeluser  hisStepFixSeluser);
      /// <summary>
      /// 添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      void Add(IList<HisStepFixSeluser>  hisStepFixSelusers);
      /// <summary>
      /// 更新存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      int Update(HisStepFixSeluser  hisStepFixSeluser);
      /// <summary>
      /// 删除存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      int Delete(HisStepFixSeluser hisStepFixSeluser);
      /// <summary>
      /// 获取指定的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      HisStepFixSeluser GetById(object id);
      /// <summary>
      /// 获取所有的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
