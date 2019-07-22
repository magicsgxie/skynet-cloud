/************************************************************************************
* Copyright (c) 2019-07-11 10:53:15 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepCycle.cs
* 版本号：  V1.0.0.0
* 唯一标识：67c26bf4-4cc9-4f0f-9c3e-f8ed5ada508c
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:15 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:15 
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

   [Table("UF_TEMPLATE_STEP_CYCLE")]
   public class TemplateStepCycle
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
      /// 事件编号
      /// <summary>
      [Column("EVENT_CODE",DbType = DBType.NVarChar)]
      public string EventCode{ set; get;}
      /// <summary>
      /// 标题
      /// <summary>
      [Column("TITLE",DbType = DBType.NVarChar)]
      public string Title{ set; get;}
      /// <summary>
      /// 间隔
      /// <summary>
      [Column("INTERVAL",DbType = DBType.Int32)]
      public int Interval{ set; get;}
      /// <summary>
      /// 次数
      /// <summary>
      [Column("TIMES",DbType = DBType.Int32)]
      public int Times{ set; get;}
      /// <summary>
      /// 事件ID
      /// <summary>
      [Column("EVENT_ID",DbType = DBType.NVarChar)]
      public string EventId{ set; get;}
      /// <summary>
      /// 返回值数据ID
      /// <summary>
      [Column("RESULT_ID",DbType = DBType.NVarChar)]
      public string ResultId{ set; get;}
      /// <summary>
      /// 0等于,1不等于,2包含,3属于,4大于,5大于等于,6小于等于,7小于
      /// <summary>
      [Column("COMPARE",DbType = DBType.Int32)]
      public int Compare{ set; get;}
      /// <summary>
      /// 数据值
      /// <summary>
      [Column("RESULT_VALUE",DbType = DBType.NVarChar)]
      public string ResultValue{ set; get;}
   }
}
