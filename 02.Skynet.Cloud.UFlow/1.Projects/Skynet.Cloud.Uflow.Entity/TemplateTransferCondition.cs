/************************************************************************************
* Copyright (c) 2019-07-11 10:53:22 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateTransferCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：dd1ff10c-3445-461e-9e16-8363b752b847
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:22 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:22 
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

   [Table("UF_TEMPLATE_TRANSFER_CONDITION")]
   public class TemplateTransferCondition
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表转发控制表ID
      /// <summary>
      [Column("STEP_TRANSFER_ID",DbType = DBType.NVarChar)]
      public string StepTransferId{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_CONDITION。
      /// <summary>
      [Column("CONDITION_ID",DbType = DBType.NVarChar)]
      public string ConditionId{ set; get;}
   }
}
