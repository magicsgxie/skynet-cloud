using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 网元条件
    /// </summary>
    
    public class NeQuery
    {

        /// <summary>
        /// 网元过滤条件
        /// </summary>
        
        public string NeCondition
        {
            set;
            get;
        }

        /// <summary>
        /// 载频列表。如果为空，即全部载频
        /// </summary>
        
        public string Carriers
        {
            set;
            get;
        }

        /// <summary>
        /// 网元查询显示模式
        /// </summary>
        
        public NeQueryMode NeQueryMode
        {
            set;
            get;
        }

        /// <summary>
        /// 网元聚合方式 0：不聚合 粒度同网元粒度
        /// </summary>
        
        public int NeAggregation
        {
            set;
            get;
        }

        /// <summary>
        /// 关联的聚合方式
        /// </summary>
        
        public int RelationNeAggregation
        {
            set;
            get;
        }
    }
}
