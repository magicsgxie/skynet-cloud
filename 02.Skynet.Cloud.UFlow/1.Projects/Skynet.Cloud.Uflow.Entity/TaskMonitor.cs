/************************************************************************************
* Copyright (c) 2019-07-11 10:53:26 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TaskMonitor.cs
* 版本号：  V1.0.0.0
* 唯一标识：b5a110c9-db74-4502-8f8d-14273a5d5ce7
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:26 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:26 
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

   [Table("UF_TASK_MONITOR")]
   public class TaskMonitor
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 所属组织
      /// <summary>
      [Column("GROUP_ID",DbType = DBType.NVarChar)]
      public string GroupId{ set; get;}
      /// <summary>
      /// 流程实例ID
      /// <summary>
      [Column("INSTANCE_FLOW_ID",DbType = DBType.NVarChar)]
      public string InstanceFlowId{ set; get;}
      /// <summary>
      /// 实例步骤ID
      /// <summary>
      [Column("INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string InstanceStepId{ set; get;}
      /// <summary>
      /// 0挂起,1解挂,2终止,3撤消
      /// <summary>
      [Column("STATUS",DbType = DBType.Int32)]
      public int Status{ set; get;}
      /// <summary>
      /// 0流程操作,1步骤操作
      /// <summary>
      [Column("TYPE",DbType = DBType.Int32)]
      public int Type{ set; get;}
      /// <summary>
      /// -1未运行,0失败,1成功
      /// <summary>
      [Column("RESULT",DbType = DBType.Int32)]
      public int Result{ set; get;}
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
