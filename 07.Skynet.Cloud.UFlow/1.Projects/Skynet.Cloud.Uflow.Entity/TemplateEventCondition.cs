/************************************************************************************
* Copyright (c) 2019-07-11 10:53:08 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateEventCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：3747f992-9b79-4672-8403-9000c376ec1f
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:08 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:08 
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

   [Table("UF_TEMPLATE_EVENT_CONDITION")]
   public class TemplateEventCondition
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_EVENT
      /// <summary>
      [Column("EVENT_ID",DbType = DBType.NVarChar)]
      public string EventId{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_CONDITION。
      /// <summary>
      [Column("CONDITION_ID",DbType = DBType.NVarChar)]
      public string ConditionId{ set; get;}
   }
}
