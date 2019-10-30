/************************************************************************************
* Copyright (c) 2019-07-11 10:53:10 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：cb31487e-1a38-49b9-8cf1-d45f93e13cc0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:10 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:10 
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

   [Table("UF_TEMPLATE_MSG")]
   public class TemplateMsg
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
      /// 标题
      /// <summary>
      [Column("TITLE",DbType = DBType.NVarChar)]
      public string Title{ set; get;}
      /// <summary>
      /// 0步骤开始后,1步骤结束后  (立即提醒)
      /// <summary>
      [Column("SEND_TYPE",DbType = DBType.Int32)]
      public int SendType{ set; get;}
      /// <summary>
      /// 多次提醒限制数
      /// <summary>
      [Column("SEND_TIMES",DbType = DBType.Int32)]
      public int SendTimes{ set; get;}
      /// <summary>
      /// 间隔时间
      /// <summary>
      [Column("INTERVAL",DbType = DBType.Int32)]
      public int Interval{ set; get;}
      /// <summary>
      /// 立即提醒
      /// <summary>
      [Column("IS_INSTANT",DbType = DBType.Int32)]
      public int? IsInstant{ set; get;}
      /// <summary>
      /// 接收对象
      /// <summary>
      [Column("RECEIVER",DbType = DBType.NVarChar)]
      public string Receiver{ set; get;}
      /// <summary>
      /// 0系统消息,1邮件,2短信
      /// <summary>
      [Column("SEND_WAY",DbType = DBType.Int32)]
      public int SendWay{ set; get;}
      /// <summary>
      /// 消息内容
      /// <summary>
      [Column("CONTENT",DbType = DBType.NVarChar)]
      public string Content{ set; get;}
   }
}
