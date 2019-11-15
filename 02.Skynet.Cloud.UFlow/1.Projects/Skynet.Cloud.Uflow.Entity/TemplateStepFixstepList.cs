/************************************************************************************
* Copyright (c) 2019-07-11 10:53:17 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStepFixstepList.cs
* 版本号：  V1.0.0.0
* 唯一标识：0015732c-2f71-4766-9b4c-d70b6fb42624
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:17 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:17 
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

   [Table("UF_TEMPLATE_STEP_FIXSTEP_LIST")]
   public class TemplateStepFixstepList
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 主表步骤设置表ID
      /// <summary>
      [Column("STEP_ID",DbType = DBType.NVarChar)]
      public string StepId{ set; get;}
      /// <summary>
      /// 可选步骤序号
      /// <summary>
      [Column("SELECT_STEP_SEQ",DbType = DBType.Int32)]
      public int SelectStepSeq{ set; get;}
      /// <summary>
      /// 可选步骤名称
      /// <summary>
      [Column("SELECT_STEP_NAME",DbType = DBType.NVarChar)]
      public string SelectStepName{ set; get;}
      /// <summary>
      /// 当前表单名称
      /// <summary>
      [Column("SELECT_FORM_NAME",DbType = DBType.NVarChar)]
      public string SelectFormName{ set; get;}
      /// <summary>
      /// 0否，1是
      /// <summary>
      [Column("IS_RETURN",DbType = DBType.Int32)]
      public bool IsReturn{ set; get;}
      /// <summary>
      /// 必须指定下一步处理人
      /// <summary>
      [Column("IS_FORM_NEXTMAN",DbType = DBType.Int32)]
      public bool IsFormNextman{ set; get;}
      /// <summary>
      /// 处理人必须指定
      /// <summary>
      [Column("IS_FORM_NEXTMAN_MUST",DbType = DBType.Int32)]
      public bool IsFormNextmanMust { set; get;}
      /// <summary>
      /// 描述
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}

      public TemplateStepFixstepList Clone()
        {
            return new TemplateStepFixstepList()
            {
                Fid = this.Fid,
                StepId = this.StepId,
                SelectStepSeq = this.SelectStepSeq,
                SelectStepName = this.SelectStepName,
                SelectFormName = this.SelectFormName,
                IsReturn = this.IsReturn,
                IsFormNextman = this.IsFormNextman,
                IsFormNextmanMust = this.IsFormNextmanMust,
                Remark = this.Remark
            };

        }
    }
}
