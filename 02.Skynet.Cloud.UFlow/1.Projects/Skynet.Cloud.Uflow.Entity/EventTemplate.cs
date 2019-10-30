/************************************************************************************
* Copyright (c) 2019-07-11 10:53:33 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.EventTemplate.cs
* 版本号：  V1.0.0.0
* 唯一标识：538a90d6-88f3-4714-aab0-8dd1b1acb804
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:33 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:33 
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

   [Table("UF_EVENT_TEMPLATE")]
   public class EventTemplate
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 所属组织
      /// <summary>
      [Column("GROUP_ID",DbType = DBType.NVarChar)]
      public string GroupId{ set; get;}
      /// <summary>
      /// 事件标题
      /// <summary>
      [Column("TITLE",DbType = DBType.NVarChar)]
      public string Title{ set; get;}
      /// <summary>
      /// 地址
      /// <summary>
      [Column("ADDR",DbType = DBType.NVarChar)]
      public string Addr{ set; get;}
      /// <summary>
      /// 方法
      /// <summary>
      [Column("METHOD",DbType = DBType.NVarChar)]
      public string Method{ set; get;}
      /// <summary>
      /// 0否,1是
      /// <summary>
      [Column("WAIT_RESULT",DbType = DBType.Int32)]
      public int WaitResult{ set; get;}
      /// <summary>
      /// 创建者
      /// <summary>
      [Column("CREATOR",DbType = DBType.NVarChar)]
      public string Creator{ set; get;}
      /// <summary>
      /// 创建日期
      /// <summary>
      [Column("CREATE_DATE",DbType = DBType.DateTime)]
      public DateTime? CreateDate{ set; get;}
      /// <summary>
      /// 编辑者
      /// <summary>
      [Column("EDITOR",DbType = DBType.NVarChar)]
      public string Editor{ set; get;}
      /// <summary>
      /// 编辑日期
      /// <summary>
      [Column("EDIT_DATE",DbType = DBType.DateTime)]
      public DateTime? EditDate{ set; get;}
   }
}
