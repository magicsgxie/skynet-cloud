/************************************************************************************
* Copyright (c) 2019-07-11 10:52:44 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceDraft.cs
* 版本号：  V1.0.0.0
* 唯一标识：6c782c98-afba-4447-869a-6c071c40fae7
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

   [Table("UF_INSTANCE_DRAFT")]
   public class InstanceDraft
   {
      /// <summary>
      /// 唯一id
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 流程ID
      /// <summary>
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      /// <summary>
      /// 实例编号
      /// <summary>
      [Column("INSTANCE_CODE",DbType = DBType.NVarChar)]
      public string InstanceCode{ set; get;}
      /// <summary>
      /// 下一步处理步骤序号
      /// <summary>
      [Column("SELECT_STEP_SEQ",DbType = DBType.NVarChar)]
      public string SelectStepSeq{ set; get;}
      /// <summary>
      /// 0主办,1汇签,2协办,3抄送
      /// <summary>
      [Column("USER_TYPE",DbType = DBType.Int32)]
      public int? UserType{ set; get;}
      /// <summary>
      /// 下一步人员编号
      /// <summary>
      [Column("USER_CODE",DbType = DBType.NVarChar)]
      public string UserCode{ set; get;}
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
   }
}
