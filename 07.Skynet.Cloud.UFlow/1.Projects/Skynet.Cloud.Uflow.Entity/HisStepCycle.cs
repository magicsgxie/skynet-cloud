/************************************************************************************
* Copyright (c) 2019-07-11 10:53:41 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.HisStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：47e8f293-df77-4643-927b-a6a5c0ab6d41
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:41 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:41 
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

   [Table("UF_HIS_STEP_CYCLE")]
   public class HisStepCycle
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string InstanceStepId{ set; get;}
      /// <summary>
      /// 流程步骤循环处理ID
      /// <summary>
      [Column("STEP_CYCLE_ID",DbType = DBType.NVarChar)]
      public string StepCycleId{ set; get;}
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
      /// 次数
      /// <summary>
      [Column("RECCNT",DbType = DBType.Int32)]
      public int Reccnt{ set; get;}
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
      /// 结果值
      /// <summary>
      [Column("RESULT",DbType = DBType.NVarChar)]
      public string Result{ set; get;}
   }
}
