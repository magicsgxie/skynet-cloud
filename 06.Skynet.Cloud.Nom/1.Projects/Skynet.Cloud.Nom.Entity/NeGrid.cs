/************************************************************************************
 * Copyright (c) 2016-06-22 16:37:04 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Nom.Entity
 * 文件名：  UWay.Skynet.Cloud.Nom.Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：6f21b30e-6d3c-4a8c-afb3-c27d15bf53b8
 * 创建人：  潘国
 * 电子邮箱：pang@uway.cn
 * 创建时间：2016-06-22 16:37:04 
 * 描述： 
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016-06-22 16:37:04 
 * 修改人： 潘国
 * 版本号： V1.0.0.0
 * 描述：
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    /// <summary>
    /// 栅格表
    /// </summary>
   
   public class NeGrid
   {
      /// <summary>
      /// 网格编号
      /// <summary>
      
      public string ReseauNo
      {
         get;
         set;
      }
      /// <summary>
      /// 网格ID
      /// <summary>
      
      public int? ReseauId
      {
         get;
         set;
      }
      /// <summary>
      /// 中心纬度
      /// <summary>
      
      public double LatitudeCenter
      {
         get;
         set;
      }
      /// <summary>
      /// 中心经度
      /// <summary>
      
      public double LongitudeCenter
      {
         get;
         set;
      }
      /// <summary>
      /// 右下角纬度
      /// <summary>
      
      public double LatitudeRb
      {
         get;
         set;
      }
      /// <summary>
      /// 右下角经度
      /// <summary>
      
      public double LongitudeRb
      {
         get;
         set;
      }
      /// <summary>
      /// 左上角纬度
      /// <summary>
      
      public double LatitudeLt
      {
         get;
         set;
      }
      /// <summary>
      /// 左上角经度
      /// <summary>
      
      public double LongitudeLt
      {
         get;
         set;
      }
      /// <summary>
      /// 栅格名称
      /// <summary>
      
      public string GridName
      {
         get;
         set;
      }
      /// <summary>
      /// 城市ID
      /// <summary>
      
      public int CityId
      {
         get;
         set;
      }
      /// <summary>
      /// 栅格N
      /// <summary>
      
      public int GridN
      {
         get;
         set;
      }
      /// <summary>
      /// 栅格M
      /// <summary>
      
      public int GridM
      {
         get;
         set;
      }
      /// <summary>
      /// 栅格ID
      /// <summary>
      
      public long GridId
      {
         get;
         set;
      }
   }
}
