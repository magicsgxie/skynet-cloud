/************************************************************************************
* Copyright (c) 2019-07-11 10:53:45 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.HisStepNextSeluser.cs
* 版本号：  V1.0.0.0
* 唯一标识：b4fe513c-90a6-4174-9583-8c4b13d61cc2
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:45 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:45 
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

   [Table("UF_HIS_STEP_NEXT_SELUSER")]
   public class HisStepNextSeluser
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Column("FID",DbType = DBType.NVarChar)]
      public string Fid{ set; get;}
      /// <summary>
      /// 流程实例步骤ID
      /// <summary>
      [Column("INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string InstanceStepId{ set; get;}
      /// <summary>
      /// 关联UF_TEMPLATE_STEP_NEXTSTEP_USER
      /// <summary>
      [Column("NEXTSTEP_USER_ID",DbType = DBType.NVarChar)]
      public string NextstepUserId{ set; get;}
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
      /// 人员编号
      /// <summary>
      [Column("USER_CODE",DbType = DBType.NVarChar)]
      public string UserCode{ set; get;}
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
