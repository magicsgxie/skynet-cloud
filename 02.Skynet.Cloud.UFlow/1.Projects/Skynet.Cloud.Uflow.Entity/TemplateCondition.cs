/************************************************************************************
* Copyright (c) 2019-07-11 10:53:27 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.TemplateCondition.cs
* 版本号：  V1.0.0.0
* 唯一标识：3b38deb9-0cf2-42c3-b756-b22b9871e906
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:27 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:27 
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

   [Table("UF_TEMPLATE_CONDITION")]
   public class TemplateCondition
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
      /// 0与,1或
      /// <summary>
      [Column("RELATION",DbType = DBType.Int32)]
      public int Relation{ set; get;}
      /// <summary>
      /// 关联T_WF_DataItem。
      /// <summary>
      [Column("DATAITEM_ID",DbType = DBType.NVarChar)]
      public string DataitemId{ set; get;}
      /// <summary>
      /// 0仅判断条件来源的存在性,1仅判断数据项的存在性,2关系比较
      /// <summary>
      [Column("COMPARE_TYPE",DbType = DBType.Int32)]
      public int CompareType{ set; get;}
      /// <summary>
      /// 0等于,1不等于,2包含,3属于,4大于,5大于等于,6小于等于,7小于
      /// <summary>
      [Column("COMPARE_VALUE",DbType = DBType.Int32)]
      public int CompareValue{ set; get;}
      /// <summary>
      /// 0数据项,1自定义值
      /// <summary>
      [Column("VALUE_TYPE",DbType = DBType.Int32)]
      public int ValueType{ set; get;}
      /// <summary>
      /// 值数据
      /// <summary>
      [Column("VALUE",DbType = DBType.NVarChar)]
      public string Value{ set; get;}
      /// <summary>
      /// 描述
      /// <summary>
      [Column("REMARK",DbType = DBType.NVarChar)]
      public string Remark{ set; get;}
   }
}
