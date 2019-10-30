/************************************************************************************
* Copyright (c) 2019-07-11 10:53:54 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceStep.cs
* 版本号：  V1.0.0.0
* 唯一标识：f264dfbe-3d90-4688-93ea-05118f2012f5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:54 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:54 
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

   [Table("UF_INSTANCE_STEP")]
   public class InstanceStep
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("INSTANCE_FLOW_ID",DbType = DBType.NVarChar)]
      public string InstanceFlowId{ set; get;}
      /// <summary>
      /// 流程步骤ID
      /// <summary>
      [Column("STEP_ID",DbType = DBType.NVarChar)]
      public string StepId{ set; get;}
      /// <summary>
      /// 就算上一步有多个步骤，也只记录最后处理的步骤
      /// <summary>
      [Column("PREVIOUS_INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string PreviousInstanceStepId{ set; get;}
      /// <summary>
      /// 下一个步骤实例ID
      /// <summary>
      [Column("NEXT_INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string NextInstanceStepId{ set; get;}
      /// <summary>
      /// 启动日期
      /// <summary>
      [Column("BEGIN_DATE",DbType = DBType.DateTime)]
      public DateTime BeginDate{ set; get;}
      /// <summary>
      /// 结束日期
      /// <summary>
      [Column("END_DATE",DbType = DBType.DateTime)]
      public DateTime? EndDate{ set; get;}
      /// <summary>
      /// 以小时为单位
      /// <summary>
      [Column("USED_HOURS",DbType = DBType.Int32)]
      public int UsedHours{ set; get;}
      /// <summary>
      /// 0未处理,1通过,2退回,3不通过
      /// <summary>
      [Column("RESULT",DbType = DBType.Int32)]
      public int Result{ set; get;}
      /// <summary>
      /// 0初始,1活跃,2等待,3运行,4挂起,5终止,6完成,7退回。
      /// <summary>
      [Column("STATUS",DbType = DBType.Int32)]
      public int Status{ set; get;}
      /// <summary>
      /// 0前期事件,1生成处理人,2前期消息,3生成可选处理人,4生成指定步骤可选处理人,5程序-子流程,6程序-循环事件,7用户处理,8后期完成事件,9后期退回事件,10转出条件,11后期消息,12转发控制,13跳转中,14完成
      /// <summary>
      [Column("PROGRESS",DbType = DBType.Int32)]
      public int Progress{ set; get;}
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
