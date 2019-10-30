/************************************************************************************
* Copyright (c) 2019-07-11 10:53:11 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateMsgParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：18e2126f-c865-45e6-999b-611b6a4aba0b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:11 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:11 
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
   [Table("UF_TEMPLATE_MSG_PARAM")]
   public class TemplateMsgParam
   {
      [Id("FID")]
      public string Fid{ set; get;}
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      [Column("MSG_ID",DbType = DBType.NVarChar)]
      public string MsgId{ set; get;}
      [Column("PARAM_ID",DbType = DBType.NVarChar)]
      public string ParamId{ set; get;}
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
