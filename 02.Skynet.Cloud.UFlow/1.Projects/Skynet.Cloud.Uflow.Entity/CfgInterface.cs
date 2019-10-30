/************************************************************************************
* Copyright (c) 2019-07-11 10:54:03 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.CfgInterface.cs
* 版本号：  V1.0.0.0
* 唯一标识：eef783c9-f041-4263-ae81-e1593c131076
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:54:03 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:54:03 
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

   [Table("UF_CFG_INTERFACE")]
   public class CfgInterface
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("GROUP_ID",DbType = DBType.NVarChar)]
      public string GroupId{ set; get;}
      /// <summary>
      /// 接口编号
      /// <summary>
      [Column("INTERFACE_CODE",DbType = DBType.NVarChar)]
      public string InterfaceCode{ set; get;}
      /// <summary>
      /// 接口名称
      /// <summary>
      [Column("INTERFACE_NAME",DbType = DBType.NVarChar)]
      public string InterfaceName{ set; get;}
      /// <summary>
      /// 接口地址
      /// <summary>
      [Column("ADDR",DbType = DBType.NVarChar)]
      public string Addr{ set; get;}
      /// <summary>
      /// 接口方法
      /// <summary>
      [Column("METHOD",DbType = DBType.NVarChar)]
      public string Method{ set; get;}
      /// <summary>
      /// 参数字符串
      /// <summary>
      [Column("PARAM",DbType = DBType.NVarChar)]
      public string Param{ set; get;}
      /// <summary>
      /// 说明
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
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
