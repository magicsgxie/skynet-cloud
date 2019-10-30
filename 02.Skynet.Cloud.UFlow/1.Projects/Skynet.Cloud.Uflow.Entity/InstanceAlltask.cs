/************************************************************************************
* Copyright (c) 2019-07-11 10:53:50 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceAlltask.cs
* 版本号：  V1.0.0.0
* 唯一标识：144fbb5b-0ebe-4c27-9cd0-05e49b41af53
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:50 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:50 
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
   [Table("UF_INSTANCE_ALLTASK")]
   public class InstanceAlltask
   {
      [Column("FID",DbType = DBType.NVarChar)]
      public string Fid{ set; get;}
      [Column("CREATE_DATE",DbType = DBType.DateTime)]
      public DateTime? CreateDate{ set; get;}
      [Column("BEGINDATE",DbType = DBType.DateTime)]
      public DateTime? Begindate{ set; get;}
      [Column("ENDDATE",DbType = DBType.DateTime)]
      public DateTime? Enddate{ set; get;}
      [Column("USEDHOURS",DbType = DBType.Int32)]
      public int? Usedhours{ set; get;}
      [Column("STATUS",DbType = DBType.Int32)]
      public int? Status{ set; get;}
      [Column("STATUSNAME",DbType = DBType.VarChar)]
      public string Statusname{ set; get;}
      [Column("PROGRESS",DbType = DBType.Int32)]
      public int? Progress{ set; get;}
      [Column("PREVIOUSSTEPID",DbType = DBType.NVarChar)]
      public string Previousstepid{ set; get;}
      [Column("FLOWSTEPSEQ",DbType = DBType.Int32)]
      public int? Flowstepseq{ set; get;}
      [Column("FLOWSTEPNAME",DbType = DBType.NVarChar)]
      public string Flowstepname{ set; get;}
      [Column("INSTANCEID",DbType = DBType.NVarChar)]
      public string Instanceid{ set; get;}
      [Column("INSTANCECODE",DbType = DBType.NVarChar)]
      public string Instancecode{ set; get;}
      [Column("INSTANCESTARTDATE",DbType = DBType.DateTime)]
      public DateTime Instancestartdate{ set; get;}
      [Column("INSTANCEOVERDATE",DbType = DBType.DateTime)]
      public DateTime? Instanceoverdate{ set; get;}
      [Column("INSTANCESTARTUSER",DbType = DBType.NVarChar)]
      public string Instancestartuser{ set; get;}
      [Column("INSTANCESTARTUSERNAME",DbType = DBType.NVarChar)]
      public string Instancestartusername{ set; get;}
      [Column("INSTANCESTATUS",DbType = DBType.Int32)]
      public int Instancestatus{ set; get;}
      [Column("INSTANCESTATUSNAME",DbType = DBType.VarChar)]
      public string Instancestatusname{ set; get;}
      [Column("INSTANCEFIELD1",DbType = DBType.NVarChar)]
      public string Instancefield1{ set; get;}
      [Column("INSTANCEFIELD2",DbType = DBType.NVarChar)]
      public string Instancefield2{ set; get;}
      [Column("INSTANCEFIELD3",DbType = DBType.NVarChar)]
      public string Instancefield3{ set; get;}
      [Column("INSTANCEFIELD4",DbType = DBType.NVarChar)]
      public string Instancefield4{ set; get;}
      [Column("INSTANCEFIELD5",DbType = DBType.NVarChar)]
      public string Instancefield5{ set; get;}
      [Column("INSTANCEFIELD6",DbType = DBType.NVarChar)]
      public string Instancefield6{ set; get;}
      [Column("GROUPID",DbType = DBType.NVarChar)]
      public string Groupid{ set; get;}
      [Column("FLOWCODE",DbType = DBType.NVarChar)]
      public string Flowcode{ set; get;}
      [Column("FLOWNAME",DbType = DBType.NVarChar)]
      public string Flowname{ set; get;}
      [Column("FLOWTYPEID",DbType = DBType.NVarChar)]
      public string Flowtypeid{ set; get;}
      [Column("FLOWTYPENAME",DbType = DBType.NVarChar)]
      public string Flowtypename{ set; get;}
      [Column("PRIOR_STEPNAME",DbType = DBType.NVarChar)]
      public string PriorStepname{ set; get;}
   }
}
