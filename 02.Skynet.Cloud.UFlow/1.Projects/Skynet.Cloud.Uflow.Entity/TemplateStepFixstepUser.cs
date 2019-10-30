/************************************************************************************
* Copyright (c) 2019-07-11 10:53:18 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepFixstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：a6f391ed-f3bb-4092-8978-9cf9c44bf851
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:18 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:18 
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

   [Table("UF_TEMPLATE_STEP_FIXSTEP_USER")]
   public class TemplateStepFixstepUser
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表可选步骤设置表ID
      /// <summary>
      [Column("FIXSTEP_ID",DbType = DBType.NVarChar)]
      public string FixstepId{ set; get;}
      /// <summary>
      /// 0按组织结构,1按岗位,2按角色,3按人员
      /// <summary>
      [Column("USER_TYPE",DbType = DBType.Int32)]
      public int UserType{ set; get;}
      /// <summary>
      /// 人员值
      /// <summary>
      [Column("USER_VALUE",DbType = DBType.NVarChar)]
      public string UserValue{ set; get;}
      /// <summary>
      /// 描述
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
   }
}
