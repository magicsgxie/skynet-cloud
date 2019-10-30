/************************************************************************************
* Copyright (c) 2019-07-11 10:53:39 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.HisFlowEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：27e8baf9-b14c-4921-99e2-9106fb3796e1
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:39 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:39 
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

   [Table("UF_HIS_FLOW_EVENT")]
   public class HisFlowEvent
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表流程实例表ID
      /// <summary>
      [Column("INSTANCE_FLOW_ID",DbType = DBType.NVarChar)]
      public string InstanceFlowId{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_FLOW_EVENT
      /// <summary>
      [Column("EVENT_ID",DbType = DBType.NVarChar)]
      public string EventId{ set; get;}
      /// <summary>
      /// 关联UF_INSTANCE_EVENT_RESULT
      /// <summary>
      [Column("EVENT_RESULT_ID",DbType = DBType.NVarChar)]
      public string EventResultId{ set; get;}
      /// <summary>
      /// 创建者
      /// <summary>
      [Column("CREATOR",DbType = DBType.NVarChar)]
      public string Creator{ set; get;}
      /// <summary>
      /// 创建日期
      /// <summary>
      [Column("CREATE_DATE",DbType = DBType.DateTime)]
      public DateTime CreateDate{ set; get;}
      /// <summary>
      /// 编辑者
      /// <summary>
      [Column("EDITOR",DbType = DBType.NVarChar)]
      public string Editor{ set; get;}
      /// <summary>
      /// 编辑日期
      /// <summary>
      [Column("EDIT_DATE",DbType = DBType.DateTime)]
      public DateTime EditDate{ set; get;}
   }
}
