/************************************************************************************
* Copyright (c) 2019-07-11 10:53:24 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.UserReplace.cs
* 版本号：  V1.0.0.0
* 唯一标识：9de10a83-027e-4e49-8c88-d323cc7f80a0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:24 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:24 
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

   [Table("UF_USER_REPLACE")]
   public class UserReplace
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联UF_CFG_GROUP。
      /// <summary>
      [Column("GROUP_ID",DbType = DBType.NVarChar)]
      public string GroupId{ set; get;}
      /// <summary>
      /// 代办发出人
      /// <summary>
      [Column("SEND_USER_CODE",DbType = DBType.NVarChar)]
      public string SendUserCode{ set; get;}
      /// <summary>
      /// 代办人
      /// <summary>
      [Column("REPLACE_USER_CODE",DbType = DBType.NVarChar)]
      public string ReplaceUserCode{ set; get;}
      /// <summary>
      /// 开始日期
      /// <summary>
      [Column("BEGINDATE",DbType = DBType.DateTime)]
      public DateTime Begindate{ set; get;}
      /// <summary>
      /// 结束日期
      /// <summary>
      [Column("ENDDATE",DbType = DBType.DateTime)]
      public DateTime Enddate{ set; get;}
      /// <summary>
      /// 启用
      /// <summary>
      [Column("ISUSED",DbType = DBType.Int32)]
      public int Isused{ set; get;}
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
