/************************************************************************************
* Copyright (c) 2019-07-11 10:53:52 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：eaf8b51b-dbba-4cca-b886-af4e3551afed
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:52 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:52 
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

    [Table("UF_INSTANCE_FLOW")]
    public class InstanceFlow
    {
        /// <summary>
        /// 唯一ID
        /// <summary>
        [Id("FID")]
        public string Fid { set; get; }
        /// <summary>
        /// 关联T_UA_GROUP。
        /// <summary>
        [Column("GROUP_ID", DbType = DBType.NVarChar)]
        public string GroupId { set; get; }
        /// <summary>
        /// 关联T_WF_Flow的ID。
        /// <summary>
        [Column("FLOW_ID", DbType = DBType.NVarChar)]
        public string FlowId { set; get; }
        /// <summary>
        /// 流程版本
        /// <summary>
        [Column("VERSION", DbType = DBType.NVarChar)]
        public string Version { set; get; }
        /// <summary>
        /// 实例编号
        /// <summary>
        [Column("INSTANCE_CODE", DbType = DBType.NVarChar)]
        public string InstanceCode { set; get; }
        /// <summary>
        /// 发起日期
        /// <summary>
        [Column("START_DATE", DbType = DBType.DateTime)]
        public DateTime StartDate { set; get; }
        /// <summary>
        /// 发起人
        /// <summary>
        [Column("START_USER", DbType = DBType.NVarChar)]
        public string StartUser { set; get; }
        /// <summary>
        /// 启动时间
        /// <summary>
        [Column("BEGIN_DATE", DbType = DBType.DateTime)]
        public DateTime BeginDate { set; get; }
        /// <summary>
        /// 结束时间
        /// <summary>
        [Column("END_DATE", DbType = DBType.DateTime)]
        public DateTime? EndDate { set; get; }
        /// <summary>
        /// 以小时为单位，是以每个步骤的时长之和计。
        /// <summary>
        [Column("USED_HOURS", DbType = DBType.Int32)]
        public int UsedHours { set; get; }
        /// <summary>
        /// 0初始,1活跃,2等待,3运行,4挂起,5终止,6完成
        /// <summary>
        [Column("STATUS", DbType = DBType.Int32)]
        public int Status { set; get; }
        /// <summary>
        /// 0创建监控人列表,1流程启动事件,2步骤处理,3流程结束事件,4完成
        /// <summary>
        [Column("PROGRESS", DbType = DBType.Int32)]
        public int Progress { set; get; }
        /// <summary>
        /// 扩展字段一
        /// <summary>
        [Column("FIELD1", DbType = DBType.NVarChar)]
        public string Field1 { set; get; }
        /// <summary>
        /// 扩展字段二
        /// <summary>
        [Column("FIELD2", DbType = DBType.NVarChar)]
        public string Field2 { set; get; }
        /// <summary>
        /// 扩展字段三
        /// <summary>
        [Column("FIELD3", DbType = DBType.NVarChar)]
        public string Field3 { set; get; }
        /// <summary>
        /// 扩展字段四
        /// <summary>
        [Column("FIELD4", DbType = DBType.NVarChar)]
        public string Field4 { set; get; }
        /// <summary>
        /// 扩展字段五
        /// <summary>
        [Column("FIELD5", DbType = DBType.NVarChar)]
        public string Field5 { set; get; }
        /// <summary>
        /// 扩展字段六
        /// <summary>
        [Column("FIELD6", DbType = DBType.NVarChar)]
        public string Field6 { set; get; }
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
        [Column("RUN_DATA", DbType = DBType.NVarChar)]
        public string RunData { set; get; }

        [Ignore]
        public IList<InstanceStep> InstanceSteps { set; get; } = new List<InstanceStep>();


        [Ignore]
        public IList<TemplateStep> TemplateSteps { set; get; } = new List<TemplateStep>();
    }
}
