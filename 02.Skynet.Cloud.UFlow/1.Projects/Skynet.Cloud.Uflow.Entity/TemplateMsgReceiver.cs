/************************************************************************************
* Copyright (c) 2019-07-11 10:53:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateMsgReceiver.cs
* 版本号：  V1.0.0.0
* 唯一标识：6d560242-3ddb-4d9d-8617-de0a1099f661
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:12 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:12 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Entity
{
   using System;
   using UWay.Skynet.Cloud.Data;

   [Table("UF_TEMPLATE_MSG_RECEIVER")]
   public class TemplateMsgReceiver
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 消息ID
      /// <summary>
      [Column("STEP_MSG_ID",DbType = DBType.NVarChar)]
      public string StepMsgId{ set; get;}
      /// <summary>
      /// 人员ID
      /// <summary>
      [Column("USER_ID",DbType = DBType.NVarChar)]
      public string UserId{ set; get;}
   }
}
