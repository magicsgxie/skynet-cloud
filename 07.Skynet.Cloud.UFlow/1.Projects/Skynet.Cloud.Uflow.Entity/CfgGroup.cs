/************************************************************************************
* Copyright (c) 2019-07-11 10:54:04 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.CfgGroup.cs
* 版本号：  V1.0.0.0
* 唯一标识：8ea596eb-2b53-4e37-8a19-2f9cf70c9bc4
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:54:04 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:54:04 
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

   [Table("UF_CFG_GROUP")]
   public class CfgGroup
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 组织名称
      /// <summary>
      [Column("GROUP_NAME",DbType = DBType.NVarChar)]
      public string GroupName{ set; get;}
      /// <summary>
      /// 组织负责人
      /// <summary>
      [Column("GROUP_USER",DbType = DBType.NVarChar)]
      public string GroupUser{ set; get;}
      /// <summary>
      /// 联系电话
      /// <summary>
      [Column("TEL",DbType = DBType.NVarChar)]
      public string Tel{ set; get;}
      /// <summary>
      /// 邮件地址
      /// <summary>
      [Column("EMAIL",DbType = DBType.NVarChar)]
      public string Email{ set; get;}
      /// <summary>
      /// 描述
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
      public DateTime? CreateDate{ set; get;}
      /// <summary>
      /// 编辑者
      /// <summary>
      [Column("EDITOR",DbType = DBType.NVarChar)]
      public string Editor{ set; get;}
      /// <summary>
      /// 编辑日期
      /// <summary>
      [Column("EDIT_DATE",DbType = DBType.DateTime)]
      public DateTime? EditDate{ set; get;}
   }
}
