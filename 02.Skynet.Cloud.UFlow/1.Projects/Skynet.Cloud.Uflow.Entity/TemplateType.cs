/************************************************************************************
* Copyright (c) 2019-07-11 10:53:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateType.cs
* 版本号：  V1.0.0.0
* 唯一标识：37edf470-9e85-4fd4-806c-de4fc51a99a1
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:23 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:23 
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

   [Table("UF_TEMPLATE_TYPE")]
   public class TemplateType
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("GROUP_ID",DbType = DBType.NVarChar)]
      public string GroupId{ set; get;}
      /// <summary>
      /// 分类编号
      /// <summary>
      [Column("TYPE_CODE",DbType = DBType.NVarChar)]
      public string TypeCode{ set; get;}
      /// <summary>
      /// 保持唯一
      /// <summary>
      [Column("TYPE_NAME",DbType = DBType.NVarChar)]
      public string TypeName{ set; get;}
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
