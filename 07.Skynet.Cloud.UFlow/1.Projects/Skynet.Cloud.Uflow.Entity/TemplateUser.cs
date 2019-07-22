/************************************************************************************
* Copyright (c) 2019-07-11 10:53:23 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：ca03ab1b-0e15-4902-a99b-47d69aed5065
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

   [Table("UF_TEMPLATE_USER")]
   public class TemplateUser
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 流程ID
      /// <summary>
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      /// <summary>
      /// 0流程发起人,1指定人员,2指定组织,3指定岗位,4指定角色,5某一步骤的选择处理者,6某一步骤的实际处理者,7从数据项中获取,8所有人
      /// <summary>
      [Column("USER_TYPE",DbType = DBType.Int32)]
      public int UserType{ set; get;}
      /// <summary>
      /// 人员值
      /// <summary>
      [Column("USER_VALUE",DbType = DBType.NVarChar)]
      public string UserValue{ set; get;}
      /// <summary>
      /// 0不启用,1启用
      /// <summary>
      [Column("IS_USE_RELATION",DbType = DBType.Int32)]
      public int IsUseRelation{ set; get;}
      /// <summary>
      /// 0所在部门,1所在部门领导,2上级部门,3上级部门领导,4下级部门,5下级部门领导
      /// <summary>
      [Column("RELATION",DbType = DBType.Int32)]
      public int Relation{ set; get;}
      /// <summary>
      /// 描述
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
   }
}
