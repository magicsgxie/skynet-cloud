/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Entity.Perf
 * 文件名：  QueryFieldBase
 * 版本号：  V1.0.0.0
 * 唯一标识：65abbebf-3b04-4264-953c-e05abee00f3b
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/7/26 17:35:00
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/7/26 17:35:00
 * 修改人： 谢韶光
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
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// NewClass 的摘要说明
    /// </summary>
    
    public class QueryBase
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        
        public long FieldID
        {
            set; get;
        }

        /// <summary>
        /// 字段ID
        /// </summary>
        
        public string FieldName
        {
            set; get;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        
        public string Alias
        {
            set;
            get;
        }

        /// <summary>
        /// 是否BSC指标
        /// </summary>

        [Ignore]
        
        public int UserInBSC
        {
            get;
            set;
        }
        /// <summary>
        /// 是否BTS指标
        /// </summary>

        [Ignore]
        
        public int UserInBTS
        {
            get;
            set;
        }

        /// <summary>
        /// 是否小区指标
        /// </summary>
        [Ignore]
        
        public int UserInCELL
        {
            get;
            set;
        }

        /// <summary>
        /// 是否载频指标
        /// </summary>
        [Ignore]
        
        public int UserInCARR
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Ignore]
        
        public bool IsVisibility
        {
            get;
            set;
        }
    }
}
