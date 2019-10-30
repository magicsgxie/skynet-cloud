/************************************************************************************
* Copyright (c) 2019-07-11 10:53:32 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.CfgWorktime.cs
* 版本号：  V1.0.0.0
* 唯一标识：150eb665-4afe-48b7-addd-055d6516ab0b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:32 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:32 
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

   [Table("UF_CFG_WORKTIME")]
   public class CfgWorktime
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表工作日详情ID
      /// <summary>
      [Column("WORKDAYDTL_ID",DbType = DBType.NVarChar)]
      public string WorkdaydtlId{ set; get;}
      /// <summary>
      /// 开始时间
      /// <summary>
      [Column("BEGINTIME",DbType = DBType.NVarChar)]
      public string Begintime{ set; get;}
      /// <summary>
      /// 结束时间
      /// <summary>
      [Column("ENDTIME",DbType = DBType.NVarChar)]
      public string Endtime{ set; get;}
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
