using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 数据源信息
    /// </summary>
    
    public class CfgDataSource
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        
        public int SourceType { get; set; }
        /// <summary>
        /// 数据源名称
        /// </summary>
        
        public string DataSourceName { get; set; }
        /// <summary>
        /// SQL解析方式
        /// </summary>
        
        public UqlType ParseType { get; set; }
        /// <summary>
        /// 是否参与网元聚合:1=参数与，0=聚合的时候，不参与计算
        /// </summary>
        
        public bool IsNeAggregation { get; set; }
    }
}
