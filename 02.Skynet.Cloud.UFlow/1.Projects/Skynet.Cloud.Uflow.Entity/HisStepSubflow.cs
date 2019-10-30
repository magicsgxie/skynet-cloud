/************************************************************************************
* Copyright (c) 2019-07-11 10:53:28 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.HisStepSubflow.cs
* 版本号：  V1.0.0.0
* 唯一标识：84c880fe-4549-48c2-a387-db442fea8bc9
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

   [Table("UF_HIS_STEP_SUBFLOW")]
   public class HisStepSubflow
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string InstanceStepId{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_STEP_SUBFLOW
      /// <summary>
      [Column("STEP_SUBFLOW_ID",DbType = DBType.NVarChar)]
      public string StepSubflowId{ set; get;}
      /// <summary>
      /// 启动时间
      /// <summary>
      [Column("BEGIN_DATE",DbType = DBType.DateTime)]
      public DateTime BeginDate{ set; get;}
      /// <summary>
      /// 结束时间
      /// <summary>
      [Column("END_DATE",DbType = DBType.DateTime)]
      public DateTime? EndDate{ set; get;}
      /// <summary>
      /// 关联UF_INSTANCE_FLOW，但是它是指另一个实例，要与本实例区分开
      /// <summary>
      [Column("INSTANCE_SUBFLOW_ID",DbType = DBType.NVarChar)]
      public string InstanceSubflowId{ set; get;}
      /// <summary>
      /// 0未完成,1完成
      /// <summary>
      [Column("STATUS",DbType = DBType.Int32)]
      public int Status{ set; get;}
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
      /// <summary>
      /// 0正常子流程,1临时子流程
      /// <summary>
      [Column("SUBFLOW_TYPE",DbType = DBType.Int32)]
      public int SubflowType{ set; get;}
   }
}
