/************************************************************************************
* Copyright (c) 2019-07-11 10:53:21 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：b18b6dd5-a18e-47a3-9eb3-8cc397c937b0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:21 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:21 
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

   [Table("UF_TEMPLATE_STEP_USER")]
   public class TemplateStepUser
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
      /// 关联T_WF_User。
      /// <summary>
      [Column("USER_ID",DbType = DBType.NVarChar)]
      public string UserId{ set; get;}
      /// <summary>
      /// 0主办,1汇签,2协办,3抄送
      /// <summary>
      [Column("USER_TYPE",DbType = DBType.Int32)]
      public int UserType{ set; get;}
   }
}
