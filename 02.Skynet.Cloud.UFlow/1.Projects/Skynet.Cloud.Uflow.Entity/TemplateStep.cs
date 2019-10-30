/************************************************************************************
* Copyright (c) 2019-07-11 10:53:14 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateStep.cs
* 版本号：  V1.0.0.0
* 唯一标识：1375d16f-b313-42ed-80ed-087ae17124c5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:14 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:14 
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

   [Table("UF_TEMPLATE_STEP")]
   public class TemplateStep
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
      /// 步骤序号
      /// <summary>
      [Column("SEQ",DbType = DBType.Int32)]
      public int Seq{ set; get;}
      /// <summary>
      /// 步骤名称
      /// <summary>
      [Column("STEP_NAME",DbType = DBType.NVarChar)]
      public string StepName{ set; get;}
      /// <summary>
      /// 0开始,1自由,2人工,3路由,4临时,5子流程,6结束
      /// <summary>
      [Column("STEP_TYPE",DbType = DBType.Int32)]
      public int StepType{ set; get;}
      /// <summary>
      /// 转入条件启用或条件投票,0否,1是
      /// <summary>
      [Column("IS_VOTE_IN",DbType = DBType.Int32)]
      public int IsVoteIn{ set; get;}
      /// <summary>
      /// 转入投票数
      /// <summary>
      [Column("VOTE_IN_NUM",DbType = DBType.Int32)]
      public int VoteInNum{ set; get;}
      /// <summary>
      /// 转出条件启用或条件投票,0否,1是
      /// <summary>
      [Column("IS_VOTE_OUT",DbType = DBType.Int32)]
      public int IsVoteOut{ set; get;}
      /// <summary>
      /// 转出投票数
      /// <summary>
      [Column("VOTE_OUT_NUM",DbType = DBType.Int32)]
      public int VoteOutNum{ set; get;}
      /// <summary>
      /// 多主办人时，只有最先处理者有效
      /// <summary>
      [Column("IS_USER_MAJOR_MUL",DbType = DBType.Int32)]
      public int IsUserMajorMul{ set; get;}
      /// <summary>
      /// 多汇签人时，只要有一人处理即可
      /// <summary>
      [Column("IS_USER_MINOR_MUL",DbType = DBType.Int32)]
      public int IsUserMinorMul{ set; get;}
      /// <summary>
      /// 如果上一步指定了处理人员，则与处理人列表中的人员共同处理
      /// <summary>
      [Column("IS_USER_ALL_DEAL",DbType = DBType.Int32)]
      public int IsUserAllDeal{ set; get;}
      /// <summary>
      /// 即使上一步指定了处理人员，也只有处理人列表中的人员有效
      /// <summary>
      [Column("IS_USER_CURRENT_DEAL",DbType = DBType.Int32)]
      public int IsUserCurrentDeal{ set; get;}
      /// <summary>
      /// 表单名称
      /// <summary>
      [Column("FORM_NAME",DbType = DBType.NVarChar)]
      public string FormName{ set; get;}
      /// <summary>
      /// 指定下一个处理步骤
      /// <summary>
      [Column("IS_FORM_NEXTSTEP",DbType = DBType.Int32)]
      public int IsFormNextstep{ set; get;}
      /// <summary>
      /// 要求必须指定
      /// <summary>
      [Column("IS_FORM_NEXTSTEP_MUST",DbType = DBType.Int32)]
      public int IsFormNextstepMust{ set; get;}
      /// <summary>
      /// 必须指定下一步处理人
      /// <summary>
      [Column("IS_FORM_NEXTMAN",DbType = DBType.Int32)]
      public int IsFormNextman{ set; get;}
      /// <summary>
      /// 处理人必须指定
      /// <summary>
      [Column("IS_FORM_NEXTMAN_MUST",DbType = DBType.Int32)]
      public int IsFormNextmanMust{ set; get;}
      /// <summary>
      /// 必须填写主办或汇签意见
      /// <summary>
      [Column("IS_FORM_SUGGESTION",DbType = DBType.Int32)]
      public int IsFormSuggestion{ set; get;}
      /// <summary>
      /// 意见仅对发起人和填写人可见
      /// <summary>
      [Column("IS_FORM_SUGGESTION_VISIBLE",DbType = DBType.Int32)]
      public int IsFormSuggestionVisible{ set; get;}
      /// <summary>
      /// 允许转办
      /// <summary>
      [Column("IS_FORM_TRANSFER",DbType = DBType.Int32)]
      public int IsFormTransfer{ set; get;}
      /// <summary>
      /// 允许退回到上一步
      /// <summary>
      [Column("IS_FORM_RETURN",DbType = DBType.Int32)]
      public int IsFormReturn{ set; get;}
      /// <summary>
      /// 允许退回到发起步骤
      /// <summary>
      [Column("IS_FORM_RETURN_FIRST",DbType = DBType.Int32)]
      public int IsFormReturnFirst{ set; get;}
      /// <summary>
      /// 允许退回到完成步骤
      /// <summary>
      [Column("IS_FORM_RETURN_FINISH",DbType = DBType.Int32)]
      public int IsFormReturnFinish{ set; get;}
      /// <summary>
      /// 说明
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
      /// <summary>
      /// 扩展字段一
      /// <summary>
      [Column("FIELD1",DbType = DBType.NVarChar)]
      public string Field1{ set; get;}
      /// <summary>
      /// 扩展字段二
      /// <summary>
      [Column("FIELD2",DbType = DBType.NVarChar)]
      public string Field2{ set; get;}
      /// <summary>
      /// 扩展字段三
      /// <summary>
      [Column("FIELD3",DbType = DBType.NVarChar)]
      public string Field3{ set; get;}
      /// <summary>
      /// 扩展字段四
      /// <summary>
      [Column("FIELD4",DbType = DBType.NVarChar)]
      public string Field4{ set; get;}
      /// <summary>
      /// 扩展字段五
      /// <summary>
      [Column("FIELD5",DbType = DBType.NVarChar)]
      public string Field5{ set; get;}
      /// <summary>
      /// 扩展字段六
      /// <summary>
      [Column("FIELD6",DbType = DBType.NVarChar)]
      public string Field6{ set; get;}
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
      /// 退回表单名称
      /// <summary>
      [Column("RETURN_FORM_NAME",DbType = DBType.NVarChar)]
      public string ReturnFormName{ set; get;}
      /// <summary>
      /// 允许临时子流程
      /// <summary>
      [Column("IS_TEMP_SUBFLOW",DbType = DBType.Int32)]
      public int IsTempSubflow{ set; get;}
   }
}
