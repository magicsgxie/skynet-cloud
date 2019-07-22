/************************************************************************************
* Copyright (c) 2019-07-11 10:53:28 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateDataitem.cs
* 版本号：  V1.0.0.0
* 唯一标识：7852f001-1197-4ee2-84d3-d6b8f3001a04
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:28 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:28 
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

   [Table("UF_TEMPLATE_DATAITEM")]
   public class TemplateDataitem
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 流程ID
      /// <summary>
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      /// <summary>
      /// 0上一步,1发起步骤,2当前步骤,3指定步骤,4已走流程,5当前日期
      /// <summary>
      [Column("SOURCE",DbType = DBType.Int32)]
      public int Source{ set; get;}
      /// <summary>
      /// 步骤ID
      /// <summary>
      [Column("STEP_SEQ",DbType = DBType.Int32)]
      public int? StepSeq{ set; get;}
      /// <summary>
      /// 0流程/步骤数据,1业务数据,2事件数据
      /// <summary>
      [Column("DATA_TYPE",DbType = DBType.Int32)]
      public int DataType{ set; get;}
      /// <summary>
      /// 数据项类别
      /// <summary>
      [Column("ITEM_TYPE",DbType = DBType.Int32)]
      public int ItemType{ set; get;}
      /// <summary>
      /// 数据项值1
      /// <summary>
      [Column("ITEM_VALUE1",DbType = DBType.NVarChar)]
      public string ItemValue1{ set; get;}
      /// <summary>
      /// 数据项值2
      /// <summary>
      [Column("ITEM_VALUE2",DbType = DBType.NVarChar)]
      public string ItemValue2{ set; get;}
      /// <summary>
      /// 描述
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
   }
}
