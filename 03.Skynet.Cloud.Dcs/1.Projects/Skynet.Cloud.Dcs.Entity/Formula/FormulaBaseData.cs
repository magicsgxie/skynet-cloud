/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.36366
 * 机器名称：UWAY
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Entity.Perf
 * 文件名：  FormulaBaseData
 * 版本号：  V1.0.0.0
 * 唯一标识：2cbe2665-8582-4fdb-8687-4f47f142775e
 * 当前的用户域：UWay
 * 创建人：  潘国
 * 电子邮箱：pang@uway.cn
 * 创建时间：2016/12/26 15:23:48
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/12/26 15:23:48
 * 修改人： 潘国
 * 版本号： V1.0.0.0
 * 描述：
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 基础数据信息
    /// </summary>
   
    public class FormulaBaseData
    {
        /// <summary>
        /// 指标编号
        /// </summary>
        
        public long AttID
        {
            set;
            get;
        }

        /// <summary>
        /// 分组编号
        /// </summary>
        
        public long GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// 指标英文名
        /// </summary>
        
        public string AttEnName
        {
            set;
            get;
        }

        /// <summary>
        /// 指标中文名
        /// </summary>
        
        public string AttCnName
        {
            set;
            get;
        }


       /// <summary>
       /// 网络制式
       /// </summary>
          
        public int NeType { get; set; }

       /// <summary>
       /// 网元级别
       /// </summary>
          
          public int NeLevel { get; set; }

        
          public int BusinessType { get;set;}

        /// <summary>
        /// 公式
        /// </summary>
        
        public string Formula
        {
            set;
            get;
        }

        /// <summary>
        /// 公式2
        /// </summary>
        
        public string Formula1
        {
            set;
            get;
        }

        /// <summary>
        /// 公式3
        /// </summary>
        
        public string Formula2
        {
            set;
            get;
        }


        /// <summary>
        /// 网元粒度组合，power(2,粒度-1) ,bitand()
        /// </summary>
        
        public Int64 NeCombination
        {
            set;
            get;
        }

        
        public string VendorVersion { get; set; }

        /// <summary>
        /// 厂家聚合方式
        /// </summary>
        
        public string VendorAggregation
        {
            set;
            get;
        }

       
        public int DataSource { get; set; }
        /// <summary>
        /// 是否是"0"
        /// </summary>
        
        public decimal IsZero
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        
        public Int16 DigitalDigit
        {
            set;
            get;
        }

        /// <summary>
        /// 使用方式，用二进制位表示
        /// </summary>
        
        public int UseType
        {
            set;
            get;
        }
    }
}
