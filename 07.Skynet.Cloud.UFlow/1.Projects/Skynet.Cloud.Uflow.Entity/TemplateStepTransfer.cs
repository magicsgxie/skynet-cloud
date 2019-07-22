/************************************************************************************
* Copyright (c) 2019-07-11 10:53:20 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：3661184a-8ad3-4b58-82ea-f92626314a35
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:20 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:20 
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

   [Table("UF_TEMPLATE_STEP_TRANSFER")]
   public class TemplateStepTransfer
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表步骤表ID
      /// <summary>
      [Column("STEP_ID",DbType = DBType.NVarChar)]
      public string StepId{ set; get;}
      /// <summary>
      /// 序号
      /// <summary>
      [Column("SEQ",DbType = DBType.Int32)]
      public int Seq{ set; get;}
      /// <summary>
      /// 存储步骤的序号,多个序号用逗号分隔
      /// <summary>
      [Column("NEXTSTEP",DbType = DBType.NVarChar)]
      public string Nextstep{ set; get;}
      /// <summary>
      /// 0非排它,1排它
      /// <summary>
      [Column("IS_FLAG",DbType = DBType.Int32)]
      public int? IsFlag{ set; get;}
   }
}
