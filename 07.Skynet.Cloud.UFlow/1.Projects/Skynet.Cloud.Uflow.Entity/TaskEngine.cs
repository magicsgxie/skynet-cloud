/************************************************************************************
* Copyright (c) 2019-07-11 10:53:25 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TaskEngine.cs
* 版本号：  V1.0.0.0
* 唯一标识：16f07128-ee4d-4401-97ed-a0e8474cff94
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:25 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:25 
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

   [Table("UF_TASK_ENGINE")]
   public class TaskEngine
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 服务器名称
      /// <summary>
      [Column("SERVER_NAME",DbType = DBType.NVarChar)]
      public string ServerName{ set; get;}
      /// <summary>
      /// 服务器IP
      /// <summary>
      [Column("SERVER_IP",DbType = DBType.NVarChar)]
      public string ServerIp{ set; get;}
      /// <summary>
      /// 启动时间
      /// <summary>
      [Column("START_DATE",DbType = DBType.DateTime)]
      public DateTime StartDate{ set; get;}
      /// <summary>
      /// 心跳时间
      /// <summary>
      [Column("HEART_DATE",DbType = DBType.DateTime)]
      public DateTime HeartDate{ set; get;}
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
