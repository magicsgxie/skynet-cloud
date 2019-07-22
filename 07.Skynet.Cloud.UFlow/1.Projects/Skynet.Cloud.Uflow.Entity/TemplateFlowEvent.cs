/************************************************************************************
* Copyright (c) 2019-07-11 10:53:09 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：e8d0914c-755e-448e-8356-da176cb750b4
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:09 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:09 
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

   [Table("UF_TEMPLATE_FLOW_EVENT")]
   public class TemplateFlowEvent
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表流程表ID
      /// <summary>
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      /// <summary>
      /// 事件编号
      /// <summary>
      [Column("EVENT_CODE",DbType = DBType.NVarChar)]
      public string EventCode{ set; get;}
      /// <summary>
      /// 0启动时事件,1结事时事件
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
