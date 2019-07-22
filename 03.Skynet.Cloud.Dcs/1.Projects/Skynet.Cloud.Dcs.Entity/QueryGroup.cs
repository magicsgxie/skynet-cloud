/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Entity.Perf
 * 文件名：  QueryGroup
 * 版本号：  V1.0.0.0
 * 唯一标识：6e789cff-48fe-49fe-b85a-325586ef1a7c
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/16 14:45:18
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/16 14:45:18
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：查询条件分组
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
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 查询条件分组
    /// </summary>
    
    public class QueryGroup
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        
        public string ID
        {
            set; get;
        }

        /// <summary>
        /// 模板ID
        /// </summary>
        
        public long TemplateID
        {
            set;
            get;
        } 


        /// <summary>
        /// 分组逻辑关系符
        /// </summary>
        
        public FilterCompositionLogicalOperator LogicalOperator
        {
            set; get;
        }

        /// <summary>
        /// 父分组条件ID
        /// </summary>
        
        public string ParentID
        {
            set;
            get;
        }

        /// <summary>
        /// 所有父分组ID，逗号分隔
        /// </summary>
        
        public string FuncKind
        {
            set; get;
        }

    }
}
