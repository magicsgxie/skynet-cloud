/************************************************************************************
 * Copyright (c) 2016-03-10 09:52:55 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：UWay.Ufa.Enterprise.Entity
 * 文件名：  UWay.Ufa.Enterprise.Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：d84f5d99-16df-4f36-a2cc-5610344e4fd7
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016-03-10 09:52:55 
 * 描述： 
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016-03-10 09:52:55 
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UWay.Ufa.Enterprise.Entity
{
   [DataContract]
   public class NormalQueryField
   {
      /// <summary>
      /// DisplayIndex：排序
      /// <summary>
    
      public int? DisplayIndex
      {
         get;
         set;
      }
      /// <summary>
      /// FieldShowFormat：显示序列化
      /// <summary>
    
      public string FieldShowFormat
      {
         get;
         set;
      }
      /// <summary>
      /// ImportTemplateFieldId：导入模版字段ID
      /// <summary>
    
      public int? ImportTemplateFeldID
      {
         get;
         set;
      }
      /// <summary>
      /// QueryTemplateId：查询模版ID
      /// <summary>
    
      public int? QueryTemplateID
      {
         get;
         set;
      }
      /// <summary>
      /// QueryFieldID：字段ID
      /// <summary>
    
      public int QueryFieldID
      {
         get;
         set;
      }
   }
}
