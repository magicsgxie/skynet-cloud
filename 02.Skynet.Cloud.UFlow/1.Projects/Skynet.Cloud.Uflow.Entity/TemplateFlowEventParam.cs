/************************************************************************************
* Copyright (c) 2019-07-11 10:53:10 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateFlowEventParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：751437f4-f087-4006-b9c4-61c6118006b0
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:10 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:10 
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
   [Table("UF_TEMPLATE_FLOW_EVENT_PARAM")]
   public class TemplateFlowEventParam
   {
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      [Column("EVENT_ID",DbType = DBType.NVarChar)]
      public string EventId{ set; get;}
      [Column("TITLE",DbType = DBType.NVarChar)]
      public string Title{ set; get;}
      [Column("PARAM_NAME",DbType = DBType.NVarChar)]
      public string ParamName{ set; get;}
      [Column("PARAM_TAG",DbType = DBType.NVarChar)]
      public string ParamTag{ set; get;}
      [Column("PARAM_TYPE",DbType = DBType.Int32)]
      public int ParamType{ set; get;}
      [Column("PARAM_VALUE",DbType = DBType.NVarChar)]
      public string ParamValue{ set; get;}
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
