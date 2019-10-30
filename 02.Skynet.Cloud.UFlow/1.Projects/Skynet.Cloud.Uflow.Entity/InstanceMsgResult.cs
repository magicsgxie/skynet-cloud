/************************************************************************************
* Copyright (c) 2019-07-11 10:52:42 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceMsgResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：832f0274-29a9-42e1-8e7a-ab81cc33ac9b
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:52:42 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:52:42 
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
   [Table("UF_INSTANCE_MSG_RESULT")]
   public class InstanceMsgResult
   {
      [Id("FID")]
      public string Fid{ set; get;}
      [Column("INSTANCE_FLOW_ID",DbType = DBType.NVarChar)]
      public string InstanceFlowId{ set; get;}
      [Column("MSG_ID",DbType = DBType.NVarChar)]
      public string MsgId{ set; get;}
      [Column("USER_CODE",DbType = DBType.NVarChar)]
      public string UserCode{ set; get;}
      [Column("CONTENT",DbType = DBType.Text)]
      public string Content{ set; get;}
      [Column("BEGIN_DATE",DbType = DBType.DateTime)]
      public DateTime BeginDate{ set; get;}
      [Column("END_DATE",DbType = DBType.DateTime)]
      public DateTime? EndDate{ set; get;}
      [Column("STATUS",DbType = DBType.Int32)]
      public int Status{ set; get;}
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
