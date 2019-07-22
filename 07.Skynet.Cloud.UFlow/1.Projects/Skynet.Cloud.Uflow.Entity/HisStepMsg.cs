/************************************************************************************
* Copyright (c) 2019-07-11 10:53:43 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.HisStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：21362d44-3ee4-45ff-a1a1-23b5e552e474
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:43 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:43 
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

   [Table("UF_HIS_STEP_MSG")]
   public class HisStepMsg
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
      /// 关联UF_TEMPLATE_MSG
      /// <summary>
      [Column("STEP_MSG_ID",DbType = DBType.NVarChar)]
      public string StepMsgId{ set; get;}
      /// <summary>
      /// 接收人编号
      /// <summary>
      [Column("USER_CODE",DbType = DBType.NVarChar)]
      public string UserCode{ set; get;}
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
      /// 0失败,1成功
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
      /// 消息内容
      /// <summary>
      [Column("CONTENT",DbType = DBType.NVarChar)]
      public string Content{ set; get;}
      /// <summary>
      /// 0系统消息,1邮件,2短信
      /// <summary>
      [Column("SEND_WAY",DbType = DBType.Int32)]
      public int? SendWay{ set; get;}
      /// <summary>
      /// 发送用户编号
      /// <summary>
      [Column("SEND_USER_CODE",DbType = DBType.NVarChar)]
      public string SendUserCode{ set; get;}
      /// <summary>
      /// 消息类型：1，步骤预处理消息。2，流程模板配置消息。3,预处理超时提醒，4，超时提醒
      /// <summary>
      [Column("MSG_TYPE",DbType = DBType.Int32)]
      public int? MsgType{ set; get;}
   }
}
