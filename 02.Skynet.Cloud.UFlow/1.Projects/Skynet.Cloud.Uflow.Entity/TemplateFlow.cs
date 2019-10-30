/************************************************************************************
* Copyright (c) 2019-07-11 10:53:08 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateFlow.cs
* 版本号：  V1.0.0.0
* 唯一标识：a63e90cb-4b7d-4965-97b4-6feeffbc5298
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:08 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:08 
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

   [Table("UF_TEMPLATE_FLOW")]
   public class TemplateFlow
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("GROUP_ID",DbType = DBType.NVarChar)]
      public string GroupId{ set; get;}
      /// <summary>
      /// 流程编号
      /// <summary>
      [Column("FLOW_CODE",DbType = DBType.NVarChar)]
      public string FlowCode{ set; get;}
      /// <summary>
      /// 流程名称
      /// <summary>
      [Column("FLOW_NAME",DbType = DBType.NVarChar)]
      public string FlowName{ set; get;}
      /// <summary>
      /// 关联T_WF_FlowType
      /// <summary>
      [Column("TYPE_ID",DbType = DBType.NVarChar)]
      public string TypeId{ set; get;}
      /// <summary>
      /// 流程分类名称
      /// <summary>
      [Column("TYPE_NAME",DbType = DBType.NVarChar)]
      public string TypeName{ set; get;}
      /// <summary>
      /// 0待用,1试用,2使用,3停用,4作废
      /// <summary>
      [Column("STATUS",DbType = DBType.Int32)]
      public int Status{ set; get;}
      /// <summary>
      /// 0按自然日,1按工作日
      /// <summary>
      [Column("RECKONTIME_TYPE",DbType = DBType.Int32)]
      public int ReckontimeType{ set; get;}
      /// <summary>
      /// 流程版本
      /// <summary>
      [Column("VERSION",DbType = DBType.NVarChar)]
      public string Version{ set; get;}
      /// <summary>
      /// 0最新版,大于0的部分都是旧版本，且是数据越小越旧。
      /// <summary>
      [Column("VERSION_SEQ",DbType = DBType.Int32)]
      public int VersionSeq{ set; get;}
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
   }
}
