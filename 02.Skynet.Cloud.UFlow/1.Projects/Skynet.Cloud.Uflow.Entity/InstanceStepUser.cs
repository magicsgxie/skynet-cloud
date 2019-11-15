/************************************************************************************
* Copyright (c) 2019-07-11 10:52:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceStepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：df01a6bf-ba35-4c0c-b153-aa8182b942b1
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:52:44 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:52:44 
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

   [Table("UF_INSTANCE_STEP_USER")]
   public class InstanceStepUser
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 实例步骤ID
      /// <summary>
      [Column("INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string InstanceStepId{ set; get;}
      /// <summary>
      /// 流程步骤处理人ID
      /// <summary>
      [Column("STEP_USER_ID",DbType = DBType.NVarChar)]
      public string StepUserId{ set; get;}
      /// <summary>
      /// 0主办,1汇签,2协办,3抄送
      /// <summary>
      [Column("USER_TYPE",DbType = DBType.Int32)]
      public int UserType{ set; get;}
      /// <summary>
      /// 人员编号
      /// <summary>
      [Column("USER_CODE",DbType = DBType.NVarChar)]
      public string UserCode{ set; get;}
      /// <summary>
      /// 0未处理,1通过,2退回,3转办,4不通过,5查看,6保存
      /// <summary>
      [Column("STATUS",DbType = DBType.Int32)]
      public int Status{ set; get;}
      /// <summary>
      /// 表单名称
      /// <summary>
      [Column("FORM_NAME",DbType = DBType.NVarChar)]
      public string FormName{ set; get;}
      /// <summary>
      /// 表单数据
      /// <summary>
      [Column("DATA",DbType = DBType.Text)]
      public string Data{ set; get;}
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
      /// <summary>
      /// 保存用户在工单处理时的选择信息
      /// <summary>
      [Column("SELECT_INFO",DbType = DBType.NVarChar)]
      public string SelectInfo{ set; get;}
      [Column("CONTENT",DbType = DBType.NVarChar)]
      public string Content{ set; get;}

        public InstanceStepUser Clone()
        {
            return new InstanceStepUser()
            {
                Fid = this.Fid,
                InstanceStepId = this.InstanceStepId,
                StepUserId = this.StepUserId,
                UserType = this.UserType,
                UserCode = this.UserCode,
                Status = this.Status,
                FormName = FormName,
                Data = Data,
                Creator = Creator,
                EditDate = EditDate,
                Editor = Editor,
                CreateDate = CreateDate,
                Content = Content
            };
        }
    }
}
