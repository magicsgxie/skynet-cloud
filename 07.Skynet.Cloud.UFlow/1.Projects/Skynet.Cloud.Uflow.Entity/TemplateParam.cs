/************************************************************************************
* Copyright (c) 2019-07-11 10:53:12 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：f197b483-9091-424e-a97e-4a785b35ea4d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:12 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:12 
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

   [Table("UF_TEMPLATE_PARAM")]
   public class TemplateParam
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
      /// 0事件,1消息,2子流程初始数据，3子流程发起数据
      /// <summary>
      [Column("TYPE",DbType = DBType.Int32)]
      public int Type{ set; get;}
      /// <summary>
      /// 分类主表ID
      /// <summary>
      [Column("PARENT_ID",DbType = DBType.NVarChar)]
      public string ParentId{ set; get;}
      /// <summary>
      /// 0上一步,1发起步骤,2当前步骤,3指定步骤,4已走流程
      /// <summary>
      [Column("TITLE",DbType = DBType.NVarChar)]
      public string Title{ set; get;}
      /// <summary>
      /// 参数名称
      /// <summary>
      [Column("PARAM_NAME",DbType = DBType.NVarChar)]
      public string ParamName{ set; get;}
      /// <summary>
      /// 扩展标记
      /// <summary>
      [Column("PARAM_TAG",DbType = DBType.NVarChar)]
      public string ParamTag{ set; get;}
      /// <summary>
      /// 0数据项，1自定义值
      /// <summary>
      [Column("PARAM_TYPE",DbType = DBType.Int32)]
      public int ParamType{ set; get;}
      /// <summary>
      /// 参数值
      /// <summary>
      [Column("PARAM_VALUE",DbType = DBType.NVarChar)]
      public string ParamValue{ set; get;}
   }
}
