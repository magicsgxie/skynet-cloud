/************************************************************************************
* Copyright (c) 2019-07-11 10:53:26 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：2d0c8595-d6e4-4bb0-b31b-46ec24fe57dc
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:26 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:26 
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

   [Table("UF_TEMPLATE_AUTHORITY")]
   public class TemplateAuthority
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表流程表ID
      /// <summary>
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      /// <summary>
      /// 0发起权限,1监控权限
      /// <summary>
      [Column("AUTHORITY_TYPE",DbType = DBType.Int32)]
      public int AuthorityType{ set; get;}
      /// <summary>
      /// 0组织结构,1岗位,2角色,3人员
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
