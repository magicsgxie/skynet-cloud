/************************************************************************************
* Copyright (c) 2019-07-11 10:53:33 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.DataitemTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：9da880e1-c08b-4c20-8008-1dfbdb064f31
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:33 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:33 
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

   [Table("UF_DATAITEM_TEMPLATE")]
   public class DataitemTemplate
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 0上一步,1发起步骤,2当前步骤,3指定步骤,4已走流程,5当前日期
      /// <summary>
      [Column("SOURCE",DbType = DBType.Int32)]
      public int Source{ set; get;}
      /// <summary>
      /// 步骤序号
      /// <summary>
      [Column("STEP_SEQ",DbType = DBType.Int32)]
      public int? StepSeq{ set; get;}
      /// <summary>
      /// 0流程/步骤数据,1业务数据,2事件数据,3子流程
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
