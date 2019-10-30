/************************************************************************************
* Copyright (c) 2019-07-11 10:53:34 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.FormMenu.cs
* 版本号：  V1.0.0.0
* 唯一标识：31e226da-1d0a-41b5-a16f-7b8e44a8f096
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:34 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:34 
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

   /// <summary>
   /// 流程业务
   /// </summary>
   [Table("UF_FORM_MENU")]
   public class FormMenu
   {
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      [Column("MENU_CODE",DbType = DBType.NVarChar)]
      public string MenuCode{ set; get;}
      [Column("MENU_NAME",DbType = DBType.NVarChar)]
      public string MenuName{ set; get;}
      [Column("LEVELS",DbType = DBType.Int32)]
      public int Levels{ set; get;}
      [Column("SEQ",DbType = DBType.Int32)]
      public int Seq{ set; get;}
      [Column("IMG",DbType = DBType.NVarChar)]
      public string Img{ set; get;}
      [Column("URL",DbType = DBType.NVarChar)]
      public string Url{ set; get;}
      [Column("PARENT_MENU_CODE",DbType = DBType.NVarChar)]
      public string ParentMenuCode{ set; get;}
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
      [Column("CREATOR",DbType = DBType.NVarChar)]
      public string Creator{ set; get;}
      [Column("CREATE_DATE",DbType = DBType.DateTime)]
      public DateTime CreateDate{ set; get;}
      [Column("EDITOR",DbType = DBType.NVarChar)]
      public string Editor{ set; get;}
      [Column("EDIT_DATE",DbType = DBType.DateTime)]
      public DateTime EditDate{ set; get;}
   }
}
