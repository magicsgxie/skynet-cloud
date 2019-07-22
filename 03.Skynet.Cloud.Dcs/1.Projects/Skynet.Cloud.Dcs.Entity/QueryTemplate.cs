/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Entity.Perf
 * 文件名：  QueryTemplateDTO
 * 版本号：  V1.0.0.0
 * 唯一标识：835154a3-b123-4bb7-98fe-da2fea9e95e2
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/16 15:17:23
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/16 15:17:23
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：模板类数据库对应实体
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
    /// 模板类数据库对应实体
    /// </summary>
    
    public class QueryTemplate:NeField
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        
        public long TemplateID
        {
            set;
            get;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        
        public string TemplateName
        {
            set;
            get;
        }

        /// <summary>
        /// 所属分组
        /// </summary>
        
        public int GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// 是否有网元
        /// </summary>
        
        public bool IsHasNe
        {
            set;
            get;
        }

        /// <summary>
        /// 模板类型
        /// </summary>
        
        public int ShareType
        {
            set;
            get;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        
        public string CreatedByName
        {
            set; get;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        
        public int CreatedBy
        {
            set; get;
        }

        /// <summary>
        /// 修改人ID
        /// </summary>
        
        public int UpdatedBy
        {
            set; get;
        }

        /// <summary>
        /// 修改人
        /// </summary>
        
        public string UpdatedByName
        {
            set; get;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        
        public DateTime? CreatedTime
        {
            set; get;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        
        public DateTime UpdatedTime
        {
            set; get;
        }

        /// <summary>
        /// 关联配置模板
        /// </summary>
        
        public long? CfgTempID
        {
            set; get;
        }

        /// <summary>
        /// 关联配置模板
        /// </summary>
        
        public int MenuID
        {
            set; get;
        }
    }
}
