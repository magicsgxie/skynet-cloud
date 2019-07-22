/************************************************************************************
* Copyright (c) 2019-07-11 10:53:11 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateMsgCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：246e5e7e-8b10-44ab-808a-51cd0a5d2e88
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:11 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:11 
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

   [Table("UF_TEMPLATE_MSG_CONDITION")]
   public class TemplateMsgCondition
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_MSG
      /// <summary>
      [Column("STEP_MSG_ID",DbType = DBType.NVarChar)]
      public string StepMsgId{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_CONDITION。
      /// <summary>
      [Column("CONDITION_ID",DbType = DBType.NVarChar)]
      public string ConditionId{ set; get;}
   }
}
