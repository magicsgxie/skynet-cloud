/************************************************************************************
 * Copyright (c) 2016-03-10 09:52:55 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：UWay.Ufa.Enterprise.Entity
 * 文件名：  UWay.Ufa.Enterprise.Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：0972a2db-dc22-4bea-88b5-741416035f79
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
   public class BasedataChangedRecord
    {
    
      public string NewValue
      {
         get;
         set;
      }
    
      public string OldValue
      {
         get;
         set;
      }
    
      public string DatafieldText
      {
         get;
         set;
      }
    
      public string ImpTemplateID
      {
         get;
         set;
      }
    
      public string Remark
      {
         get;
         set;
      }
    
      public string Attachment
      {
         get;
         set;
      }
    
      public string DataRowKeyValue
      {
         get;
         set;
      }
    
      public int? CityID
      {
         get;
         set;
      }
    
      public string OperateUser
      {
         get;
         set;
      }
    
      public DateTime? OperateTime
      {
         get;
         set;
      }
    
      public string DataField
      {
         get;
         set;
      }
    
      public string DatabaseOperate
      {
         get;
         set;
      }
    
      public string DataRowTitle
      {
         get;
         set;
      }
    
      public string DataRowKey
      {
         get;
         set;
      }
    
      public int TemplateID
      {
         get;
         set;
      }
   }
}
