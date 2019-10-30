/************************************************************************************
* Copyright (c) 2019-07-11 10:53:16 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：46a3ffec-3b2d-49a7-ba61-a7a3c5b4b056
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:16 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:16 
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

   [Table("UF_TEMPLATE_STEP_EVENT")]
   public class TemplateStepEvent
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表步骤表ID
      /// <summary>
      [Column("STEP_ID",DbType = DBType.NVarChar)]
      public string StepId{ set; get;}
      /// <summary>
      /// 事件编号
      /// <summary>
      [Column("EVENT_CODE",DbType = DBType.NVarChar)]
      public string EventCode{ set; get;}
      /// <summary>
      /// 0前期事件,1后期完成事件,2后期退回事件
      /// <summary>
      [Column("EVENT_TYPE",DbType = DBType.Int32)]
      public int EventType{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_EVENT。
      /// <summary>
      [Column("EVENT_ID",DbType = DBType.NVarChar)]
      public string EventId{ set; get;}
   }
}
