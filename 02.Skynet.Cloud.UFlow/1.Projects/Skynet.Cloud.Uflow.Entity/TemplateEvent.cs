/************************************************************************************
* Copyright (c) 2019-07-11 10:53:07 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：07353743-7aad-40e3-927b-5c5f766fc2de
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:07 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:07 
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

   [Table("UF_TEMPLATE_EVENT")]
   public class TemplateEvent
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 流程ID
      /// <summary>
      [Column("FLOW_ID",DbType = DBType.NVarChar)]
      public string FlowId{ set; get;}
      /// <summary>
      /// 事件标题
      /// <summary>
      [Column("TITLE",DbType = DBType.NVarChar)]
      public string Title{ set; get;}
      /// <summary>
      /// 0WebService
      /// <summary>
      [Column("INTERFACE_TYPE",DbType = DBType.Int32)]
      public int InterfaceType{ set; get;}
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
   }
}
