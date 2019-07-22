/************************************************************************************
* Copyright (c) 2019-07-11 10:53:13 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateServerMapping.cs
* 版本号：  V1.0.0.0
* 唯一标识：e107b495-3bc8-463c-970a-2a8d590af64e
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:13 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:13 
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

   [Table("UF_TEMPLATE_SERVER_MAPPING")]
   public class TemplateServerMapping
   {
      /// <summary>
      /// FID
      /// <summary>
      [Id("FID")]
      public int Fid{ set; get;}
      /// <summary>
      /// 服务器编号
      /// <summary>
      [Column("SERVER_ID",DbType = DBType.Int32)]
      public int? ServerId{ set; get;}
      /// <summary>
      /// 模版编号
      /// <summary>
      [Column("FLOW_CODE",DbType = DBType.NVarChar)]
      public string FlowCode{ set; get;}
   }
}
