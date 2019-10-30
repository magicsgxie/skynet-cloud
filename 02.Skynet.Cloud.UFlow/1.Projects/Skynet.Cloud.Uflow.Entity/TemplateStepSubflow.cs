/************************************************************************************
* Copyright (c) 2019-07-11 10:53:20 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：d9a79147-d900-48ca-ae33-43743ec0aad4
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

   [Table("UF_TEMPLATE_STEP_SUBFLOW")]
   public class TemplateStepSubflow
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
      /// 关联UF_TEMPLATE_FLOW。
      /// <summary>
      [Column("SUBFLOW_CODE",DbType = DBType.NVarChar)]
      public string SubflowCode{ set; get;}
      /// <summary>
      /// 0同步子流程,1异步子流程
      /// <summary>
      [Column("TYPE",DbType = DBType.Int32)]
      public int Type{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_User。流程发起人。
      /// <summary>
      [Column("USER_ID",DbType = DBType.NVarChar)]
      public string UserId{ set; get;}
      /// <summary>
      /// 禁止重覆：禁止重覆
      /// <summary>
      [Column("NO_REPEAT",DbType = DBType.Int32)]
      public int NoRepeat{ set; get;}
      /// <summary>
      /// 禁止重覆的方式：0不重覆启动当前子流程,1终止正在运转的当前子流程
      /// <summary>
      [Column("NO_REPEAT_TYPE",DbType = DBType.Int32)]
      public int NoRepeatType{ set; get;}
      /// <summary>
      /// 指定步骤序号：指定发起流程的下一步处理步骤。
      /// <summary>
      [Column("FIX_STEP_SEQ",DbType = DBType.Int32)]
      public int FixStepSeq{ set; get;}
      /// <summary>
      /// 指定处理人：关联UF_TEMPLATE_USER，指定发起流程的下一步处理人。
      /// <summary>
      [Column("FIX_USER_ID",DbType = DBType.NVarChar)]
      public string FixUserId{ set; get;}
   }
}
