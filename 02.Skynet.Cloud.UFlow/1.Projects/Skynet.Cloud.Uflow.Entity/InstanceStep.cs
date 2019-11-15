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



namespace UWay.Skynet.Cloud.Uflow.Entity
{
    using System;
    using System.Collections.Generic;
    using UWay.Skynet.Cloud.Data;

    [Table("UF_INSTANCE_STEP")]
    public class InstanceStep
    {
        /// <summary>
        /// 唯一ID
        /// <summary>
        [Id("FID")]
        public string Fid { set; get; }
        /// <summary>
        /// 关联T_UA_GROUP。
        /// <summary>
        [Column("INSTANCE_FLOW_ID", DbType = DBType.NVarChar)]
        public string InstanceFlowId { set; get; }
        /// <summary>
        /// 流程步骤ID
        /// <summary>
        [Column("STEP_ID", DbType = DBType.NVarChar)]
        public string StepId { set; get; }
        /// <summary>
        /// 就算上一步有多个步骤，也只记录最后处理的步骤
        /// <summary>
        [Column("PREVIOUS_INSTANCE_STEP_ID", DbType = DBType.NVarChar)]
        public string PreviousInstanceStepId { set; get; }
        /// <summary>
        /// 下一个步骤实例ID
        /// <summary>
        [Column("NEXT_INSTANCE_STEP_ID", DbType = DBType.NVarChar)]
        public string NextInstanceStepId { set; get; }
        /// <summary>
        /// 启动日期
        /// <summary>
        [Column("BEGIN_DATE", DbType = DBType.DateTime)]
        public DateTime BeginDate { set; get; }
        /// <summary>
        /// 结束日期
        /// <summary>
        [Column("END_DATE", DbType = DBType.DateTime)]
        public DateTime? EndDate { set; get; }
        /// <summary>
        /// 以小时为单位
        /// <summary>
        [Column("USED_HOURS", DbType = DBType.Int32)]
        public int UsedHours { set; get; }
        /// <summary>
        /// 0未处理,1通过,2退回,3不通过
        /// <summary>
        [Column("RESULT", DbType = DBType.Int32)]
        public int Result { set; get; }
        /// <summary>
        /// 0初始,1活跃,2等待,3运行,4挂起,5终止,6完成,7退回。
        /// <summary>
        [Column("STATUS", DbType = DBType.Int32)]
        public int Status { set; get; }
        /// <summary>
        /// 0前期事件,1生成处理人,2前期消息,3生成可选处理人,4生成指定步骤可选处理人,5程序-子流程,6程序-循环事件,7用户处理,8后期完成事件,9后期退回事件,10转出条件,11后期消息,12转发控制,13跳转中,14完成
        /// <summary>
        [Column("PROGRESS", DbType = DBType.Int32)]
        public int Progress { set; get; }
        /// <summary>
        /// 创建者
        /// <summary>
        [Column("CREATOR", DbType = DBType.NVarChar)]
        public string Creator { set; get; }
        /// <summary>
        /// 创建日期
        /// <summary>
        [Column("CREATE_DATE", DbType = DBType.DateTime)]
        public DateTime CreateDate { set; get; }
        /// <summary>
        /// 编辑者
        /// <summary>
        [Column("EDITOR", DbType = DBType.NVarChar)]
        public string Editor { set; get; }
        /// <summary>
        /// 编辑日期
        /// <summary>
        [Column("EDIT_DATE", DbType = DBType.DateTime)]
        public DateTime EditDate { set; get; }


        /// <summary>
        /// 循环处理表
        /// </summary>
        [Ignore]
        public List<InstanceStepCycle> InstanceStepCycles { set; get; } = new List<InstanceStepCycle>();

        /// <summary>
        /// 步骤转入转出条件结果
        /// </summary>
        [Ignore]
        public List<InstanceStepCondition> InstanceStepConditions { set; get; } = new List<InstanceStepCondition>();


        /// <summary>
        /// 步骤前后期事件结果
        /// </summary>
        [Ignore]
        public List<InstanceStepEvent> InstanceStepEvents { set; get; } = new List<InstanceStepEvent>();


        /// <summary>
        /// 步骤消息处理
        /// </summary>
        [Ignore]
        public List<InstanceStepMsg> InstanceStepMsgs { set; get; } = new List<InstanceStepMsg>();

        /// <summary>
        /// 子流程表
        /// </summary>
        [Ignore]
        public List<InstanceStepSubflow> InstanceStepSubflows { set; get; } = new List<InstanceStepSubflow>();

        /// <summary>
        /// 步骤转发控制
        /// </summary>
        [Ignore]
        public List<InstanceStepTransfer> InstanceStepTransfers { set; get; } = new List<InstanceStepTransfer>();



        /// <summary>
        /// 实例步骤处理人
        /// </summary>
        [Ignore]
        public List<InstanceStepUser> InstanceStepUsers { set; get; } = new List<InstanceStepUser>();

        /// <summary>
        /// 指定的下一步步骤
        /// </summary>
        [Ignore]
        public List<InstanceStepFixstep> InstanceStepFixsteps { set; get; } = new List<InstanceStepFixstep>();

        /// <summary>
        /// 退回到的步骤
        /// </summary>
        [Ignore]
        public List<InstanceStepReturnstep> InstanceStepReturnsteps { set; get; } = new List<InstanceStepReturnstep>();


        /// <summary>
        /// / 指定步骤的可选人员
        /// </summary>
        [Ignore]
        public List<InstanceStepFixSeluser> InstanceStepFixSelusers { get; set; } = new List<InstanceStepFixSeluser>();

        /// <summary>
        /// 下一步可选人员
        /// </summary>
        [Ignore]
        public List<InstanceStepNextSeluser> InstanceStepNextSelusers { get; set; } = new List<InstanceStepNextSeluser>();

        /// <summary>
        /// 指定的下一步处理人
        /// </summary>
        [Ignore]
        public List<InstanceStepNextstepUser> InstanceStepNextstepUsers { get; set; } = new List<InstanceStepNextstepUser>();
    }
}
