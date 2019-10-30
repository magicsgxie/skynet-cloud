/************************************************************************************
* Copyright (c) 2019-07-11 10:53:31 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.CfgWorkdayDtl.cs
* 版本号：  V1.0.0.0
* 唯一标识：60561ca2-f383-45ba-bd91-a9fea978cb6d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:31 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:31 
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

   [Table("UF_CFG_WORKDAY_DTL")]
   public class CfgWorkdayDtl
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表工作日ID
      /// <summary>
      [Column("WORKDAY_ID",DbType = DBType.NVarChar)]
      public string WorkdayId{ set; get;}
      /// <summary>
      /// 日数
      /// <summary>
      [Column("DAY",DbType = DBType.Int32)]
      public int Day{ set; get;}
      /// <summary>
      /// 0非工作日,1是工作日
      /// <summary>
      [Column("WORKDAY",DbType = DBType.Int32)]
      public int Workday{ set; get;}
      /// <summary>
      /// 时长
      /// <summary>
      [Column("HOURS",DbType = DBType.Int32)]
      public int Hours{ set; get;}
      /// <summary>
      /// 说明
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
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
