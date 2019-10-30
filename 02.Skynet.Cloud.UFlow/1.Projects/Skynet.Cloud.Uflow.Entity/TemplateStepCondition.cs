/************************************************************************************
* Copyright (c) 2019-07-11 10:53:14 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：e55ebc24-223b-4c5e-a2ac-8f8faa5b62d6
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:14 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:14 
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

   [Table("UF_TEMPLATE_STEP_CONDITION")]
   public class TemplateStepCondition
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
      /// 0转入条件,1转出条件
      /// <summary>
      [Column("TYPE",DbType = DBType.Int32)]
      public int Type{ set; get;}
      /// <summary>
      /// 关联T_WF_Condition。
      /// <summary>
      [Column("CONDITION_ID",DbType = DBType.NVarChar)]
      public string ConditionId{ set; get;}
   }
}
